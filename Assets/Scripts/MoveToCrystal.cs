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
        if (agent.remainingDistance <= 1f)
        {
            Debug.Log("Path complete");
            Crystal.Instance.DamageCrystal(damage);
            Destroy(Object);
        }
    }
}
