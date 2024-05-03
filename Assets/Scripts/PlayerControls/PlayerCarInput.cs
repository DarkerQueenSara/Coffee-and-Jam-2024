using System;
using Cars;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControls
{
    [RequireComponent(typeof(Car))]
    public class PlayerCarInput : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private Car _car;

        public void InitializeCar(PlayerInput pi)
        {
            _playerInput = pi;
            _car = GetComponent<Car>();
            _playerInput.actions["Steer"].performed += Steer;
            _playerInput.actions["Accelerate"].performed += Accelerate;            
            _playerInput.actions["Accelerate"].canceled += Accelerate;
            _playerInput.actions["Break"].performed += Break;
            _playerInput.actions["Break"].canceled += Break;
            _playerInput.actions["Shoot"].performed += Shoot;
            _playerInput.actions["Shoot"].canceled += Shoot;
            _playerInput.actions["Special"].performed += Special;
        }

       

        private void OnDisable()
        {
            if (_playerInput.actions != null)
            {
                _playerInput.actions["Steer"].performed -= Steer;
                _playerInput.actions["Accelerate"].performed -= Accelerate;            
                _playerInput.actions["Accelerate"].canceled -= Accelerate;
                _playerInput.actions["Break"].performed -= Break;
                _playerInput.actions["Break"].canceled -= Break;
                _playerInput.actions["Shoot"].performed -= Shoot;
                _playerInput.actions["Shoot"].canceled -= Shoot;
                _playerInput.actions["Special"].performed -= Special;
            }
        }

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