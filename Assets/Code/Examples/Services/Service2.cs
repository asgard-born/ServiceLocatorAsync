namespace Examples.Services
{
    using System.Threading.Tasks;
    using UnityEngine;

    public class Service2 : MonoBehaviour
    {
        public int delayMillSec = 1500;
        public int field        = 50;

        private async void Start()
        {
            // For Testing
            await Task.Delay(delayMillSec);

            Locator.Register(this);
        }
    }
}