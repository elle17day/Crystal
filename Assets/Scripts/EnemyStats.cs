using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    private GameObject host;        // Parent of script
    private string alias;           // Name of enemy
    private bool isEnemy;           

    public enum enemyType {Grunt,Elite,Swarmer,Tank,Boss};
    [SerializeField] private enemyType thisEnemy;

    private int WaveCount;

    private bool isDead;            // Bool for checking state of enemy
    private Renderer objectRenderer;

    private float currentHealth;    // Enemies current health
    private float maxHealth;        // Enemies maximum health
    private float armour;           // Enemies armour 
    private float speed;            // Enemy speed value
    private float damage;           // Enemy damage
    private Material enemyMat;      // Enemy material
    private Material deadMat;       // Dead material
    private float deadTimer;        // Time before destroying enemy
    private float enemyScale;       // Scaling for enemy capsule size

    private void Awake()
    {
        // Generate stats for enemies based on wave
        // Get wave number before generating stats
        host = this.gameObject;
        objectRenderer = GetComponent<Renderer>();

        GenerateStats(thisEnemy);   // Generate enemy stats
        enemyMat = Resources.Load("DebugAlive") as Material;
        deadMat = Resources.Load("DebugDead") as Material;

    }

    void Start()
    {
        // Adds component to move enemy to crystal
        MonoBehaviour script = host.AddComponent<MoveToCrystal>();
        script.SendMessage("SetSpeed", speed);
        script.SendMessage("SetDamage", damage);

        objectRenderer.material = enemyMat;
        host.transform.localScale = new Vector3(enemyScale, enemyScale, enemyScale);
    }

    void Update()
    {
        if ((this.currentHealth <= 0) && (isDead == false)) // Calls death function
        {
            onDeath();
        }
    }
    void onDeath()
    {
        isDead = true; // Stops event being called multiple times
        host.GetComponent<MeshRenderer>().material = deadMat; // Change colour to show death
        Destroy(this.gameObject, deadTimer);   // Destroys object after 3 seconds
    }

    public void ModifyEnemyType(enemyType newType)
    {
        thisEnemy = newType;
    }

    public void TakeDamage(float incomingDamage)
    {
        currentHealth -= incomingDamage/armour;
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void GenerateStats(enemyType enemy)
    {
        switch (enemy)
        {
            case enemyType.Grunt:
                isDead = false;
                maxHealth = 50f;
                currentHealth = maxHealth;
                armour = 1f;
                speed = 3f;
                damage = 2f;
                deadTimer = 1f;
                enemyScale = 0.8f;
                break;

            case enemyType.Elite:
                isDead = false;
                maxHealth = 80f;
                currentHealth = maxHealth;
                armour = 1.2f;
                speed = 2.5f;
                damage = 5f;
                deadTimer = 2f;
                enemyScale = 1f;
                break;

            case enemyType.Swarmer:
                isDead = false;
                maxHealth = 30f;
                currentHealth = maxHealth;
                armour = 1f;
                speed = 5f;
                damage = 1f;
                deadTimer = 1f;
                enemyScale = 0.5f;
                break;

            case enemyType.Tank:
                isDead = false;
                maxHealth = 150f;
                currentHealth = maxHealth;
                armour = 3f;
                speed = 1.5f;
                damage = 20f;
                deadTimer = 3f;
                enemyScale = 1.8f;
                break;

            case enemyType.Boss:
                isDead = false;
                maxHealth = 200f;
                currentHealth = maxHealth;
                armour = 2f;
                speed = 1f;
                damage = 50f;
                deadTimer = 5f;
                enemyScale = 2.2f;
                break;
        }
    }
}
