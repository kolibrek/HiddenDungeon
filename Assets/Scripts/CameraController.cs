using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Camera TPSCamera;
    public Camera FPSCamera;

    private int currentCamera;

    // Use this for initialization
    void Start () {
        currentCamera = 0;
        TPSCamera.enabled = true;
        FPSCamera.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        TPSCamera.transform.position = new Vector3(this.transform.position.x, 10f, this.transform.position.z - 10f);

        if (Input.GetKeyDown(KeyCode.V)) {
            ToggleCamera();
        }
	}

    void SelectCamera(int cameraNum) {

    }

    public int CurrentCamera() {
        return currentCamera;
    }

    void ToggleCamera() {
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
