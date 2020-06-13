// created by Victor
// https://github.com/VictorsGarden

namespace Examples
{
    using Services;
    using UnityEngine;
    using System.Threading.Tasks;

    public class Example2 : MonoBehaviour
    {
        public bool AreServicesObtained;

        private void Start()
        {
            var task1 = Locator.GetServiceAsync<Service1>();
            var task2 = Locator.GetServiceAsync<Service2>();
            var task3 = Locator.GetServiceAsync<Service3>();
            var task4 = Locator.GetServiceAsync<Service4>();

            Task[] tasks = {task1, task2, task3, task4};

            var tasksContainer = Task.WhenAll(tasks);

            tasksContainer.ContinueWith(task =>
            {
                AreServicesObtained = true;
                Debug.Log("all services successfully loaded");
            });
        }
    }
}