using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject FPSController;
    public GameObject TPSController;

    public Camera TPSCamera;
    public Camera FPSCamera;

    private int currentCamera;

    // Use this for initialization
    void Start () {
        currentCamera = 0;
        FPSController.SetActive(false);
        TPSController.SetActive(true);
        //Debug.Log("FPS:" + FPSController.activeInHierarchy + " TPS: " + TPSController.activeInHierarchy);
        //TPSCamera.enabled = true;
        //FPSCamera.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (TPSController.activeInHierarchy) {
            this.transform.position = TPSController.transform.position;
            this.transform.rotation = TPSController.transform.rotation;
        }
        if (FPSController.activeInHierarchy) {
            this.transform.position = FPSController.transform.position;
            this.transform.rotation = FPSController.transform.rotation;
        }
        TPSCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 4f, this.transform.position.z - 2f);

        if (Input.GetKeyDown(KeyCode.V)) {
            //Debug.Log("Switching Controllers!");
            Debug.Log("FPS:" + FPSController.activeInHierarchy + " TPS: " + TPSController.activeInHierarchy);
            ToggleController();
        }
	}

    public int CurrentCamera() {
        return currentCamera;
    }

    void ToggleController() {
        switch(currentCamera) {
            case 0:
                currentCamera = 1;
                TPSCamera.enabled = false;
                TPSController.SetActive(false);
                FPSController.SetActive(true);
                FPSController.transform.position = transform.position + Vector3.up;
                FPSController.transform.rotation = transform.rotation;
                return;
            case 1:
                currentCamera = 0;
                TPSCamera.enabled = true;
                FPSController.SetActive(false);
                TPSController.SetActive(true);
                TPSController.transform.position = transform.position + Vector3.down;
                TPSController.transform.rotation = transform.rotation;
                return;
        }
    }
}
