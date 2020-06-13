namespace Services
{
    using System.Threading.Tasks;
    using UnityEngine;

    public class Service3 : MonoBehaviour
    {
        public int delayMillSec = 999;
        public int field        = 30;

        private async void Awake()
        {
            // For Testing
            await Task.Delay(delayMillSec);

            Locator.RegisterIn(this);
        }
    }
}