using JetBrains.Annotations;
using System;
using Unity.Mathematics;
using Unity.Mathematics.Geometry;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    [Header("Movement")]
    public Vector3 moveDir;
    public float moveSpeed;
    public float notGroundedPenalty;
    public float boostSpeed;
    public Rigidbody rb;
    public float jumpStrength;
    public float gravity;
    public float maxDampening;
    public float minDampening;

    public float invertMultiplier = 1f;

    [Header("Cam movement")]
    public Vector3 bodyRotate;
    public Vector3 camRotate;
    public float rotateSpeed;
    public Transform cam;
    public float minClamp;
    public float maxClamp;

    [Header("Ground checks")]
    public float groundDistance;
    public RaycastHit groundedHit;
    public bool grounded;

    [Header("WallCling checks")]
    public float wallDistance;
    public RaycastHit walledHit;
    public bool walled;
    public float wallClingBoostStrength;

    [Header("Air actions")]
    public int doubleJumps;
    private int dashesRemaining;
    private int doubleJumpsRemaining;
    public int dashes;

    [Header("Battery")]
    public float batteryRemaining;
    public float batteryMax;
    public float batteryMin;
    public float chargeRate;
    public float dashCost;
    public float jumpCost;

    // internal camera pitch tracked in degrees (-180..180)
    private float cameraPitch;

    void Start()
    {
        // Initialize cameraPitch from current local rotation and normalize to -180..180 range
        if (cam != null)
        {
            cameraPitch = cam.localEulerAngles.x;
            if (cameraPitch > 180f)
            {
                cameraPitch -= 360f;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (grounded == true)
        {
            if (batteryRemaining < batteryMax)
            {
                batteryRemaining += chargeRate * Time.deltaTime;
            }
        }
        if (batteryRemaining < batteryMin)
        {
            batteryRemaining = batteryMin;
        }

        BodyMovement();
        Jump();
        WallCling();
    }

    private void BodyMovement()
    {
        // body movement
        moveDir.x = Input.GetAxis("Horizontal") * invertMultiplier;
        moveDir.z = Input.GetAxis("Vertical") * invertMultiplier;

        if (grounded == true)
        {
            rb.AddRelativeForce(moveSpeed * Time.deltaTime * moveDir, ForceMode.Impulse);
        }
        else
        {
            rb.AddRelativeForce(moveSpeed * notGroundedPenalty * Time.deltaTime * moveDir, ForceMode.Impulse);
        }

        // mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // rotate body (yaw)
        if (Mathf.Abs(mouseX) > 0f)
        {
            // use rotateSpeed as sensitivity; multiply by Time.deltaTime for frame-rate independence
            transform.Rotate(Vector3.up * (mouseX * rotateSpeed * Time.deltaTime));
        }

        // rotate camera (pitch) with clamping
        if (cam != null)
        {
            // invert mouseY to match original intent (moving mouse up looks up)
            cameraPitch -= mouseY * rotateSpeed * Time.deltaTime;
            cameraPitch = Mathf.Clamp(cameraPitch, minClamp, maxClamp);

            // apply only pitch locally to avoid messing with player's yaw
            cam.localEulerAngles = new Vector3(cameraPitch, 0f, 0f);
        }

        // dashes
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (batteryRemaining >= dashCost)
            {
                if (grounded == true)
                {
                    rb.AddForce(transform.forward * boostSpeed, ForceMode.Impulse);
                }
                else
                {
                    rb.AddForce(transform.forward * boostSpeed * notGroundedPenalty, ForceMode.Impulse);
                }
                batteryRemaining -= dashCost;
            }
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (batteryRemaining >= jumpCost)
            {
                rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
                batteryRemaining -= jumpCost;
            }
        }

        // checks for sky vs ground linear dampening
        if (grounded == true)
        {
            rb.linearDamping = maxDampening;
        }
        else
        {
            rb.linearDamping = minDampening;
        }

        // checks if player is allowed to jump
        if (Physics.Raycast(transform.position, -transform.up, out groundedHit, groundDistance))
        {
            grounded = true;
            //doubleJumpsRemaining = doubleJumps;
            //dashesRemaining = dashes;
        }
        else
        {
            grounded = false;
        }
    }

    private void WallCling()
    {
        if (grounded == false)
        {
            if (Physics.Raycast(transform.position, transform.right, out walledHit, wallDistance))
            {
                rb.useGravity = false;
                rb.AddForce(1 * Time.deltaTime * transform.right, ForceMode.Impulse);
                rb.AddRelativeForce(2 * Time.deltaTime * -transform.up, ForceMode.Impulse);
                rb.AddForce(wallClingBoostStrength * Time.deltaTime * transform.forward, ForceMode.Impulse);
                //walled = true;
                Debug.Log("ik werk");
            }

            if (Physics.Raycast(transform.position, -transform.right, out walledHit, wallDistance))
            {
                rb.useGravity = false;
                rb.AddForce(1 * Time.deltaTime * -transform.right, ForceMode.Impulse);
                rb.AddRelativeForce(2 * Time.deltaTime * -transform.up, ForceMode.Impulse);
                rb.AddForce(wallClingBoostStrength * Time.deltaTime * transform.forward, ForceMode.Impulse);
                //walled = true;
            }

            if (!Physics.Raycast(transform.position, -transform.right, out walledHit, wallDistance))
            {
                if (!Physics.Raycast(transform.position, transform.right, out walledHit, wallDistance))
                {
                    rb.useGravity = true;
                }
            }
        }
    }
}