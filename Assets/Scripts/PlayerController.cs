using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float jumpForce;
    public float walkSpeed;
    public float runSpeed;
    public float gravity;

    private CharacterController controller;
    private CameraController cameraControl;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 lookDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
        cameraControl = GetComponent<CameraController>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if (cameraControl.CurrentCamera() == 0) {
            LookDirection(input);

            moveDirection.x = input.x * runSpeed;
            moveDirection.z = input.y * runSpeed;
            if (controller.isGrounded) {
                moveDirection.y = 0;
                moveDirection = transform.TransformDirection(moveDirection);


                if (Input.GetButtonDown("Jump")) {
                    moveDirection.y = jumpForce;
                }
            }
        }

        if (cameraControl.CurrentCamera() == 1) {


            moveDirection = input.y * transform.GetChild(0).forward * runSpeed;
            moveDirection += input.x * transform.GetChild(0).right * runSpeed;

            if (controller.isGrounded) {
                moveDirection.y = 0;

                if (Input.GetButtonDown("Jump")) {
                    moveDirection.y = jumpForce;
                }
            }
        }
        

        

        
        moveDirection.y -= gravity * Time.deltaTime;

        

        controller.Move(moveDirection * Time.deltaTime);

        Debug.DrawRay(transform.position, moveDirection, Color.red);
    }

    void LookDirection(Vector2 input) {
        
        if (input.magnitude > 0.25f) {
            lookDirection = new Vector3(input.x, 0, input.y);
            transform.GetChild(0).rotation = Quaternion.LookRotation(lookDirection);
        }
        
    }
}
