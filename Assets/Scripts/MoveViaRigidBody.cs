using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveViaRigidBody : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 1f;
    public float groundDistance = 0.2f;
    public float dashDistance = 5f;
    public float doubleJumpDuration = 0.5f;
    public float flySpeedMultiplier = 5f;
    public LayerMask ground;
    public Transform geometry;

    
    private bool isGrounded = false;
    private Rigidbody body;
    private Vector3 inputs = Vector3.zero;
    private float distanceToGround = 0f;
    private float timeSinceJump = 0;

    public void Start() {
        this.body = GetComponent<Rigidbody>();
        this.distanceToGround = GetComponent<Collider>().bounds.extents.y;
    }

    public void Update()
    {
        this.isGrounded = this.IsIntersectingWithGround();

        this.inputs = Vector3.zero;
        this.inputs.x = Input.GetAxis("Horizontal");
        this.inputs.z = Input.GetAxis("Vertical");

        if (this.isGrounded) {
            this.EnableGravity();
        }
        
        if (Input.GetButtonDown("Decend")) {
            this.Dash(Vector3.down, this.flySpeedMultiplier);
        }

        if (Input.GetButtonUp("Decend")
            && !this.body.useGravity) {
            this.body.velocity = new Vector3(
                this.body.velocity.x,
                0,
                this.body.velocity.z
            );
        }

        if (Input.GetButtonDown("Jump")) {
            if (this.timeSinceJump < this.doubleJumpDuration) {
                this.ToggleGravity();
                this.timeSinceJump = this.doubleJumpDuration;
            } else {
                this.timeSinceJump = 0;
                this.Dash(Vector3.up, this.flySpeedMultiplier);
            }
        }

        if (Input.GetButtonUp("Jump")
            && !this.body.useGravity) {
            this.body.velocity = new Vector3(
                this.body.velocity.x,
                0,
                this.body.velocity.z
            );
        }

        if (Input.GetButtonDown("Dash")) {
            Vector3 dashVelocity = Vector3.Scale(
                this.transform.forward,
                this.dashDistance *
                new Vector3(
                    Mathf.Log(1f / (Time.deltaTime * body.drag + 1)) / -Time.deltaTime, 
                    0, 
                    Mathf.Log(1f / (Time.deltaTime * body.drag + 1)) / -Time.deltaTime
                )
            );
            this.body.AddForce(dashVelocity, ForceMode.VelocityChange);
        }

        this.timeSinceJump += Time.deltaTime;
    }


    public void FixedUpdate() {
        this.body.MovePosition(
            this.body.position
            + transform.TransformDirection(this.inputs)
            * this.speed
            * Time.fixedDeltaTime
            * (
                this.body.useGravity
                ? 1f
                : this.flySpeedMultiplier
            )
        );
    }

    private void ToggleGravity() {
        this.body.useGravity = !this.body.useGravity;
    }

    private void DisableGravity() {
        this.body.useGravity = false;
    }

    private void EnableGravity() {
        this.body.useGravity = true;
    }

    private bool IsIntersectingWithGround() {
        return this.IsIntersectingWithGround(this.geometry.transform.position);
    }

    private bool IsIntersectingWithGround(Vector3 position) {
        return Physics.Raycast(
            position, 
            Vector3.down, 
            this.distanceToGround + 0.1f
        );
    }

    private void Dash(Vector3 direction, float multiplier = 1) {
        this.body.AddForce(
            direction
            * Mathf.Sqrt(
                this.jumpHeight
                * -2f 
                * Physics.gravity.y
            )
            * multiplier, 
            ForceMode.VelocityChange
        );
    }
}
