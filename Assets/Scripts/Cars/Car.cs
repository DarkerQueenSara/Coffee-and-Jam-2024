using PlayerControls;
using UnityEngine;

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
        public int health = 3;
    
        [HideInInspector] public bool isAccelerating = false;
        [HideInInspector] public bool isBreaking = false;
        [HideInInspector] public bool isFiring = false;
        [HideInInspector] public Vector2 steering;
        
        private Vector2 _position;
        private Rigidbody2D _rigidbody;

        // Start is called before the first frame update
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
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
            }
            else {
                _rigidbody.drag = 0;
            }
            /* if (steering.y != 0) currentAcceleration = steering.y * acceleration; // cheat code for AI throttle */
            Vector2 engineForce = transform.up * currentAcceleration;
            _rigidbody.AddForce(engineForce, ForceMode2D.Force);
        }

        private void ApplySteering() {
            if (GetComponent<PlayerCarInput>() != null) Debug.Log("Rot = " + _rigidbody.rotation);
            float minSpeedBeforeAllowTurning = _rigidbody.velocity.magnitude / 8;
            minSpeedBeforeAllowTurning = Mathf.Clamp01(minSpeedBeforeAllowTurning);
            rotationAngle -= steering.x * turnFactor * minSpeedBeforeAllowTurning;
            _rigidbody.rotation = (rotationAngle);
            if (GetComponent<PlayerCarInput>() != null) Debug.Log(rotationAngle);
        }

        private void KillOrthogonalVelocity() {
            Vector2 forwardVelocity = transform.up * Vector2.Dot(_rigidbody.velocity, transform.up);
            Vector2 rightVelocity = transform.right * Vector2.Dot(_rigidbody.velocity, transform.right);

            _rigidbody.velocity = forwardVelocity + rightVelocity * driftFactor;
        }

        public virtual void ShootSpecial() {
            Debug.Log("Special");
        }

    }
}
