# ServiceLocatorAsync
Register your service by type value to the Service Locator dictionary.
It will trigger an event that service was added. This event subscribers for callback.
You can't register the same service twice.
Asynchronously obtaining the service by type from dictionary.
You can sets a subscription to the event like a promise of adding a new service or stop the flow before it's received.

```csharp
private void Awake()
  {
    Locator.RegisterIn(this);
  }
```
