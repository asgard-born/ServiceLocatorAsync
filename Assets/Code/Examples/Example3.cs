// created by Victor
// https://github.com/VictorsGarden

namespace Examples
{
    using Services;
    using UnityEngine;

    public class Example3 : MonoBehaviour
    {
        public bool AreServicesObtained;

        private async void Start()
        {
            // 999 millisec 
            var service3 = await Locator.GetServiceAsync<Service3>();
            Debug.Log(service3.field);

            // 1500 millisec
            var service2 = await Locator.GetServiceAsync<Service2>();
            Debug.Log(service2.field);

            // 3000 millisec
            var service4 = await Locator.GetServiceAsync<Service4>();
            Debug.Log(service4.field);

            // 5000 millisec
            var service1 = await Locator.GetServiceAsync<Service1>();
            Debug.Log(service1.field);

            AreServicesObtained = true;
        }
    }
}