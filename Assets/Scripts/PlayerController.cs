using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float jumpForce;
    public float walkSpeed;
    public float runSpeed;
    public float gravity;

    public MouseLook mouseLook = new MouseLook();

    private CharacterController controller;
    private CameraController cameraControl;
    private Transform model;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 lookDirection = new Vector3(0,0,1);

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        cameraControl = GetComponent<CameraController>();
        
        model = transform;
        mouseLook.Init(model, cameraControl.FPSCamera.transform);
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Third-Person View
        if (cameraControl.CurrentCamera() == 0) {
            
            LookDirection(input);

            moveDirection.x = input.x * runSpeed;
            moveDirection.z = input.y * runSpeed;
            
        }

        // First-Person View
        if (cameraControl.CurrentCamera() == 1) {

            moveDirection.x = 0;
            moveDirection.z = 0;

            mouseLook.LookRotation(model, cameraControl.FPSCamera.transform);
            lookDirection = transform.forward;
            
            moveDirection += input.y * transform.forward * runSpeed;
            moveDirection += input.x * transform.right * runSpeed;
            
        }

        if (controller.isGrounded) {
            moveDirection.y = 0;

            if (Input.GetButtonDown("Jump")) {
                moveDirection.y = jumpForce;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        Debug.DrawRay(transform.position, moveDirection, Color.red);
    }

    void LookDirection(Vector2 input) {
        if (input.magnitude > 0) {
            lookDirection = new Vector3(input.x, 0, input.y);
            model.localRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
        }
    }
}
