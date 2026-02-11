using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 moveDir;
    public float moveSpeed;
    public float notGroundedPenalty;
    public float boostSpeed;
    public Vector3 bodyRotate;
    public Vector3 camRotate;
    public float rotateSpeed;
    public Transform cam;
    public Rigidbody rb;
    public float jumpStrength;
    public float gravity;

    public float groundDistance;
    public RaycastHit groundedHit;
    public bool grounded;
    private int doubleJumpsRemaining;
    public int doubleJumps;
    private int dashesRemaining;
    public int dashes;
    // Update is called once per frame
    void Update()
    {
        BodyMovement();
        CamMovement();
        Jump();
        if (Physics.Raycast(transform.position, -transform.up, out groundedHit, groundDistance))
        {
            grounded = true;
            doubleJumpsRemaining = doubleJumps;
            dashesRemaining = dashes;
        }
        else
        {
            grounded = false;
        }
    }
    private void BodyMovement()
    {
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.z = Input.GetAxis("Vertical");

        if (grounded == true)
        {
            rb.AddRelativeForce(moveSpeed * Time.deltaTime * moveDir, ForceMode.Impulse);            
        }
        else
        {
            rb.AddRelativeForce(moveSpeed * notGroundedPenalty * Time.deltaTime * moveDir, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(dashesRemaining > 0)
            {
                if (grounded == true)
                {
                    rb.AddForce(transform.forward * boostSpeed * 5, ForceMode.Impulse);
                }
                else
                {
                    rb.AddForce(transform.forward * boostSpeed, ForceMode.Impulse);
                }
                dashesRemaining -= 1;
            }
            //if (grounded == true)
            //{
            //    rb.AddForce(transform.forward * boostSpeed * 5, ForceMode.Impulse);
            //}
            //else
            //{
            //    rb.AddForce(transform.forward * boostSpeed, ForceMode.Impulse);
            //}
        }
    }
    private void CamMovement()
    {
        bodyRotate.y = Input.GetAxis("Mouse X");
        camRotate.x = -Input.GetAxis("Mouse Y");

        transform.Rotate(bodyRotate * Time.deltaTime * rotateSpeed);
        cam.Rotate(camRotate * Time.deltaTime * rotateSpeed);
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (doubleJumpsRemaining > 0)
            {
                rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
                doubleJumpsRemaining -= 1;
            }
        }

        if (grounded == true)
        {
            rb.linearDamping = 10f;            
        }
        else
        {
            rb.linearDamping = 0.05f;
        }
    }
}
