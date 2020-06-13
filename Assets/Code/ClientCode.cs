// created by Victor
// https://github.com/VictorsGarden

using Services;
using UnityEngine;

public class ClientCode : MonoBehaviour
{
    private void Start()
    {
        var task1 = Locator.GetServiceAsync<Service1>();
        var task2 = Locator.GetServiceAsync<Service2>();
        var task3 = Locator.GetServiceAsync<Service3>();
        var task4 = Locator.GetServiceAsync<Service4>();

        task1.ContinueWith(task => Debug.Log(task.Result.field));
        task2.ContinueWith(task => Debug.Log(task.Result.field));
        task3.ContinueWith(task => Debug.Log(task.Result.field));
        task4.ContinueWith(task => Debug.Log(task.Result.field));

        // another variant is to wait service through await command
        // it will stop the thread until the service will be obtained
        // var task1 = await Locator.GetService<Service2>();
    }
}