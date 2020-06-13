namespace Services
{
    using System.Threading.Tasks;
    using UnityEngine;

    public class Service1 : MonoBehaviour
    {
        public int delayMillSec = 4000;
        public int field        = 99;

        private async void Awake()
        {
            // For Testing
            await Task.Delay(delayMillSec);

            Locator.RegisterIn(this);
        }
    }
}