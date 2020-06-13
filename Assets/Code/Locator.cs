// created by Victor
// https://github.com/VictorsGarden

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class Locator
{
    private static Dictionary<Type, object> services = new Dictionary<Type, object>();

    private const int timeout = 10000;

    private static event Action<Type> OnServiceAdded;

    /// <summary>
    /// Asynchronously obtaining the service by type from dictionary.
    /// You can set subscription to the adding new service event or stop the flow before it's received.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public static async Task<T> GetServiceAsync<T>()
    {
        var serviceType = typeof(T);

        await WaitUntil<T>(() => services.ContainsKey(serviceType), serviceType);

        if (services.TryGetValue(serviceType, out var requiredObject))
        {
            return (T) requiredObject;
        }

        throw new NullReferenceException($"Cannot find {serviceType.Name} service");
    }

    /// <summary>
    /// Register your service by type value to the Service Locator dictionary.
    /// It will trigger an event that service was added. This event subscribers for callback.
    /// You can't register the same service twice.
    /// </summary>
    /// <param name="service"></param>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="ArgumentException"></exception>
    public static void Register<T>(T service)
    {
        var serviceType = typeof(T);

        if (!services.ContainsKey(serviceType))
        {
            services.Add(serviceType, service);
            OnServiceAdded?.Invoke(serviceType);
        }
        else
        {
            throw new ArgumentException($"<color=red><b>The service {serviceType.Name} has already been added</b></color>");
        }
    }

    private static async Task WaitUntil<T>(Func<bool> condition, Type type)
    {
        if (!condition())
        {
            var tcs = new TaskCompletionSource<Type>();

            OnServiceAdded += serviceType =>
            {
                if (serviceType == type)
                {
                    tcs.SetResult(serviceType);
                }
            };

            if (tcs.Task != await Task.WhenAny(tcs.Task, Task.Delay(timeout)))
            {
                throw new TimeoutException($"<color=red><b>The time to find {typeof(T).Name} is over</b></color>");
            }
        }
    }
}
