using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 boxSize;
    public float maxDistance;
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
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * inputX * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * inputY * Time.deltaTime * speed);

        

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10f;
        }
        else
        {
            speed = 5f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && GroundCheck())
        {
            rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, boxSize);
    }
    bool GroundCheck()
    {
        if (Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, maxDistance, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}


