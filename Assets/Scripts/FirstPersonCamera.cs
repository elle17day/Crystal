using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{

    public Transform player;
    public float moueSensitivity = 2f;
    float cameraVerticalRotation = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * moueSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * moueSensitivity;

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        player.Rotate(Vector3.up * inputX);

    }
}
