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

    private void OnEnable()
    {
        currentWaveProp = serializedObject.FindProperty("currentWave");
        waveCostProp = serializedObject.FindProperty("waveCost");
        waveTimerProp = serializedObject.FindProperty("waveTimer");
        playerMoneyProp = serializedObject.FindProperty("playerMoney");

        scatterLevelProp = serializedObject.FindProperty("scatterLevel");
        rapidFireLevelProp = serializedObject.FindProperty("rapidFireLevel");
        apLevelProp = serializedObject.FindProperty("apLevel");
        damageLevelProp = serializedObject.FindProperty("damageLevel");

        gruntEnabledProp = serializedObject.FindProperty("gruntEnabled");
        gruntCostProp = serializedObject.FindProperty("gruntCost");
        gruntUnlockWaveProp = serializedObject.FindProperty("gruntUnlockWave");

        swarmerEnabledProp = serializedObject.FindProperty("swarmerEnabled");
        swarmerCostProp = serializedObject.FindProperty("swarmerCost");
        swarmerUnlockWaveProp = serializedObject.FindProperty("swarmerUnlockWave");

        eliteEnabledProp = serializedObject.FindProperty("eliteEnabled");
        eliteCostProp = serializedObject.FindProperty("eliteCost");
        eliteUnlockWaveProp = serializedObject.FindProperty("eliteUnlockWave");

        tankEnabledProp = serializedObject.FindProperty("tankEnabled");
        tankCostProp = serializedObject.FindProperty("tankCost");
        tankUnlockWaveProp = serializedObject.FindProperty("tankUnlockWave");

        bossEnabledProp = serializedObject.FindProperty("bossEnabled");
        bossCostProp = serializedObject.FindProperty("bossCost");
        bossUnlockWaveProp = serializedObject.FindProperty("bossUnlockWave");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Reference target script
        GameManager gameManager = (GameManager)target;

        EditorGUILayout.LabelField("Game configuration tool", EditorStyles.boldLabel);


    }
}
