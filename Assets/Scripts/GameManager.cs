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
    [SerializeField] private int gruntCost = 5;
    [SerializeField] private int gruntUnlockWave = 1;

    [SerializeField] private bool swarmerEnabled = false;
    [SerializeField] private int swarmerCost = 1;
    [SerializeField] private int swarmerUnlockWave = 2;

    [SerializeField] private bool eliteEnabled = false;
    [SerializeField] private int eliteCost = 8;
    [SerializeField] private int eliteUnlockWave = 4;

    [SerializeField] private bool tankEnabled = false;
    [SerializeField] private int tankCost = 15;
    [SerializeField] private int tankUnlockWave = 5;

    [SerializeField] private bool siegeEnabled = false;
    [SerializeField] private int siegeCost = 10;
    [SerializeField] private int siegeUnlockWave = 7;

    [SerializeField] private bool stealthEnabled = false;
    [SerializeField] private int stealthCost = 8;
    [SerializeField] private int stealthUnlockWave = 8;

    [SerializeField] private bool bossEnabled = false;
    [SerializeField] private int bossCost = 20;
    [SerializeField] private int bossUnlockWave = 10;


    private void Start()
    {   // Function for defining base game state
        currentState = GameStates.BuildPhase;
        playerMoney = 50;
        northEnabled = true;
        currentWave = 1;
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
        waveCost = currentWave * 15 + 10;
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
        if (currentWave >= siegeUnlockWave)
        {
            siegeEnabled = true;
        }
        if (currentWave >= siegeUnlockWave)
        {
            siegeEnabled = true;
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
}
