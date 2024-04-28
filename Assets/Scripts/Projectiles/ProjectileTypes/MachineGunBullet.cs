using UnityEngine;

namespace Projectiles.ProjectileTypes
{
    public class MachineGunBullet : Projectile
    {
        private void Start()
        {
            Body.AddForce(transform.up * projectileSpeed, ForceMode2D.Impulse);

        }

        protected override void OnTriggerEnter2D(Collider2D col)
        {
            
        }
    }
}