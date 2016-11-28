using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float runSpeed = 6f;
    public float walkSpeed = 3f;
    public float jumpForce = 80f;
    public float gravityMult = 1f;

    private bool isGrounded = false;

    Rigidbody rb;
    //CharacterController cc;
    CameraController cameraControl;
    MouseLook mouseLook;
    Vector3 targetVelocity = Vector3.zero;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        //cc = GetComponent<CharacterController>();
        mouseLook = GetComponent<MouseLook>();
        cameraControl = GetComponent<CameraController>();
        mouseLook.Init(this.transform, cameraControl.FPSCamera.transform);
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 kInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 mInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        targetVelocity = rb.velocity;
        float fallSpeed = targetVelocity.y;
        
        // Third-Person view
        if (cameraControl.CurrentCamera() == 0) {
            targetVelocity.x = kInput.x * runSpeed;
            targetVelocity.z = kInput.y * runSpeed;
        }
        // First-Person view
        if (cameraControl.CurrentCamera() == 1) {
            mouseLook.LookRotation(this.transform, cameraControl.FPSCamera.transform);

            targetVelocity = transform.forward * kInput.y * runSpeed + transform.right * kInput.x * runSpeed;
            targetVelocity.y = fallSpeed;
        }
        
        if (isGrounded) {
            if (Input.GetButtonDown("Jump")) {
                targetVelocity.y = jumpForce;
            }
        }

        rb.velocity = (targetVelocity);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Floor") {
            isGrounded = true;
        }
        if (collision.collider.GetComponent<Rigidbody>()) {
            Rigidbody body = collision.collider.GetComponent<Rigidbody>();
            Debug.Log("hit " + body.name);
        }
    }
}
