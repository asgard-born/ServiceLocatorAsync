namespace Examples.Services
{
    using System.Threading.Tasks;
    using UnityEngine;

    public class Service4 : MonoBehaviour
    {
        public int delayMillSec = 3000;
        public int field        = 777;

        private async void Awake()
        {
            // For Testing
            await Task.Delay(delayMillSec);

            Locator.Register(this);
        }
    }
}