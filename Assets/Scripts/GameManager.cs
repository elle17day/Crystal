using NUnit.Framework.Constraints;
using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

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
    public enum GameStates {BuildPhase, FightPhase};
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
    private GameObject[] gruntEnemyArray;
    private GameObject[] swarmerEnemyArray;
    private GameObject[] eliteEnemyArray;
    private GameObject[] tankEnemyArray;
    private GameObject[] bossEnemyArray;
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


    private void FlipGameState()
    {   // Function for flipping game state
        switch (currentState)
        {
            case GameStates.BuildPhase:
                currentState = GameStates.FightPhase;
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

    public void WaveIncrement()
    {   // Increments wave count and runs between wave functions
        currentWave++;
        checkEnemyUnlock();
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

    private void checkEnemyUnlock()
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

    private void CreateWaveBase()
    {
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
        int gruntCount = 0;
        int swarmerCount = 0;
        int elitecount = 0;
        int tankCount = 0;
        int bossCount = 0;
        int remainder = 0;

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

    }
}
