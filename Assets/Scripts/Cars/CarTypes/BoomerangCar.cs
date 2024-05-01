using Projectiles.ProjectileTypes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cars.CarTypes
{
    public class BoomerangCar : Car
    {
        public GameObject boomerangPrefab;
        public Transform firePoint;
        
        [HideInInspector] public bool canShoot = true;

        private void Update()
        {
            if (isFiring) Shoot();
        }
        
        protected override void Shoot()
        {
            if (!canShoot) return;
            //If the player can shoot, we spawn a boomerang
            Vector3 gunPosition = firePoint.position;
            GameObject spawnedBoomerangObject = Instantiate(boomerangPrefab, gunPosition, firePoint.rotation);
            Boomerang spawnedBoomerang = spawnedBoomerangObject.GetComponent<Boomerang>();
            spawnedBoomerang.shooter = this;
            //Reset the cooldown
            canShoot = false;
        }
    }
}