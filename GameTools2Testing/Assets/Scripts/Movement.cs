using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public KeyCode runKey, crouchKey;

    public float walkSpeed;
    public float runSpeed;
    public float moveBuildup;
    public float moveWindDown;

    bool isRunning, isCrouching;

    float currentSpeed = 0;

    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate() {
        if (Input.GetAxis("Vertical") > 0 && currentSpeed < walkSpeed) {
            currentSpeed += moveBuildup;
        } else if (Mathf.Abs(Input.GetAxis("Vertical")) <= 0 && currentSpeed > 0) {
            currentSpeed -= moveWindDown;
        }
        
        if (isRunning && currentSpeed >= walkSpeed && currentSpeed <= runSpeed - moveBuildup) {
            currentSpeed += moveBuildup;
        } else if (!isRunning && currentSpeed >= walkSpeed) {
            currentSpeed -= moveWindDown;
        }
        
        float turn = Mathf.Lerp(anim.GetFloat("Turn"), Input.GetAxis("Horizontal"), Time.deltaTime);

        anim.SetFloat("Forward", currentSpeed / runSpeed);
        anim.SetFloat("Turn", turn);
    }

    void Update() {
        isRunning = Input.GetKey(runKey);

        if (Input.GetKeyDown(runKey) && isCrouching) {
            isCrouching = false;
        }

        if (Input.GetKeyDown(crouchKey)) {
            isCrouching = !isCrouching;
        }
    }

}
