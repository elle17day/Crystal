//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemyStats : MonoBehaviour
{

    [SerializeField] private GameObject host;       // Parent of script
    [SerializeField] private string alias;          // Name of enemy
    [SerializeField] private bool isEnemy;

    [SerializeField] private bool isDead;           // Bool for checking state of enemy
    [SerializeField] private float currentHealth;   // Enemies current health
    [SerializeField] private float maxHealth;       // Enemies maximum health
    [SerializeField] private float armour;          // Enemies armour 
    [SerializeField] private float speed;           // Enemy speed value
    [SerializeField] private int waveCost;          // Arbitrary 'enemy cost', scale difficulty with wave budgets.

    [SerializeField] private bool isSlowed;         // Bool for applying speed modifiers
    [SerializeField] private bool isPoisoned;       // Bool for checking poisoned state
    [SerializeField] private bool isBurned;         // Bool for checking burned state

    [SerializeField] private Material aliveMat;     // Debug material for alive
    [SerializeField] private Material deadMat;      // Debug material for death

    private NavMeshAgent agent;
    private Renderer objectRenderer;


    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material = aliveMat;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.isEnemy = true;
        this.currentHealth = maxHealth;
        SetDestination(Crystal.Instance.GetCrystalLocation());
    }

    // Update is called once per frame
    void Update()
    {
        if ((this.currentHealth <= 0) && (isDead == false)) // Calls death function
        {
            onDeath();  
        }

        SetDestination(new Vector3(Random.Range(-10f,10f), 0, Random.Range(-10f, 10f)));
    }

    void onDeath()
    {
        Debug.Log(transform.name + " is dead.");
        agent.speed = 0; // Stops enemy moving after death
        isDead = true; // Stops event being called multiple times
        gameObject.GetComponent<MeshRenderer>().material = deadMat; // Change colour to show death
        Destroy(this.gameObject, 4f);   // Destroys object after 4 seconds
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void SetDestination(Vector3 destination)
    {
        if (agent != null)
        {
            if (!agent.pathPending && agent.remainingDistance < 1f) {
                agent.destination = destination;
            }
        }
    }
}
