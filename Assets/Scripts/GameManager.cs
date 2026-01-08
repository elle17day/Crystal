// using NUnit.Framework.Constraints;
//using System;
using UnityEngine;
//using UnityEngine.Events;
// using System.Collections;
// using Unity.VisualScripting;
//using Unity.Mathematics;


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
    private float waveStartTime = 0;
    private int enemyCount = 0;
    int gruntCount = 0;
    int gruntSpawned = 0;
    int swarmerCount = 0;
    int swarmerSpawned = 0;
    int eliteCount = 0;
    int eliteSpawned = 0;
    int tankCount = 0;
    int tankSpawned = 0;
    int bossCount = 0;
    int bossSpawned = 0;
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
        FlipGameState();
    }

    private void Update()
    {   // Checks game phase and sufficient time has passed between spawns
        if (currentState == GameStates.FightPhase && Time.time >= lastSpawnTime + nextSpawnDelay)
        {   // Switch for spawning enemies
            enemyType nextSpawn = (enemyType)UnityEngine.Random.Range(0, 5);
            switch (nextSpawn) 
            {
                case enemyType.Grunt:
                    if (gruntSpawned < gruntCount)
                    {
                        SpawnGrunt();
                        SetSpawnTimers();
                    }
                    break;
                case enemyType.Swarmer:
                    if (swarmerSpawned < swarmerCount)
                    {
                        SpawnSwarmer();
                        SetSpawnTimers();
                    }
                    break;
                case enemyType.Elite:
                    if (eliteSpawned < eliteCount)
                    {
                        SpawnElite();
                        SetSpawnTimers();
                    }
                    break;
                case enemyType.Tank:
                    if (tankSpawned < tankCount)
                    {
                        SpawnTank();
                        SetSpawnTimers();
                    }
                    break;
                case enemyType.Boss:
                    if (bossSpawned < bossCount)
                    {
                        SpawnBoss();
                        SetSpawnTimers();
                    }
                    break;
            }
        }

        if (currentState == GameStates.FightPhase && enemyCount <= 0 && Time.time >= waveStartTime + 30f)
        {   // Checks wave has successfully been defeated
            // Make wave survived screen to display here
            FlipGameState();
            WaveIncrement();
            Debug.Log(currentWave);
        }
    }


    private void FlipGameState()
    {   // Function for flipping game state
        switch (currentState)
        {
            case GameStates.BuildPhase:
                currentState = GameStates.FightPhase;
                waveStartTime = Time.time;
                CreateWave();
                Debug.Log("Now: " + currentState);
                break;
            case GameStates.FightPhase:
                currentState = GameStates.BuildPhase;
                Debug.Log("Now: " + currentState);
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

    public void IncrementScatter()
    {   // Increments scatter level
        scatterLevel++;
    }

    public void IncrementRapidFire()
    {   // Increments rapid fire level
        rapidFireLevel++;
    }

    public void IncrementAP()
    {   // Increments AP level
        apLevel++;
    }

    public void IncrementDamage()
    {   // Increments damage level
        damageLevel++;
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
        Debug.Log("Grunt active");
        if (currentWave % swarmerUnlockWave == 0)
        {
            Debug.Log("Swarmer active");
            returnArray[0] = true;
        }
        else returnArray[0] = false;

        if (currentWave % eliteUnlockWave == 0)
        {
            Debug.Log("Elite active");
            returnArray[1] = true;
        }
        else returnArray[1] = false;

        if (currentWave % tankUnlockWave == 0)
        {
            Debug.Log("Tank active");
            returnArray[2] = true;
        }
        else returnArray[2] = false;

        if (currentWave % bossUnlockWave == 0)
        {
            Debug.Log("Boss active");
            returnArray[3] = true;
        }
        else returnArray[3] = false;

        return returnArray;
    }

    private void CreateWave()
    {   // Method to create wave and allocate the amount of each enemy to spawn in a given wave
        CalculateWaveCost();                        // Calculates cost of wave
        bool[] waveEnemies = GetActiveEnemies();    // Creates array of available enemy types
        int enemyTypes = 1;                         // Sets default value to 1 because of grunts
        foreach (bool b in waveEnemies)
        {   // Counts how many enemy types are available for the wave
            if (b == true)
            {
                enemyTypes++;
                Debug.Log(b);
            }
        }
        float waveSplit = waveCost/ (float)enemyTypes;  // Splits the wave cost between every enemy type

        // Resets enemy counters to 0
        gruntCount = 0;    
        swarmerCount = 0;
        eliteCount = 0;
        tankCount = 0;
        bossCount = 0;
        remainder = 0;

        // Always works out grunt counts and adds remainder to be spawned as swarmers
        gruntCount = (int)Mathf.Floor(waveSplit / gruntCost);
        Debug.Log("Grunts this wave: " + gruntCount);
        remainder += (int)waveSplit % gruntCost;

        if (waveEnemies[0] == true)
        {   // If swarmer wave, works out amount of swarmers to spawn
            swarmerCount = (int)Mathf.Floor(waveSplit / swarmerCost);
            Debug.Log("Swarmers this wave: " +  swarmerCount);
            remainder += (int)waveSplit % swarmerCost;
        }
        if (waveEnemies[1] == true)
        {   // If elite wave, works out amount of elites to spawn
            eliteCount = (int)Mathf.Floor(waveSplit / eliteCost);
            Debug.Log("Elite this wave: " + eliteCount);
            remainder += (int)waveSplit % eliteCost;
        }
        if (waveEnemies[2] == true)
        {   // If tank wave, works out amount of tanks to spawn
            tankCount = (int)Mathf.Floor(waveSplit / tankCost);
            Debug.Log("Tank this wave: " + tankCount);
            remainder += (int)waveSplit % tankCost;
        }
        if (waveEnemies[3] == true)
        {   // If boss wave, works out amount of bosses to spawn
            bossCount = (int)Mathf.Floor(waveSplit / bossCost);
            Debug.Log("Boss this wave: " + bossCount);
            remainder += (int)waveSplit % bossCost;
        }

        // Adds remainder to swarmer count
        swarmerCount += remainder;
        // Sets amount of enemies in the wave to work out when the wave has finished
        enemyCount = gruntCount + swarmerCount + eliteCount + tankCount + bossCount;
    }

    private Vector3 GetSpawnLoc()
    {   // Randomly selects a spawn location for small enemies
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
    {   // Resets spawn timers for variation in spawn pattern
        lastSpawnTime = Time.time;
        nextSpawnDelay = UnityEngine.Random.Range(minSpawnDelay,maxSpawnDelay);
    }

    private void SpawnGrunt()
    {   // Spawns the grunt enemy in the world
        GameObject grunt = Instantiate(enemyBase);
        grunt.transform.localPosition = GetSpawnLoc();
        grunt.AddComponent<EnemyStats>();
        grunt.SendMessage("ModifyEnemyType", enemyType.Grunt);
        gruntSpawned++;
    }

    private void SpawnSwarmer()
    {   // Spawns the swarmer enemy in the world
        GameObject swarmer = Instantiate(enemyBase);
        swarmer.transform.localPosition = GetSpawnLoc();
        swarmer.AddComponent<EnemyStats>();
        swarmer.SendMessage("ModifyEnemyType", enemyType.Swarmer);
        swarmerSpawned++;
    }

    private void SpawnElite()
    {   // Spawns the elite enemy in the world
        GameObject elite = Instantiate(enemyBase);
        elite.transform.localPosition = GetSpawnLoc();
        elite.AddComponent<EnemyStats>();
        elite.SendMessage("ModifyEnemyType", enemyType.Elite);
        eliteSpawned++;
    }

    private void SpawnTank()
    {   // Spawns the tank enemy in the world
        GameObject tank = Instantiate(enemyBase);
        tank.transform.localPosition = cenSpwn3;
        tank.AddComponent<EnemyStats>();
        tank.SendMessage("ModifyEnemyType", enemyType.Tank);
        tankSpawned++;
    }

    private void SpawnBoss()
    {   // Spawns the boss enemy in the world
        GameObject boss = Instantiate(enemyBase);
        boss.transform.localPosition = cenSpwn3;
        boss.AddComponent<EnemyStats>();
        boss.SendMessage("ModifyEnemyType", enemyType.Boss);
        bossSpawned++;
    }
}
