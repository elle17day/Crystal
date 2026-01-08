using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isInUI = false;
    public Vector3 boxSize;
    public float boxDistance;
    public LayerMask layerMask;
    public float inputX;
    public float inputY;
    public float speed = 20f;
    Rigidbody rb;
    public float jumpAmount = 2;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player movement inputs
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * inputX * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * inputY * Time.deltaTime * speed);

        
        // Sprint code
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10f;
        }
        else
        {
            speed = 5f;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && GroundCheck())
        {
            rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
        }

        // Attack/Interact
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isInUI == true)
            {
                Debug.Log("Bad");
            }

            else 
            {
                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3f , layerMask))

                {
                    // check if hits tower and display UI
                    // check if hits enemy and have enemy take damage
                }
            }
        }

    }

    // Draw where the player hits
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up * boxDistance, boxSize);
    }
    bool GroundCheck()
    {
        if (Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, boxDistance, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}


