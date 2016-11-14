using UnityEngine;
using System.Collections;

public class MouseLook {

    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public bool clampVerticalRotation = true;
    public float MinimumX = -90f;
    public float MaximumX = 90f;
    public bool smooth;
    public float smoothTime = 5f;
    public bool lockCursor = true;

    private Quaternion CharacterTargetRotation;
    private Quaternion CameraTargetRotation;
    private bool cursorLocked = true;

    public void Init(Transform character, Transform camera) {
        CharacterTargetRotation = character.localRotation;
        CameraTargetRotation = camera.localRotation;
    }

    public void LookRotation(Transform character, Transform camera) {
        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

        CharacterTargetRotation *= Quaternion.Euler(0f, yRot, 0f);
        CameraTargetRotation *= Quaternion.Euler(-xRot, 0f, 0f);

        if (clampVerticalRotation)
            CameraTargetRotation = ClampRotationAroundXAxis(CameraTargetRotation);

        if (smooth) {
            character.localRotation = Quaternion.Slerp(character.localRotation, CharacterTargetRotation,
                smoothTime * Time.deltaTime);
            camera.localRotation = Quaternion.Slerp(camera.localRotation, CameraTargetRotation,
                smoothTime * Time.deltaTime);
        } else {
            character.localRotation = CharacterTargetRotation;
            camera.localRotation = CameraTargetRotation;
        }

        UpdateCursorLock();
    }

    public void SetCursorLock(bool value) {
        lockCursor = value;
        if (!lockCursor) {//we force unlock the cursor if the user disable the cursor locking helper
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock() {
        //if the user set "lockCursor" we check & properly lock the cursos
        if (lockCursor)
            InternalLockUpdate();
    }

    private void InternalLockUpdate() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            cursorLocked = false;
        } else if (Input.GetMouseButtonUp(0)) {
            cursorLocked = true;
        }

        if (cursorLocked) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else if (!cursorLocked) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q) {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
