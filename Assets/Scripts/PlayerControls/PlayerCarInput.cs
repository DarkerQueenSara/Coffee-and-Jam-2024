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
            _inputMap.Driving.Accelerate.performed += _ => _car.isAccelerating = true;
            _inputMap.Driving.Accelerate.canceled += _ => _car.isAccelerating = false;
            _inputMap.Driving.Break.performed += _ => _car.isBreaking = true;
            _inputMap.Driving.Break.canceled += _ => _car.isBreaking = false;
            _inputMap.Driving.Shoot.performed += _ => _car.isFiring = true;
            _inputMap.Driving.Shoot.canceled += _ => _car.isFiring = false;
            _inputMap.Driving.Special.performed += _ => _car.ShootSpecial();
            _inputMap.Driving.Steer.performed += ctx => _car.steering = ctx.ReadValue<Vector2>();
            _inputMap.Driving.Steer.canceled += _ => _car.steering = Vector2.zero;
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