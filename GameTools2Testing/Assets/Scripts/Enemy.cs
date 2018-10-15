using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public float walkSpeed;
    public float runSpeed;
    public float lookAroundTime;
    public float lookAroundRadius;

    public List<Transform> patrolePoints = new List<Transform>();

    public string currentTarget;

    int currentPatrolNode;

    NavMeshAgent navAgent;

    Animator anim;

    DetectionBehaviour detector;

    void Start() {
        navAgent = GetComponent<NavMeshAgent>();
        detector = GetComponentInChildren<DetectionBehaviour>();

        Patrole();
    }

    void Update() {
        if (!navAgent.hasPath) {
            Debug.Log("nopath");
        }

        if (!navAgent.hasPath && currentTarget != "Player" && currentTarget != "PatrolNode") {
            LookAround();
        }

        if (!navAgent.hasPath && currentTarget == "PatrolNode") {
            Patrole();
        }
    }

    public void SetMoveTarget (GameObject newTarget, string targetType) {
        switch (targetType) {

            case "Player":
                currentTarget = targetType;
                navAgent.SetDestination(newTarget.transform.position);
                return;

            case "LastSeen":
                currentTarget = targetType;
                navAgent.SetDestination(newTarget.transform.position);
                return;

            case "Sound":
                if (currentTarget != "Player" && currentTarget != "LastSeen") {
                    currentTarget = targetType;
                    navAgent.SetDestination(newTarget.transform.position);
                }
                return;

            default:
                Debug.Log("Invalid target type!");
                Patrole();
                return;

        }
	}

    void LookAround() {
        float searchTime = 0;

        while (searchTime < lookAroundTime && !navAgent.hasPath) {
            detector.CircleDetect(lookAroundRadius);

            searchTime += Time.deltaTime;
        }

        if (!navAgent.hasPath) {
            Patrole();
        }
    }

    void Patrole() {
        if (!navAgent.hasPath && currentTarget == "PatrolNode") {
            if (currentPatrolNode == patrolePoints.Count - 1) {
                currentPatrolNode = 0;
            } else {
                currentPatrolNode++;
            }
        } else {
            currentTarget = "PatrolNode";
            currentPatrolNode = 0;
        }

        navAgent.SetDestination(patrolePoints[currentPatrolNode].position);
    }

}
