using NUnit.Framework;
using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class TestTower : MonoBehaviour
{

    [SerializeField] private float damage = 20f;
    [SerializeField] private float fireRate = 4f;
    [SerializeField] private float range = 300f;
    [SerializeField] private Material beamMat;

    private float lastShotTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= lastShotTime + fireRate)   // Waits fire rate time before shooting
        {
            ShootClosest();                         // Shoots at enemy
        }
    }

    void ShootClosest()
    {
        lastShotTime = Time.time;   // Updates last shot timer
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, range);   // Get list of game objects within range of tower
        var listEnemies = enemiesInRange.ToList();  // Converts collider to list of enemies within range
        var actEnemies = listEnemies.ToList();      // Creates an extra list to store actual enemies
        actEnemies.Clear();                         // Clears list of actual enemies
        if (listEnemies.Count > 0)  // Checks there is at least one enemy
        {
            foreach (var enemy in listEnemies)  // Iterates over list of enemies
            {
                if (enemy.GetComponent<TestEnemyStats>() != null)               // Checks enemy has a stat column
                {
                    Debug.Log(enemy.transform.name + " is enemy!");             // Debug line
                    var enemyObject = enemy.GetComponent<TestEnemyStats>();     // Gets the enemy stat component
                    if (!enemyObject.IsDead())                                  // Checks if enemy is dead
                    {
                        Debug.Log(enemy.transform.name + " is alive!");         // Debug line
                        actEnemies.Add(enemy);                                  // Adds alive enemies to list of actual enemies
                    }
                }
                if (enemy.GetComponent<TestEnemyStats>() == null)               // Debug lines to ensure all objects are checked
                {
                    Debug.Log(enemy.transform.name + " is not enemy!");
                }
            }
            if (actEnemies.Count > 0)       // Checks for actual enemies
            {
                actEnemies[0].SendMessage("TakeDamage", damage);                                        // Makes first enemy in list take damage
                Debug.Log("Enemy" + actEnemies[0].transform.name + " taking " + damage + " damage.");   // Debug line
                BeamEnemy(gameObject.transform.position, actEnemies[0].transform.position);             // Draws beam for visuals
            }
        }
    }

    void BeamEnemy(Vector3 tower, Vector3 enemy)
    {
        LineRenderer beam = gameObject.AddComponent<LineRenderer>();    // Creates line renderer
        beam.material = beamMat;                                        // Assigns red material
        beam.startWidth = 0.2f;                                         // Defines width of beam at start
        beam.endWidth = 0.2f;                                           // Defines width of beam at end
        beam.positionCount = 2;                                         // Defines number of beam vertices

        beam.SetPosition(0, tower);                                     // Define start of beam
        beam.SetPosition(1, enemy);                                     // Define end of beam
        Destroy(beam, 0.5f);                                            // Destroy beam after time has passed
    }

}
