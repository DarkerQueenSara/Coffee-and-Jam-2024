using UnityEngine;

namespace Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        public LayerMask wallsLayer;
        
        public GameObject explosionPrefab;
        
        public bool destroy;
        public int projectileDamage = 1;
        public float projectileSpeed = 20.0f;
        
        protected Rigidbody2D Body;
        
        private void Awake()
        {
            Body = GetComponent<Rigidbody2D>();
        }

        protected abstract void OnTriggerEnter2D(Collider2D col);
    }
}