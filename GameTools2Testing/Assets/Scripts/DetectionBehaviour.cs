using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionBehaviour : MonoBehaviour {

    Transform parentTransform;

    public LayerMask hitLayer;

    public bool inScan;
    public bool canSee;

	void Start () {
        parentTransform = GetComponentInParent<Transform>();
	}

    void OnTriggerStay(Collider col) {
        if (col.CompareTag("Player")) {
            inScan = true;

            RaycastHit ray;

            Vector3 rayDirection = (col.gameObject.transform.position - parentTransform.transform.position).normalized;
            

            Debug.DrawRay(parentTransform.position, rayDirection);
            Physics.Raycast(parentTransform.position, rayDirection, out ray, Mathf.Infinity, hitLayer);

            if (ray.transform.gameObject.CompareTag("Player")) {
                canSee = true;
            } else {
                canSee = false;
            }
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.CompareTag("Player")) {
            inScan = false;
            canSee = false;
        }
    }

}
