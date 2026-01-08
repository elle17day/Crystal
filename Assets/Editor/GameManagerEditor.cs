using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{

    SerializedProperty currentWaveProp;
    SerializedProperty waveCostProp;
    SerializedProperty waveTimerProp;
    SerializedProperty playerMoneyProp;

    SerializedProperty scatterLevelProp;
    SerializedProperty rapidFireLevelProp;
    SerializedProperty apLevelProp;
    SerializedProperty damageLevelProp;

    SerializedProperty gruntEnabledProp;
    SerializedProperty gruntCostProp;
    SerializedProperty gruntUnlockWaveProp;

    SerializedProperty swarmerEnabledProp;
    SerializedProperty swarmerCostProp;
    SerializedProperty swarmerUnlockWaveProp;

    SerializedProperty eliteEnabledProp;
    SerializedProperty eliteCostProp;
    SerializedProperty eliteUnlockWaveProp;

    SerializedProperty tankEnabledProp;
    SerializedProperty tankCostProp;
    SerializedProperty tankUnlockWaveProp;

    SerializedProperty bossEnabledProp;
    SerializedProperty bossCostProp;
    SerializedProperty bossUnlockWaveProp;

    public override void OnInspectorGUI()
    {   // Draws inspector
        DrawDefaultInspector();

        // Reference target script
        GameManager gameManager = (GameManager)target;


    }
}
