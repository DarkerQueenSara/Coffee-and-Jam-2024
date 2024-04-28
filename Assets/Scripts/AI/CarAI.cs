using System;
using System.Collections;
using System.Collections.Generic;
using Cars;
using UnityEngine;
using UnityEngine.AI;

public class CarAI : MonoBehaviour {

    private Car _car;
    private NavMeshAgent _navMeshAgent;

    private void Awake() {
        _car = GetComponent<Car>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate() {
        FollowNearestPlayer();
    }

    private void FollowNearestPlayer() {
        // Find the nearest player
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject nearestPlayer = null;
        float nearestDistance = Mathf.Infinity;
        foreach (GameObject player in players) {
            float distance = Vector2.Distance(player.transform.position, transform.position);
            if (distance < nearestDistance) {
                nearestDistance = distance;
                nearestPlayer = player;
            }
        }
        if (nearestPlayer != null) {
            NavMeshPath path = new NavMeshPath();
            Vector2 target = nearestPlayer.transform.position;
            /* _navMeshAgent.CalculatePath (target, path); */

            float steerAmount = TurnToTarget(target/* path.corners[1] */);
            _car.steering = new Vector2(steerAmount,  0/* ApplyThrottleOrBreak(steerAmount) */);
            
            if (nearestDistance < 5) {
                _car.isFiring = true;
                _car.isBreaking = false;
                _car.isAccelerating = false;
            } else {
                _car.isBreaking = false;
                _car.isAccelerating = true;
            }

        }

    }

    private float TurnToTarget(Vector2 target) {
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();

        float angle = Vector2.SignedAngle(transform.up, direction);
        angle *= -1;

        // Turn as much as possible if angle > 45 and smooth turn if angle < 45
        float steerAmount = angle / 45f;

        steerAmount = Mathf.Clamp(steerAmount, -1f, 1f);

        return steerAmount;
    }

    private float ApplyThrottleOrBreak(float steer) { // Full acceleration if straight, very little acceleration if turning
        return 1.05f - Mathf.Abs(steer) / 1.0f;
    }

}