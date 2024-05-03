using Cars;
using Cars.CarTypes;
using UnityEngine;

namespace Projectiles.ProjectileTypes
{
    public class MachineGunBullet : Projectile
    {

        public MachineGunCar _shooter;

        private void Start()
        {
            Body.AddForce(transform.up * projectileSpeed, ForceMode2D.Impulse);

        }

        protected override void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject == _shooter.gameObject) return;
            Car hit = col.GetComponent<Car>();
            if (hit != null && !hit.IsInvencible())
            {
                hit.TakeHit();
            }
        }
    }
}