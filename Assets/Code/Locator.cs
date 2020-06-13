using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class Locator
{
    private static Dictionary<Type, object> services = new Dictionary<Type, object>();

    private const int timeout        = 10000;
    private const int eternalTimeout = -1;

    private static event Action<Type> OnServiceAdded;

    /// <summary>
    /// Asynchronously obtaining the service by type from dictionary.
    /// You can sets a subscription to the event like a promise of adding a new service or stop the flow before it's received.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public static async Task<T> GetServiceAsync<T>()
    {
        await WaitUntil<T>(() => services.ContainsKey(typeof(T)), typeof(T), timeout);

        if (services.TryGetValue(typeof(T), out var requiredObject))
        {
            return (T) requiredObject;
        }

        throw new NullReferenceException($"Cannot find {typeof(T)} service");
    }

    /// <summary>
    /// Register your service by type value to the Service Locator dictionary.
    /// It will trigger an event that service was added. This event subscribers for callback.
    /// You can't register the same service twice.
    /// </summary>
    /// <param name="service"></param>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="ArgumentException"></exception>
    public static void RegisterIn<T>(T service)
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

    private static async Task WaitUntil<T>(Func<bool> condition, Type type, int timeout = eternalTimeout)
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