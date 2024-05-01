using System;
using Cars.CarTypes;
using UnityEngine;

namespace Projectiles.ProjectileTypes
{
    public class BoomerangSprite : MonoBehaviour
    {
        
        private BoomerangCar _shooter;
        private CircleCollider2D _collider2D;
        
        private void Start()
        {
            _collider2D = GetComponent<CircleCollider2D>();
            _collider2D.enabled = false;
            Invoke(nameof(ActivateCollider), 0.1f);
        }

        private void ActivateCollider()
        {
            _collider2D.enabled = true;
            _shooter = gameObject.transform.parent.gameObject.GetComponent<Boomerang>().shooter;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject == _shooter.gameObject)
            {
                _shooter.canShoot = true;
                Destroy(gameObject.transform.parent.gameObject);
            }
        }
    }
}