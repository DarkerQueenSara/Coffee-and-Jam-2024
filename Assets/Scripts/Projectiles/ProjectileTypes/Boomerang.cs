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
        private Vector2 _initialVelocity;
        
        private bool _travelingBack;
        private bool _addedForceBack;
        
        private void Start()
        {
            Body.AddForce(transform.up * projectileSpeed, ForceMode2D.Impulse);
        }

        private void Update()
        {
            boomerangSprite.transform.Rotate(transform.forward, rotationSpeed * Time.deltaTime);
            _initialVelocity = Body.velocity;
        }

        private void FixedUpdate()
        {
             Body.velocity = _travelingBack ? 
                            Body.velocity + Body.velocity.normalized * (boomerangAcceleration * Time.fixedDeltaTime) : 
                            Body.velocity - Body.velocity.normalized * (boomerangAcceleration * Time.fixedDeltaTime);
            
            if (_travelingBack)
            {
                if (!_addedForceBack)
                {
                    Body.AddForce((shooter.transform.position - transform.position).normalized * projectileSpeed, ForceMode2D.Impulse);
                    _addedForceBack = true;
                }
                else
                {
                    float targetMagnitude = Body.velocity.magnitude;
                    Body.AddForce((shooter.transform.position- transform.position).normalized * projectileSpeed, ForceMode2D.Impulse);
                    Body.velocity = Body.velocity.normalized * targetMagnitude;
                }
            }
            else
            {
                var velocity = Body.velocity;
                _travelingBack = velocity.magnitude <= 0.1f ||
                                 (_initialVelocity.x < 0 && velocity.x > 0) ||
                                 (_initialVelocity.y < 0 && velocity.y > 0) ||
                                 (_initialVelocity.x > 0 && velocity.x < 0) ||
                                 (_initialVelocity.y > 0 && velocity.y < 0);
            }
        }


        protected override void OnTriggerEnter2D(Collider2D col)
        {
            if (wallsLayer.HasLayer(col.gameObject.layer))
            {
                _travelingBack = true;
            }
        }
    }
}