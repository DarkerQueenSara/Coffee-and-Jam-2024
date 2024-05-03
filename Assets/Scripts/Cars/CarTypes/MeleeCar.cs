using UnityEngine;

namespace Cars.CarTypes
{
    public class MeleeCar : Car
    {
        protected override void Shoot()
        {
            //Do nothing
        }

        private void OnTriggerEnter2D(Collider2D other) {
            Car hit =  other.GetComponent<Car>();
            if (hit != null && ! hit.IsInvencible()) {
                hit.TakeHit();
            }
        }
    }
}