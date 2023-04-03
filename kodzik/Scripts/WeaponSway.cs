using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] Rigidbody player;

    Vector3 startPos;
    [SerializeField] Transform orientation;
    public float smooth;
    public float lookIntensity = 1;
    public float velIntensity = 0.005f;

    void Awake() {
        startPos = transform.localPosition;
    }

    void Update() {
        UpdateSway();
    }
    private void UpdateSway() {
        Vector2 mouse = new Vector2(
            Input.GetAxisRaw("Mouse X"),
            Input.GetAxisRaw("Mouse Y")
        );
        mouse *= lookIntensity;

        Vector3 vel = player.velocity;
        vel = orientation.InverseTransformDirection(vel);

        Quaternion rotationX = Quaternion.AngleAxis(-mouse.y, Vector3.left);
        Quaternion rotationY = Quaternion.AngleAxis(-mouse.x, Vector3.up);


        Vector3 targetPos = startPos;

        targetPos += (-vel.x * velIntensity) * transform.right;
        targetPos += (-vel.y * velIntensity) * transform.up;
        targetPos += (-vel.z * velIntensity) * transform.forward;

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, smooth * Time.deltaTime);
    }

}
