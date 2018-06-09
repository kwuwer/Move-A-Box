using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Reference to rigidbody
    public Rigidbody rb;
    // Reference to camera, which will follow the player
    public GameObject playerCamera;
    // Vector distance between camera start pos and player pos
    private Vector3 offset;
    // Force applied to player which will move it forward
    public float forwardForce;
    // Force for moving sideways
    public float sideForce;

    public bool lostLife;
    public bool finishedLevel;

    // Variables for getting the movement
    private bool leftSide;
    private bool rightSide;

    // parameters are set
    void Start () {
        offset = transform.position - playerCamera.transform.position;
        Debug.Log(GameManager.Instance.difficulty);
        forwardForce = GameManager.Instance.fwSpeed;
        sideForce = GameManager.Instance.sideSpeed;
        lostLife = false;
        finishedLevel = false;
	}

    private void Update()
    {
        if (Input.GetKey("a") || Input.GetKey("left")){
            leftSide = true;
            rightSide = false;
            return;
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            leftSide = false;
            rightSide = true;
            return;
        }
        leftSide = false;
        rightSide = false;

        if (transform.position.z >= 200){
            finishedLevel = true;
        }
        if (transform.position.y <0.0f){
            lostLife = true;
        }
    }

    // Force applied in FixedUpdate as it's preffered way of dealing with 
    // physics by Unity
    void FixedUpdate () {
        if (leftSide)
            rb.AddForce(-sideForce, 0, 0, ForceMode.VelocityChange);
        else if (rightSide)
            rb.AddForce(sideForce, 0, 0, ForceMode.VelocityChange);
        rb.AddForce(0, 0, forwardForce);
	}

    // Camera is set in Late Update as this is the last calculation before 
    // showing the frame
    private void LateUpdate()
    {
        playerCamera.transform.position = transform.position - offset;
    }

    // Collision!
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Obstacle") == true){
            Debug.Log("Collision!");
            forwardForce = 0;
            rb.AddForce(0, 0, forwardForce);
            lostLife = true;
        }
    }
}
