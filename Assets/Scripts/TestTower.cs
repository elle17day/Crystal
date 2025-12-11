using NUnit.Framework;
using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;
using UnityEngine.Pool;

public class TestTower : MonoBehaviour
{

    [SerializeField] private float damage = 20f;
    [SerializeField] private float fireRate = 4f;
    [SerializeField] private float range = 300f;
    [SerializeField] private Material beamMat;
    [SerializeField] private int scatterCount = 2;

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
            ScatterTower();                            // Shoots at enemy
        }
    }

    /*  Protoype tower code
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
                actEnemies = actEnemies.OrderBy(enemy => (enemy.transform.position - transform.position).sqrMagnitude).ToList();    // Orders enemies by distance
                actEnemies[0].SendMessage("TakeDamage", damage);                                        // Makes first enemy in list take damage
                Debug.Log("Enemy" + actEnemies[0].transform.name + " taking " + damage + " damage.");   // Debug line
                BeamEnemy(actEnemies[0].transform.position);                                            // Draws beam for visuals
            }
        }
    }
    */

    // Function to get enemies in range
    private List<Collider> GetEnemiesInRange()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, range);  // Gets array of enemies in range
        List<Collider> enemiesList = enemies.ToList();                          // Converts array to list
        List<Collider> enemiesReturn = new List<Collider>();                    // Creates list to be returned
        if (enemiesList.Count > 0)                                              // Checks length of list
        {
            foreach (Collider enemy in enemiesList)
            {
                if (enemy.GetComponent<TestEnemyStats>() != null)               // Checks an object is an enemy
                {
                    var enemyObject = enemy.GetComponent<TestEnemyStats>();     // Gets the enemy stat component
                    if (!enemyObject.IsDead())                                  // Checks enemy is alive
                    {
                        enemiesReturn.Add(enemy);                               // Adds enemy to return list
                    }
                }
            }
            if (enemiesReturn.Count > 0)    // Only returns list if enemies are present
            { 
                return enemiesReturn; 
            } 
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    // Function to sort list by distance
    private Collider GetClosestEnemy(List<Collider> enemies)
    {
        List<Collider> returnEnemies = enemies.OrderBy(enemy => (enemy.transform.position - transform.position).sqrMagnitude).ToList(); // Sorts list by distance from tower
        return returnEnemies[0];    // Returns first entry
    }

    // Function for standard beam tower
    private void BeamTower()
    {
        if (GetEnemiesInRange() != null)
        {
            lastShotTime = Time.time;                                   // Updates last shot timer
            List<Collider> enemiesInRange = GetEnemiesInRange();        // Gets enemies in range
            Collider closestEnemy = GetClosestEnemy(enemiesInRange);    // Gets closest enemy to tower
            closestEnemy.SendMessage("TakeDamage", damage);             // Makes enemy take damage
            BeamEnemy(closestEnemy.transform.position);                 // Draws beam to enemy
        }
    }

    // Scatter tower hits multiple enemies simultaneously
    private void ScatterTower()
    {
        if (GetEnemiesInRange() != null)
        {
            lastShotTime = Time.time;                               // Updates last shot timer
            List<Collider> enemiesInRange = GetEnemiesInRange();    // Gets list of enemies in range
            List<Collider> targettedEnemies = new List<Collider>(); // Creates a list of the enemies to deal damage to
            bool findTargets = true;                                // Bool for looping array
            while (findTargets)             // Checks the right amount of enemies has been selected or every enemy has been selected
            {
                Collider curTarget = enemiesInRange[UnityEngine.Random.Range(0, enemiesInRange.Count)]; // Designates a target

                if (!DetectDuplicateTarget(targettedEnemies, curTarget))                                // Checks if target is already in list of targets
                {
                    targettedEnemies.Add(curTarget);    // Adds target to list of targets if it's not already a target
                }

                if (targettedEnemies.Count == scatterCount || targettedEnemies.Count == enemiesInRange.Count)   // Checks if max amount of enemies targetted or all enemies in range targetted
                {
                    findTargets = false;    // Stops looping over array
                }
            }
            foreach (Collider c in targettedEnemies)    // Loops through enemies to damage
            {
                c.SendMessage("TakeDamage", damage);    // Tells enemy to take damage
                BeamEnemy(c.transform.position);        // Draws beam to enemy
            }
        }
    }

    // Function to attach beam to enemies
    private void BeamEnemy(Vector3 enemy)
    {
        GameObject BeamHouse = new GameObject();                        // Creates empty object to house beam
        BeamHouse.transform.SetParent(gameObject.transform);            // Sets parent as tower
        LineRenderer beam = BeamHouse.AddComponent<LineRenderer>();     // Creates line renderer
        beam.material = beamMat;                                        // Assigns red material
        beam.startWidth = 0.2f;                                         // Defines width of beam at start
        beam.endWidth = 0.1f;                                           // Defines width of beam at end
        beam.positionCount = 2;                                         // Defines number of beam vertices

        beam.SetPosition(0, gameObject.transform.position);             // Define start of beam from tower
        beam.SetPosition(1, enemy);                                     // Define end of beam to enemy
        Destroy(BeamHouse, 0.5f);                                       // Destroy beam after time has passed
    }

    // Function to check for duplicated enemeis in a list
    private bool DetectDuplicateTarget(List<Collider> list, Collider target)
    {
        foreach (Collider c in list)    // Loops through list of enemies
        {
            if (c == target)            // Checks if each index is the enemy to check against
            {
                return true;            // If enemy exists in list, return true
            }
        }
        return false;                   // If enemy not in list, return false
    }
}
