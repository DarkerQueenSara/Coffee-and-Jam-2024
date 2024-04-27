using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{

    float _top_speed = 10.0f;
    float _acceleration = 20f;
    bool _is_accelerating = false;
    bool _is_breaking = false;
    bool _is_firing = false;
    private float _current_speed = 0.0f;
    float _velocityVsUp = 0.0f;
    float rotation_angle = 0;
    float turn_factor = 3.5f;
    float _drift_factor = 0.95f;
    int _health = 3;
    Vector2 _position;
    Vector2 _steering;
    InputMap _input_map;
    Rigidbody2D _rigidbody;

    private void Awake() {
        _input_map = new InputMap();
        _input_map.Driving.Accelerate.performed += ctx => _is_accelerating = true;
        _input_map.Driving.Accelerate.canceled += ctx => _is_accelerating = false;
        _input_map.Driving.Break.performed += ctx => _is_breaking = true;
        _input_map.Driving.Break.canceled += ctx => _is_breaking = false;
        _input_map.Driving.Shoot.performed += ctx => _is_firing = true;
        _input_map.Driving.Shoot.canceled += ctx => _is_firing = false;
        _input_map.Driving.Special.performed += ctx => ShootSpecial();
        _input_map.Driving.Steer.performed += ctx => _steering = ctx.ReadValue<Vector2>();
        _input_map.Driving.Steer.canceled += ctx => _steering = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
        {
            _input_map.Driving.Enable();
        }

        /// <summary>
        /// Called when [disable].
        /// </summary>
        private void OnDisable()
        {
            _input_map.Driving.Disable();
        }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySteering();
    }

    void ApplyEngineForce() {

        float current_acceleration = 0;
        if (_is_accelerating) current_acceleration += _acceleration;
        if (_is_breaking) current_acceleration -= _acceleration;

        _velocityVsUp = Vector2.Dot(_rigidbody.velocity, transform.up);

        if (_velocityVsUp > _top_speed && current_acceleration > 0) {
            return;
        }

        if (_velocityVsUp < -_top_speed / 2 && current_acceleration < 0) {
            return;
        }

        if (_rigidbody.velocity.sqrMagnitude > _top_speed * _top_speed && current_acceleration > 0) {
            return;
        }

        if (current_acceleration == 0) {
            _rigidbody.drag = Mathf.Lerp(_rigidbody.drag, 3, Time.fixedDeltaTime * 3);
        }
        else {
            _rigidbody.drag = 0;
        }
        Vector2 engineForce = transform.up * current_acceleration;
        _rigidbody.AddForce(engineForce, ForceMode2D.Force);
    }

    void ApplySteering() {
        float minSpeedBeforeAllowTurning = _rigidbody.velocity.magnitude / 8;
        minSpeedBeforeAllowTurning = Mathf.Clamp01(minSpeedBeforeAllowTurning);

        rotation_angle -= _steering.x * turn_factor * minSpeedBeforeAllowTurning;

        _rigidbody.MoveRotation(rotation_angle);
    }

    void KillOrthogonalVelocity() {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(_rigidbody.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(_rigidbody.velocity, transform.right);

        _rigidbody.velocity = forwardVelocity + rightVelocity * _drift_factor;
    }

    void ShootSpecial() {
        Debug.Log("Special");
    }
}
