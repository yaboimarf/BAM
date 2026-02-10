using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 moveDir;
    public float moveSpeed;
    public Vector3 bodyRotate;
    public Vector3 camRotate;
    public float rotateSpeed;
    public Transform cam;
    public Rigidbody rb;
    public float jumpStrength;
    // Update is called once per frame
    void Update()
    {
        BodyMovement();
        CamMovement();
        Jump();
    }
    private void BodyMovement()
    {
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.z = Input.GetAxis("Vertical");

        rb.AddRelativeForce(moveSpeed * Time.deltaTime * moveDir, ForceMode.Impulse);
        //transform.Translate(moveDir * Time.deltaTime * moveSpeed);
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
            rb.AddForce(Vector3.up * Time.deltaTime * jumpStrength, ForceMode.Impulse);
        }
    }
}
