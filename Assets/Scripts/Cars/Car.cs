using PlayerControls;
using UnityEngine;
using UnityEngine.UI;

namespace Cars
{
    public abstract class Car : MonoBehaviour
    {

        public float topSpeed = 10.0f;
        public float acceleration = 20f;
        public float driftFactor = 0.95f;
        public float velocityVsUp = 0.0f;
        public float rotationAngle = 0;
        public float turnFactor = 3.5f;
        public int maxHealth = 3;

        public Image healthBar;
        
        public int playerIndex;
        [HideInInspector] public bool isAccelerating = false;
        [HideInInspector] public bool isBreaking = false;
        [HideInInspector] public bool isFiring = false;
        [HideInInspector] public Vector2 steering;

        [SerializeField] private int _currentHealth;
        private float _invencibilityTime = 0;
        private Vector2 _position;
        private Rigidbody2D _rigidbody;
        private AudioSource _audioSource;

        // Start is called before the first frame update
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource>();
            _currentHealth = maxHealth;
        }

        protected void Update()
        {
            healthBar.fillAmount = 1.0f * _currentHealth / maxHealth;
            if (_invencibilityTime > 0) 
            {
                _invencibilityTime = Mathf.Max(0, _invencibilityTime - Time.deltaTime);
            }
        }

        private void FixedUpdate() {
            ApplyEngineForce();
            KillOrthogonalVelocity();
            ApplySteering();
        }

        private void ApplyEngineForce() {

            float currentAcceleration = 0;
            if (isAccelerating) currentAcceleration += acceleration;
            if (isBreaking) currentAcceleration -= acceleration;

            velocityVsUp = Vector2.Dot(_rigidbody.velocity, transform.up);

            if (velocityVsUp > topSpeed && currentAcceleration > 0) {
                return;
            }

            if (velocityVsUp < -topSpeed / 2 && currentAcceleration < 0) {
                return;
            }

            if (_rigidbody.velocity.sqrMagnitude > topSpeed * topSpeed && currentAcceleration > 0) {
                return;
            }

            if (currentAcceleration == 0) {
                _rigidbody.drag = Mathf.Lerp(_rigidbody.drag, 3, Time.fixedDeltaTime * 3);
                if (_audioSource != null) _audioSource.Play();
            }
            else {
                _rigidbody.drag = 0;
                if (_audioSource != null) _audioSource.Stop();
            }
            /* if (steering.y != 0) currentAcceleration = steering.y * acceleration; // cheat code for AI throttle */
            Vector2 engineForce = transform.up * currentAcceleration;
            _rigidbody.AddForce(engineForce, ForceMode2D.Force);
        }

        private void ApplySteering() {
            float minSpeedBeforeAllowTurning = _rigidbody.velocity.magnitude / 8;
            minSpeedBeforeAllowTurning = Mathf.Clamp01(minSpeedBeforeAllowTurning);
            rotationAngle -= steering.x * turnFactor * minSpeedBeforeAllowTurning;
            _rigidbody.rotation = (rotationAngle);
        }

        private void KillOrthogonalVelocity() {
            Vector2 forwardVelocity = transform.up * Vector2.Dot(_rigidbody.velocity, transform.up);
            Vector2 rightVelocity = transform.right * Vector2.Dot(_rigidbody.velocity, transform.right);

            _rigidbody.velocity = forwardVelocity + rightVelocity * driftFactor;
        }

        protected abstract void Shoot();

        public void ShootSpecial() {
            Debug.Log("Special");
        }

        public bool IsInvencible() {
            return _invencibilityTime > 0;
        }

        public void TakeHit() {
            _currentHealth--;
            if (_currentHealth <= 0) { // FIXME : maybe change this to another behaviour after death
                Destroy(gameObject);
                return;
            }
            _invencibilityTime = 3;
        }

    }
}
