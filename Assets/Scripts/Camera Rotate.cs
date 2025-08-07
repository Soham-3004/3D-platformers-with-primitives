using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotate : MonoBehaviour
{
    public float rotationSpeed = 75f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float mouseY = Input.GetAxisRaw("Mouse X");
        transform.Rotate(Vector3.up * mouseY * Time.deltaTime * rotationSpeed);
    }
}
