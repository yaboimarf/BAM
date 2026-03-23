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

    public float invertMultiplier = 1f;

    public float groundDistance;
    public RaycastHit groundedHit;
    public bool grounded;

    public float wallDistance;
    public RaycastHit walledHit;
    public bool walled;

    private int doubleJumpsRemaining;
    public int doubleJumps;
    private int dashesRemaining;
    public int dashes;

    //public int batteryRemaining;
    //public int dashCost;
    //public int jumpCost;

    public float maxDampening;
    public float minDampening;

    // Update is called once per frame
    void Update()
    {
        BodyMovement();
        Jump();
        WallCling();

        //if (Physics.Raycast(transform.position, -transform.up, out groundedHit, groundDistance))
        //{
        //    grounded = true;
        //    doubleJumpsRemaining = doubleJumps;
        //    dashesRemaining = dashes;
        //}
        //else
        //{
        //    grounded = false;
        //}
    }
    private void BodyMovement()
    {
        //body movement
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

        // cam movement
        bodyRotate.y = Input.GetAxis("Mouse X");
        camRotate.x = -Input.GetAxis("Mouse Y");

        transform.Rotate(bodyRotate * Time.deltaTime * rotateSpeed);
        cam.Rotate(camRotate * Time.deltaTime * rotateSpeed);

        // dashes
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
        }
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
            doubleJumpsRemaining = doubleJumps;
            dashesRemaining = dashes;
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
                rb.AddForce(10 * Time.deltaTime * transform.forward, ForceMode.Impulse);
                walled = true;
                Debug.Log("ik werk");
            }

            if (Physics.Raycast(transform.position, -transform.right, out walledHit, wallDistance))
            {
                rb.useGravity = false;
                rb.AddForce(1 * Time.deltaTime * -transform.right, ForceMode.Impulse);
                rb.AddRelativeForce(2 * Time.deltaTime * -transform.up, ForceMode.Impulse);
                rb.AddForce(10 * Time.deltaTime * transform.forward, ForceMode.Impulse);
                walled = true;
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
