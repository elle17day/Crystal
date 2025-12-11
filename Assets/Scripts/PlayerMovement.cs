using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float inputX;
    public float inputY;
    public float speed = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * inputX * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * inputY * Time.deltaTime * speed);
    }
}
