using Projectiles.ProjectileTypes;
using UnityEngine;

namespace Cars.CarTypes
{
    public class MachineGunCar : Car
    {
        public float fireRate;
        
        public GameObject bulletPrefab;
        public Transform firePoint;
        
        private bool _canShoot = true;
        private float _timeLeft = 0.0f;
        
        new protected void Update()
        {
            base.Update();
            //Check if the player can shoot
      
            if (_timeLeft < 0) _canShoot = true;
            _timeLeft -= Time.deltaTime;
            if (isFiring) Shoot();
        }

        protected override void Shoot()
        {
            if (!_canShoot) return;
            //If the player can shoot, we spawn a bullet
            Vector3 gunPosition = firePoint.position;
            GameObject bullet = Instantiate(bulletPrefab, gunPosition, firePoint.rotation);
            bullet.GetComponent<MachineGunBullet>()._shooter = this;
            //Reset the cooldown
            _canShoot = false;
            _timeLeft = 1 / fireRate;
        }
    }
}