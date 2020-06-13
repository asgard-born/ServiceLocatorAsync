# Asynchronous Service Locator
Register your service by type value to the Service Locator dictionary.
It will trigger an event that service was added. This event subscribers for callback.
You can't register the same service twice.
Asynchronously obtaining the service by type from dictionary.
You can sets a subscription to the event like a promise of adding a new service or stop the flow before it's received.

0. First of all we need to register service like this. Locator is static. I dont like an idea to make it singleton.
```csharp
private void Awake()
{
  Locator.RegisterIn(this);
}
```

In client code we can obtain our service by two ways:
1. Getting as Task (raw) and setting callback method. We have more options here
```csharp
public class ClientCode : MonoBehaviour
{
    private void Awake()
    {
        var task1 = Locator.GetServiceAsync<Service1>();
        var task2 = Locator.GetServiceAsync<Service2>();
        var task3 = Locator.GetServiceAsync<Service3>();
        var task4 = Locator.GetServiceAsync<Service4>();

        task1.ContinueWith(task => Debug.Log(task.Result.field));
        task2.ContinueWith(task => Debug.Log(task.Result.field));
        task3.ContinueWith(task => Debug.Log(task.Result.field));
        task4.ContinueWith(task => Debug.Log(task.Result.field));
    }
}

```
   1.1. A more elegant way to collect feedback when we've got all the services.
```csharp
private void Start()
{
    var task1 = Locator.GetServiceAsync<Service1>();
    var task2 = Locator.GetServiceAsync<Service2>();
    var task3 = Locator.GetServiceAsync<Service3>();
    var task4 = Locator.GetServiceAsync<Service4>();

    Task[] tasks = {task1, task2, task3, task4};

    Task tasksContainer = Task.WhenAll(tasks);

    tasksContainer.ContinueWith(task =>
    {
        isServicesObtained = true;
        Debug.Log("all services successfully loaded");
    });
}
```
2. Another variant is to wait service through await command
it will stop the thread until the service will be obtained
```csharp
private async void Start()
{
    var service1 = await Locator.GetServiceAsync<Service1>();
    Debug.Log(service1.field);

    var service2 = await Locator.GetServiceAsync<Service2>();
    Debug.Log(service2.field);

    var service3 = await Locator.GetServiceAsync<Service3>();
    Debug.Log(service3.field);

    var service4 = await Locator.GetServiceAsync<Service4>();
    Debug.Log(service4.field);

    isServicesObtained = true;
}
```

Benefits:
  You make subscription on adding service<br />
  Avoiding problems, when we have many singletones and time we getting them is earlier than time they are created<br />
  You can call the service even in Awake() method, not worrying about NullReferenceException (if timeout inside the Locator allows you to wait)
