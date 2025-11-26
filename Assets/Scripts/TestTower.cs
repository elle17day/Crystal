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
        if (Time.time >= lastShotTime + fireRate)
        {
            ShootClosest();
        }
    }

    void ShootClosest()
    {
        lastShotTime = Time.time;
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, range);
        var listEnemies = enemiesInRange.ToList();
        var actEnemies = listEnemies.ToList();
        actEnemies.Clear();
        foreach (var enemy in listEnemies) 
        {
            if (enemy.GetComponent<TestEnemyStats>() != null )
            {
                Debug.Log(enemy.transform.name);
                actEnemies.Add(enemy);
            }
            if (enemy.GetComponent<TestEnemyStats>() == null )
            {
                Debug.Log(enemy.transform.name + " popped as not enemy!");
            }
        }
        actEnemies[0].SendMessage("TakeDamage",damage);
        Debug.Log("Enemy" + actEnemies[0].transform.name + " taking " +  damage + " damage.");
        BeamEnemy(gameObject.transform.position, actEnemies[0].transform.position);
    }

    void BeamEnemy(Vector3 tower, Vector3 enemy)
    {
        LineRenderer beam = gameObject.AddComponent<LineRenderer>();
        beam.material = beamMat;
        beam.startWidth = 0.2f;
        beam.endWidth = 0.2f;
        beam.positionCount = 2;

        beam.SetPosition(0, tower);
        beam.SetPosition(1, enemy);
        Destroy(beam, 0.5f);
    }

}
