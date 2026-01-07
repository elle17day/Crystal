using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

// Script for moving enemies to crystal

public class MoveToCrystal : MonoBehaviour
{
    // Create private variables
    private NavMeshAgent agent;
    private GameObject Object;
    private float damage;

    private void Awake()
    {   // Assign variables
        agent = GetComponent<NavMeshAgent>();
        Object = this.gameObject;
    }

    private void Start()
    {   // Set agents destination as crystal location
        agent.SetDestination(Crystal.Instance.GetCrystalLocation());
    }

    void Update()
    {   // Checks to see if at crystal
        if (!agent.pathPending && agent.remainingDistance <= 1f)
        {
            Crystal.Instance.DamageCrystal(damage); // Damages crystal
            GameManager.Instance.ReduceEnemyCount();
            Destroy(Object);                        // Destroys object
        }
    }

    public void SetDamage(float dmg)
    {   // Method for setting enemies damage
        damage = dmg;
    }

    public void SetSpeed(float speed)
    {   // Methd for setting enemies speed
        agent.speed = speed;
    }
}
