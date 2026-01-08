using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// using UnityEngine.Rendering;


public enum State { Beam, Rapid, Scatter, ArmourPen }

public class MainTower : MonoBehaviour
{
    // Tower stats
    private State state;
    private float damage = 20f;
    private float fireRate = 4f;
    private float rapidFireRate = 4f;
    private float range = 300f;
    private float armourPen = 0f;
    private int scatterCount = 2;
    private int damageLevel = 1;
    private int armourPenLevel = 1;
    private int scatterLevel = 0;
    private float fireRateLevel = 1;
    private Material beamMat;

    private float lastShotTime = 0;
    private Collider currentTarget;

    public AudioClip laserSound;


    void Update()
    {   // Calls functions based on tower setting
        if ((state != State.Rapid && Time.time >= lastShotTime + fireRate) || (state == State.Rapid && Time.time >= lastShotTime + rapidFireRate))
        {
            switch (state)
            {
                case State.Beam:
                    BeamTower();
                    break;

                case State.Rapid:
                    RapidFireTower();
                    break;

                case State.Scatter:
                    ScatterTower();
                    break;

                case State.ArmourPen:
                    ArmourPenTower();
                    break;
            }
        }
    }


    private List<Collider> GetEnemiesInRange()
    {   // Method to get enemies in range
        Collider[] enemies = Physics.OverlapSphere(transform.position, range);  // Gets array of enemies in range
        List<Collider> enemiesList = enemies.ToList();                          // Converts array to list
        List<Collider> enemiesReturn = new List<Collider>();                    // Creates list to be returned
        if (enemiesList.Count > 0)                                              // Checks length of list
        {
            foreach (Collider enemy in enemiesList)
            {
                if (enemy.GetComponent<EnemyStats>() != null)               // Checks an object is an enemy
                {
                    var enemyObject = enemy.GetComponent<EnemyStats>();     // Gets the enemy stat component
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


    private Collider GetClosestEnemy(List<Collider> enemies)
    {   // Method to sort list by distance
        List<Collider> returnEnemies = enemies.OrderBy(enemy => (enemy.transform.position - transform.position).sqrMagnitude).ToList(); // Sorts list by distance from tower
        return returnEnemies[0];    // Returns first entry
    }


    private void BeamTower()
    {   // Method for beam tower
        if (GetEnemiesInRange() != null)
        {
            lastShotTime = Time.time;                                   // Updates last shot timer
            List<Collider> enemiesInRange = GetEnemiesInRange();        // Gets enemies in range
            Collider closestEnemy = GetClosestEnemy(enemiesInRange);    // Gets closest enemy to tower
            closestEnemy.SendMessage("TakeDamage", damage);             // Makes enemy take damage
            BeamEnemy(closestEnemy.transform.position);                 // Draws beam to enemy
        }
    }


    private void ArmourPenTower()
    {   // Method for beam tower
        if (GetEnemiesInRange() != null)
        {
            lastShotTime = Time.time;                                   // Updates last shot timer
            List<Collider> enemiesInRange = GetEnemiesInRange();        // Gets enemies in range
            Collider closestEnemy = GetClosestEnemy(enemiesInRange);    // Gets closest enemy to tower
            closestEnemy.SendMessage("TakeDamage", damage * armourPen);             // Makes enemy take damage
            BeamEnemy(closestEnemy.transform.position);                 // Draws beam to enemy
        }
    }


    private void RapidFireTower()
    {   // Method for rapid fire tower
        // Checks current target is dead or null
        if (currentTarget == null || currentTarget.GetComponent<TestEnemyStats>().IsDead())
        {
            List<Collider> enemiesInRange = GetEnemiesInRange();    // Gets enemies in range
            if (enemiesInRange != null)                             // Checks enemies are in range
            {
                currentTarget = GetClosestEnemy(enemiesInRange);        // Gets closest enemy to tower
            }
        }
        else
        {
            lastShotTime = Time.time;                           // Resets shoot timer
            currentTarget.SendMessage("TakeDamage", damage);    // Deals damage to enemy
            BeamEnemy(currentTarget.transform.position);        // Draws beam to enemy
        }
    }


    private void ScatterTower()
    {   // Method for scatter tower shooting
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


    private void BeamEnemy(Vector3 enemy)
    {   // Method for drawing beams to enemies
        GameObject BeamHouse = new GameObject();                        // Creates empty object to house beam
        BeamHouse.transform.SetParent(gameObject.transform);            // Sets parent as tower
        LineRenderer beam = BeamHouse.AddComponent<LineRenderer>();     // Creates line renderer
        beam.material = beamMat;                                        // Assigns red material
        beam.startWidth = 0.2f;                                         // Defines width of beam at start
        beam.endWidth = 0.1f;                                           // Defines width of beam at end
        beam.positionCount = 2;                                         // Defines number of beam vertices

        beam.SetPosition(0, gameObject.transform.position);             // Define start of beam from tower
        beam.SetPosition(1, enemy);                                     // Define end of beam to enemy
        AudioManager.Instance.PlaySFX(laserSound);                      // Play laser sound
        Destroy(BeamHouse, 0.3f);                                       // Destroy beam after time has passed
    }


    private bool DetectDuplicateTarget(List<Collider> list, Collider target)
    {   // Method to check for duplicated enemeis in a list
        foreach (Collider c in list)    // Loops through list of enemies
        {
            if (c == target)            // Checks if each index is the enemy to check against
            {
                return true;            // If enemy exists in list, return true
            }
        }
        return false;                   // If enemy not in list, return false
    }


    public void ModifyTowerState(State type)
    {   // Changes tower states
        state = type;
        GetTowersStats();
    }


    public void GetTowersStats()
    {   // Modifies tower stats based on values from GameManager
        int[] towerLevels = GameManager.Instance.GetTowerStats();

        scatterLevel = towerLevels[0];
        scatterCount = (int)(Mathf.Floor(scatterLevel / 5f + 1.8f));

        fireRateLevel = towerLevels[1];
        rapidFireRate = 2 * (2 / Mathf.Pow(fireRateLevel, 6 / 7)) + 0.2f;

        armourPenLevel = towerLevels[2];
        armourPen = 0.12f * armourPenLevel + 1.1f;

        damageLevel = towerLevels[3];
        damage = 10 * Mathf.Log(damageLevel) + 15;
    }

}
