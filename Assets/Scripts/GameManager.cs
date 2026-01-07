using NUnit.Framework.Constraints;
using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Unity.VisualScripting;
using Unity.Mathematics;


public enum GameStates { BuildPhase, FightPhase };

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        // If there is no instance yet, this is the one
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // If one already exists, destroy this duplicate
            Destroy(gameObject);
        }
    }

    // Game States
    private GameStates currentState;

    // Wave Metrics
    [SerializeField] private int currentWave;
    [SerializeField] private int waveCost;
    [SerializeField] private float waveTimer;
    [SerializeField] private int playerMoney;

    // Wave directions
    [SerializeField] private bool westEnabled;
    [SerializeField] private bool northEnabled;
    [SerializeField] private bool eastEnabled;

    // Global Tower Levels
    [SerializeField] private int scatterLevel = 1;
    [SerializeField] private int rapidFireLevel = 1;
    [SerializeField] private int apLevel = 1;
    [SerializeField] private int damageLevel = 1;

    // Enemy metrics
    [SerializeField] private bool gruntEnabled = false;
    [SerializeField] private int gruntCost = 3;
    [SerializeField] private int gruntUnlockWave = 1;

    [SerializeField] private bool swarmerEnabled = false;
    [SerializeField] private int swarmerCost = 1;
    [SerializeField] private int swarmerUnlockWave = 2;

    [SerializeField] private bool eliteEnabled = false;
    [SerializeField] private int eliteCost = 8;
    [SerializeField] private int eliteUnlockWave = 3;

    [SerializeField] private bool tankEnabled = false;
    [SerializeField] private int tankCost = 15;
    [SerializeField] private int tankUnlockWave = 5;

    [SerializeField] private bool bossEnabled = false;
    [SerializeField] private int bossCost = 20;
    [SerializeField] private int bossUnlockWave = 10;

    // Variables for enemy spawning
    [SerializeField] private GameObject enemyBase;
    private int enemyCount = 0;
    int gruntCount = 0;
    int swarmerCount = 0;
    int elitecount = 0;
    int tankCount = 0;
    int bossCount = 0;
    int remainder = 0;
    private float lastSpawnTime = 0f;
    private float nextSpawnDelay = 3f;
    private float minSpawnDelay = 2.5f;
    private float maxSpawnDelay = 4f;
    private Vector3 cenSpwn1 = new Vector3(175, 2, 3);
    private Vector3 cenSpwn2 = new Vector3(175, 2, 1.5f);
    private Vector3 cenSpwn3 = new Vector3(175, 2, 0);
    private Vector3 cenSpwn4 = new Vector3(175, 2, -1.5f);
    private Vector3 cenSpwn5 = new Vector3(175, 2, -3f);

    private void Start()
    {   // Function for defining base game state
        currentState = GameStates.BuildPhase;
        playerMoney = 50;
        northEnabled = true;
        currentWave = 1;
        CalculateSpawnDelays();
    }

    private void Update()
    {
        if (currentState == GameStates.FightPhase && Time.time >= lastSpawnTime + nextSpawnDelay)
        {

        }
    }


    private void FlipGameState()
    {   // Function for flipping game state
        switch (currentState)
        {
            case GameStates.BuildPhase:
                currentState = GameStates.FightPhase;
                CreateWave();
                break;
            case GameStates.FightPhase:
                currentState = GameStates.BuildPhase;
                break;
        }
    }

    private void CalculateSpawnDelays()
    {
        minSpawnDelay = (-0.1f * currentWave) + 2.6f;
        if (minSpawnDelay < 0.6f)
        {
            minSpawnDelay = 0.6f;
        }
        maxSpawnDelay = (-0.05f * currentWave) + 4.05f;
        if (maxSpawnDelay < 2)
        {
            maxSpawnDelay = 2;
        }
    }

    public int[] GetTowerStats()
    {   // Function for getting levels of towers
        int[] stats = new int[4];
        stats[0] = scatterLevel;
        stats[1] = rapidFireLevel;
        stats[2] = apLevel;
        stats[3] = damageLevel;
        return stats;
    }

    public int GetCurrentWave()
    {   // Returns current wave
        return currentWave;
    }

    public void SetCurrentWave(int i)
    {   // Debug for setting wave
        currentWave = i;
    }

    public void ReduceEnemyCount()
    {
        enemyCount--;
    }

    public int GetEnemyCount()
    {
        return enemyCount;
    }

    public void WaveIncrement()
    {   // Increments wave count and runs between wave functions
        currentWave++;
        CheckEnemyUnlock();
    }

    public int GetPlayerMoney()
    {   // Returns player money
        return playerMoney;
    }

    public void SetPlayerMoney(int i)
    {   // Debug for setting players money
        playerMoney = i;
    }

    public void ModPlayerMoney(int i)
    {   // Modifies player money
        playerMoney += i;
    }

    private void CalculateWaveCost()
    {   // Simple linear function for wave costs
        waveCost = currentWave * 20 + 10;
    }

    private void CheckEnemyUnlock()
    {   // Checks which enemies should be unlocked for the current wave
            // Could use (currentWave % x) for spawning every x waves
        if (currentWave >= gruntUnlockWave)
        {
            gruntEnabled = true;
        }
        if (currentWave >= swarmerUnlockWave)
        {
            swarmerEnabled = true;
        }
        if (currentWave >= eliteUnlockWave)
        {
            eliteEnabled = true;
        }
        if (currentWave >= tankUnlockWave)
        {
            tankEnabled = true;
        }
        if (currentWave >= bossUnlockWave)
        {  
            bossEnabled = true;
        }
    }

    public bool CanUpgrade(int value)
    {   // Function for checking the player can afford an upgrade
        if (playerMoney >= value)
        {
            return true;
        } return false;
    }

    private bool[] GetActiveEnemies()
    {   // Method for calculating which enemies should be spawned on a given wave
        bool[] returnArray = new bool[4];
        if (currentWave % swarmerUnlockWave == 0)
        {
            returnArray[0] = true;
        }
        else returnArray[0] = false;

        if (currentWave % eliteUnlockWave == 0)
        {
            returnArray[1] = true;
        }
        else returnArray[1] = false;

        if (currentWave % tankUnlockWave == 0)
        {
            returnArray[2] = true;
        }
        else returnArray[2] = false;

        if (currentWave % bossUnlockWave == 0)
        {
            returnArray[3] = true;
        }
        else returnArray[3] = false;

        return returnArray;
    }

    private void CreateWave()
    {   // Method to create wave and allocate the amount of each enemy to spawn in a given wave
        CalculateWaveCost();
        bool[] waveEnemies = GetActiveEnemies();
        int enemyTypes = 1;
        foreach (bool b in waveEnemies)
        {
            if (b == true)
            {
                enemyTypes++;
            }
        }
        float waveSplit = waveCost/ (float)enemyTypes;
        gruntCount = 0;
        swarmerCount = 0;
        elitecount = 0;
        tankCount = 0;
        bossCount = 0;
        remainder = 0;

        gruntCount = (int)Mathf.Floor(waveSplit / gruntCost);
        remainder += (int)waveSplit % gruntCost;
        if (waveEnemies[0] == true)
        {
            swarmerCount = (int)Mathf.Floor(waveSplit / swarmerCost);
            remainder += (int)waveSplit % swarmerCost;
        }
        if (waveEnemies[1] == true)
        {
            elitecount = (int)Mathf.Floor(waveSplit / eliteCost);
            remainder += (int)waveSplit % eliteCost;
        }
        if (waveEnemies[2] == true)
        {
            tankCount = (int)Mathf.Floor(waveSplit / tankCost);
            remainder += (int)waveSplit % tankCost;
        }
        if (waveEnemies[3] == true)
        {
            bossCount = (int)Mathf.Floor(waveSplit / bossCost);
            remainder += (int)waveSplit % bossCost;
        }

        swarmerCount += remainder;
        enemyCount = gruntCount + swarmerCount + elitecount + tankCount + bossCount + remainder;
    }

    private Vector3 GetSpawnLoc()
    {
        int i = UnityEngine.Random.Range(0, 4);
        switch (i)
        {
            case 0:
                return cenSpwn1;
            case 1:
                return cenSpwn2;
            case 2:
                return cenSpwn3;
            case 3:
                return cenSpwn4;
            case 4:
                return cenSpwn5;
        }
        return cenSpwn3;
    }

    private void SetSpawnTimers()
    {
        lastSpawnTime = Time.time;
        nextSpawnDelay = UnityEngine.Random.Range(minSpawnDelay,maxSpawnDelay);
    }

    private void SpawnGrunt()
    {
        GameObject grunt = Instantiate(enemyBase);
        grunt.transform.localPosition = GetSpawnLoc();
        grunt.AddComponent<EnemyStats>();
        grunt.SendMessage("ModifyEnemyType", enemyType.Grunt);
    }

    private void SpawnSwarmer()
    {
        GameObject swarmer = Instantiate(enemyBase);
        swarmer.transform.localPosition = GetSpawnLoc();
        swarmer.AddComponent<EnemyStats>();
        swarmer.SendMessage("ModifyEnemyType", enemyType.Swarmer);
    }

    private void SpawnElite()
    {
        GameObject elite = Instantiate(enemyBase);
        elite.transform.localPosition = GetSpawnLoc();
        elite.AddComponent<EnemyStats>();
        elite.SendMessage("ModifyEnemyType", enemyType.Elite);
    }

    private void SpawnTank()
    {
        GameObject tank = Instantiate(enemyBase);
        tank.transform.localPosition = cenSpwn3;
        tank.AddComponent<EnemyStats>();
        tank.SendMessage("ModifyEnemyType", enemyType.Tank);
    }

    private void SpawnBoss()
    {
        GameObject boss = Instantiate(enemyBase);
        boss.transform.localPosition = cenSpwn3;
        boss.AddComponent<EnemyStats>();
        boss.SendMessage("ModifyEnemyType", enemyType.Boss);
    }
}
