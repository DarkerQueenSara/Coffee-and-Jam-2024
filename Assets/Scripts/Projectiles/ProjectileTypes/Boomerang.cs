using Cars.CarTypes;
using Extensions;
using UnityEngine;

namespace Projectiles.ProjectileTypes
{
    public class Boomerang : Projectile
    {

        [HideInInspector] public BoomerangCar shooter;
        
        public float boomerangAcceleration;
        public float rotationSpeed;
        public GameObject boomerangSprite;
        
        private float _timeLeft;
        private bool _travelingBack;
        private bool _addedForceBack;
        
        private void Start()
        {
            Body.AddForce(transform.up * projectileSpeed, ForceMode2D.Impulse);
        }

        private void Update()
        {
            boomerangSprite.transform.Rotate(transform.up, rotationSpeed * Time.deltaTime);
            
            if (_travelingBack) return;
            _timeLeft -= Time.deltaTime;
            if (_timeLeft < 0) _travelingBack = true;
        }

        private void FixedUpdate()
        {
             Body.velocity = _travelingBack ? 
                            Body.velocity * (boomerangAcceleration * Time.fixedDeltaTime) : 
                            Body.velocity * (-boomerangAcceleration * Time.fixedDeltaTime);
            
            if (_travelingBack)
            {
                if (!_addedForceBack)
                {
                    Body.AddForce((shooter.transform.position- transform.position) * projectileSpeed, ForceMode2D.Impulse);
                    _addedForceBack = true;
                }
                else
                {
                    float targetMagnitude = Body.velocity.magnitude;
                    Body.AddForce((shooter.transform.position- transform.position) * projectileSpeed, ForceMode2D.Impulse);
                    Body.velocity = Body.velocity.normalized * targetMagnitude;
                }
            }
            else
            {
                _travelingBack = Body.velocity.magnitude <= 0.01f;
            }
        }


        protected override void OnTriggerEnter2D(Collider2D col)
        {
            if (wallsLayer.HasLayer(col.gameObject.layer))
            {
                _travelingBack = true;
            }

            if (col.gameObject == shooter.gameObject)
            {
                shooter.canShoot = true;
                Destroy(gameObject);
            }
        }
    }
}