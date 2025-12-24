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
    Ray _ray;
    RaycastHit _hit;
    BoxCollider boxCollider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _ray = new Ray(Vector3.zero, Vector3.forward);
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

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isInUI == true)
            {
                
            }

            else if (Physics.Raycast(_ray, out _hit, 5f))
            {
                // check if hits tower and display UI
                Debug.Log(_hit.transform.gameObject.name);
                // check if hits enemy and have enemy take damage
            }
        }

    }

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


