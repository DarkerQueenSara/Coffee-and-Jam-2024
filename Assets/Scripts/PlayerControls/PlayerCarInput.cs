using Cars;
using UnityEngine;

namespace PlayerControls
{
    [RequireComponent(typeof(Car))]
    public class PlayerCarInput : MonoBehaviour
    {
        private InputMap _inputMap;
        
        private Car _car;
        
        private void Awake()
        {
            _car = GetComponent<Car>();
            _inputMap = new InputMap();
            _inputMap.Driving.Accelerate.performed += ctx => _car.isAccelerating = true;
            _inputMap.Driving.Accelerate.canceled += ctx => _car.isAccelerating = false;
            _inputMap.Driving.Break.performed += ctx => _car.isBreaking = true;
            _inputMap.Driving.Break.canceled += ctx => _car.isBreaking = false;
            _inputMap.Driving.Shoot.performed += ctx => _car.isFiring = true;
            _inputMap.Driving.Shoot.canceled += ctx => _car.isFiring = false;
            _inputMap.Driving.Special.performed += ctx => _car.ShootSpecial();
            _inputMap.Driving.Steer.performed += ctx => _car.steering.x = ctx.ReadValue<Vector2>().x;
            _inputMap.Driving.Steer.canceled += ctx => _car.steering.x = 0;
        }

        void FixedUpdate() {
            /* Debug.Log(_car.steering);
            Debug.Log(_car.rotationAngle); */
        }

        private void OnEnable()
        {
            _inputMap.Driving.Enable();
        }
        
        private void OnDisable()
        {
            _inputMap.Driving.Disable();
        }
    }
}