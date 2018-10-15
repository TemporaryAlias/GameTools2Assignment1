using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionBehaviour : MonoBehaviour {

    Transform parentTransform;

    Enemy parentEnemy;

    public LayerMask hitLayer;

    public bool inScan;
    public bool canSee;

	void Start () {
        parentTransform = GetComponentInParent<Transform>();
        parentEnemy = GetComponentInParent<Enemy>();
	}

    void OnTriggerStay(Collider col) {
        if (col.CompareTag("Player")) {
            inScan = true;

            RaycastHit ray;

            Vector3 rayDirection = (col.gameObject.transform.position - parentTransform.transform.position).normalized;
            

            Debug.DrawRay(parentTransform.position, rayDirection);
            Physics.Raycast(parentTransform.position, rayDirection, out ray, Mathf.Infinity, hitLayer);

            if (ray.transform.gameObject.CompareTag("Player")) {
                parentEnemy.SetMoveTarget(col.gameObject, "Player");
                canSee = true;
            } else {
                canSee = false;
            }
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.CompareTag("Player")) {
            parentEnemy.SetMoveTarget(col.gameObject, "LastSeen");

            inScan = false;
            canSee = false;
        }
    }

    public void CircleDetect(float radius) {
        RaycastHit[] hits = Physics.SphereCastAll(parentTransform.position, radius, Vector3.up);
        
        foreach (RaycastHit hit in hits) {
            if (hit.collider.gameObject.CompareTag("Player")) {
                inScan = true;

                RaycastHit ray;

                Vector3 rayDirection = (hit.transform.gameObject.transform.position - parentTransform.transform.position).normalized;


                Debug.DrawRay(parentTransform.position, rayDirection);
                Physics.Raycast(parentTransform.position, rayDirection, out ray, Mathf.Infinity, hitLayer);

                if (ray.transform.gameObject.CompareTag("Player")) {
                    parentEnemy.SetMoveTarget(hit.transform.gameObject, "Player");
                    canSee = true;
                } else {
                    canSee = false;
                }
            }
        }
    }

}
