using Unity.VisualScripting;
using UnityEngine;

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



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.isEnemy = true;
        this.currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if ((this.currentHealth <= 0) && (isDead == false)) // Calls death function
        {
            onDeath();  
        }
    }

    void onDeath()
    {
        Debug.Log(transform.name + " is dead.");
        isDead = true; // Stops event being called multiple times
        gameObject.GetComponent<MeshRenderer>().material = deadMat; // Change colour to show death
        Destroy(this.gameObject, 4f);   // Destroys object after 4 seconds
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
}
