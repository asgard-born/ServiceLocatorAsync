// created by Victor
// https://github.com/VictorsGarden

namespace Examples
{
    using Services;
    using UnityEngine;

    public class Example1 : MonoBehaviour
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
        }
    }
}