using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float cameraHeight = 8f;
    public float cameraOffset = 4f;
    public float cameraAngle = 45f;

    public Camera TPSCamera;
    public Camera FPSCamera;

    private int currentCamera;

    // Use this for initialization
    void Start () {
        currentCamera = 0;
        FPSCamera.enabled = false;
        TPSCamera.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {

        TPSCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + cameraHeight, this.transform.position.z - cameraOffset);

        if (cameraAngle != TPSCamera.transform.rotation.eulerAngles.x) {
            TPSCamera.transform.rotation = Quaternion.Euler(cameraAngle, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.V)) {
            ToggleController();
        }
	}

    // return 0 for Third-Person view and 1 for First-Person view;
    public int CurrentCamera() {
        return currentCamera;
    }

    // Switches between First-Person and Third-Person views.
    void ToggleController() {
        switch(currentCamera) {
            case 0:
                currentCamera = 1;
                FPSCamera.enabled = true;
                TPSCamera.enabled = false;
                return;
            case 1:
                currentCamera = 0;
                TPSCamera.enabled = true;
                FPSCamera.enabled = false;
                return;
        }
    }
}
