using System.Linq;
using Cars;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControls
{
    /* [RequireComponent(typeof(Car))] */
    public class PlayerCarInput : MonoBehaviour
    {
        private InputMap _inputMap;
        private PlayerInput _playerInput;
        private Car _car;
        public GameObject _cameraPrefab;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            var cars = FindObjectsByType<Car>(FindObjectsSortMode.None);
            var index = _playerInput.playerIndex;
            Debug.Log(index);
            _car = cars.FirstOrDefault(car => car.playerIndex == index);
            /* GameObject camera = Instantiate(_cameraPrefab);
            _playerInput.camera = camera.GetComponent<Camera>();
            camera.GetComponent<CameraFollow>().car = _car; */
        }

        void FixedUpdate() {
            /* Debug.Log(_car.steering);
            Debug.Log(_car.rotationAngle); */
        }

       /*  private void OnEnable()
        {
            _inputMap.Driving.Enable();
        }
        
        private void OnDisable()
        {
            _inputMap.Driving.Disable();
        } */

        public void Accelerate(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                _car.isAccelerating = true;
            }
            else if (ctx.canceled)
            {
                _car.isAccelerating = false;
            }
        }

        public void Break(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                _car.isBreaking = true;
            }
            else if (ctx.canceled)
            {
                _car.isBreaking = false;
            }
        }

        public void Shoot(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                _car.isFiring = true;
            }
            else if (ctx.canceled)
            {
                _car.isFiring = false;
            }
        }

        public void Special(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                _car.ShootSpecial();
            }
        }

        public void Steer(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
                _car.steering.x = ctx.ReadValue<Vector2>().x;
            else if (ctx.canceled)
                _car.steering.x = 0;
        }
    }
}