using UnityEngine;

namespace Cars.CarTypes
{
    public class MachineGunCar : Car
    {
        public float fireRate;
        
        public GameObject bulletPrefab;
        public Transform firePoint;
        
        private bool _canShoot = true;
        private float _timeLeft;
        
        private void Update()
        {
            //Check if the player can shoot
            if (_timeLeft < 0) _canShoot = true;
            _timeLeft -= Time.deltaTime;
        }

        protected override void Shoot()
        {
            if (!_canShoot) return;
            //If the player can shoot, we spawn a bullet
            Vector3 gunPosition = firePoint.position;
            Instantiate(bulletPrefab, gunPosition, firePoint.rotation);
            //Reset the cooldown
            _canShoot = false;
            _timeLeft = 1 / fireRate;
        }
    }
}