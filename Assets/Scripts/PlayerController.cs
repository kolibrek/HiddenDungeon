using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float runSpeed = 6f;
    public float walkSpeed = 3f;
    public float jumpForce = 80f;
    public float gravityMult = 1f;

    Rigidbody rb;
    CharacterController cc;
    CameraController cameraControl;
    MouseLook mouseLook;
    Vector3 targetVelocity = Vector3.zero;
    private CollisionFlags m_CollisionFlags;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        mouseLook = GetComponent<MouseLook>();
        cameraControl = GetComponent<CameraController>();
        mouseLook.Init(this.transform, cameraControl.FPSCamera.transform);
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 kInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 mInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        //targetVelocity = Vector3.zero;

        // Third-Person view
        if (cameraControl.CurrentCamera() == 0) {
            targetVelocity = new Vector3(kInput.x, 0, kInput.y);
            targetVelocity.Normalize();

            
        }
        // First-Person view
        if (cameraControl.CurrentCamera() == 1) {
            targetVelocity = transform.right * kInput.x + transform.forward * kInput.y;
            targetVelocity.Normalize();

            mouseLook.LookRotation(this.transform, cameraControl.FPSCamera.transform);
        }

        targetVelocity *= runSpeed;

        

        if (cc.isGrounded) {
            targetVelocity.y = 0;
            if (Input.GetButtonDown("Jump")) {
                targetVelocity.y += jumpForce;
            }
        }
        
        rb.velocity = (targetVelocity);
    }

    void OnCollisionEnter(Collision collision) {
        Rigidbody body = collision.collider.attachedRigidbody;
        Debug.Log("hit " + body.name);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        Rigidbody body = hit.collider.attachedRigidbody;
        
    }
}
