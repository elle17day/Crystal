using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    SerializedProperty currentWaveProp;
    SerializedProperty waveCostProp;
    SerializedProperty playerMoneyProp;

    SerializedProperty scatterLevelProp;
    SerializedProperty rapidFireLevelProp;
    SerializedProperty apLevelProp;
    SerializedProperty damageLevelProp;

    SerializedProperty gruntEnabledProp;
    SerializedProperty gruntCostProp;

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
    {   // Finds values for all serialized values
        currentWaveProp = serializedObject.FindProperty("currentWave");
        waveCostProp = serializedObject.FindProperty("waveCost");
        playerMoneyProp = serializedObject.FindProperty("playerMoney");

        scatterLevelProp = serializedObject.FindProperty("scatterLevel");
        rapidFireLevelProp = serializedObject.FindProperty("rapidFireLevel");
        apLevelProp = serializedObject.FindProperty("apLevel");
        damageLevelProp = serializedObject.FindProperty("damageLevel");

        gruntEnabledProp = serializedObject.FindProperty("gruntEnabled");
        gruntCostProp = serializedObject.FindProperty("gruntCost");

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

        // Tool title
        EditorGUILayout.LabelField("Game configuration tool", EditorStyles.boldLabel);

        EditorGUILayout.Space();

        // Control current wave at start of game
        EditorGUILayout.LabelField("Wave/Player Properties", EditorStyles.boldLabel);

        EditorGUILayout.IntSlider(currentWaveProp, 1, 10, new GUIContent("Current Wave"));

        EditorGUILayout.PropertyField(playerMoneyProp, new GUIContent("Player Money"));

        if (GUILayout.Button("Set to default"))
        {
            currentWaveProp.intValue = 1;
            playerMoneyProp.intValue = 0;
        }

        EditorGUILayout.Space();

        // Control tower levels at start of game
        EditorGUILayout.LabelField("Tower Properties", EditorStyles.boldLabel);

        EditorGUILayout.IntSlider(scatterLevelProp, 1, 20, new GUIContent("Scatter Level"));
        EditorGUILayout.IntSlider(rapidFireLevelProp, 1, 20, new GUIContent("Repeater Level"));
        EditorGUILayout.IntSlider(apLevelProp, 1, 20, new GUIContent("AP Level"));
        EditorGUILayout.IntSlider(damageLevelProp, 1, 20, new GUIContent("Damage Level"));

        if (GUILayout.Button("Set levels to 1"))
        {   // Button to set all levels to 1
            scatterLevelProp.intValue = 1;
            rapidFireLevelProp.intValue = 1;
            apLevelProp.intValue = 1;
            damageLevelProp.intValue = 1;
        }

        if (GUILayout.Button("Set levels to 10"))
        {   // Button to set all levels to 10
            scatterLevelProp.intValue = 10;
            rapidFireLevelProp.intValue = 10;
            apLevelProp.intValue = 10;
            damageLevelProp.intValue = 10;
        }

        if (GUILayout.Button("Set levels to 20"))
        {   // Button to set all levels to 20
            scatterLevelProp.intValue = 20;
            rapidFireLevelProp.intValue = 20;
            apLevelProp.intValue = 20;
            damageLevelProp.intValue = 20;
        }

        EditorGUILayout.Space();

        // Control Grunt metrics
        EditorGUILayout.LabelField("Grunt Properties", EditorStyles.boldLabel);

        EditorGUILayout.IntSlider(gruntCostProp, 1, 10, new GUIContent("Grunt cost"));

        if (GUILayout.Button("Restore defaults"))
        {
            gruntCostProp.intValue = 3;
        }

        EditorGUILayout.Space();

        // Control Swarmer metrics
        EditorGUILayout.LabelField("Swarmer Properties", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(swarmerEnabledProp, new GUIContent("Swarmer enabled"));
        EditorGUILayout.IntSlider(swarmerCostProp, 1, 10, new GUIContent("Swarmer cost"));
        EditorGUILayout.IntSlider(swarmerUnlockWaveProp, 1, 10, new GUIContent("Swarmer unlock wave"));

        if (GUILayout.Button("Restore defaults"))
        {
            swarmerEnabledProp.boolValue = false;
            swarmerCostProp.intValue = 1;
            swarmerUnlockWaveProp.intValue = 2;
        }

        EditorGUILayout.Space();

        // Control Elite metrics
        EditorGUILayout.LabelField("Elite Properties", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(eliteEnabledProp, new GUIContent("Elite enabled"));
        EditorGUILayout.IntSlider(eliteCostProp, 1, 10, new GUIContent("Elite cost"));
        EditorGUILayout.IntSlider(eliteUnlockWaveProp, 1, 10, new GUIContent("Elite unlock wave"));

        if (GUILayout.Button("Restore defaults"))
        {
            eliteEnabledProp.boolValue = false;
            eliteCostProp.intValue = 8;
            eliteUnlockWaveProp.intValue = 3;
        }

        EditorGUILayout.Space();

        // Control Tank metrics
        EditorGUILayout.LabelField("Tank Properties", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(tankEnabledProp, new GUIContent("Tank enabled"));
        EditorGUILayout.IntSlider(tankCostProp, 1, 20, new GUIContent("Tank cost"));
        EditorGUILayout.IntSlider(tankUnlockWaveProp, 1, 10, new GUIContent("Tank unlock wave"));

        if (GUILayout.Button("Restore defaults"))
        {
            tankEnabledProp.boolValue = false;
            tankCostProp.intValue = 15;
            tankUnlockWaveProp.intValue = 5;
        }

        EditorGUILayout.Space();

        // Control Boss metrics
        EditorGUILayout.LabelField("Boss Properties", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(bossEnabledProp, new GUIContent("Boss enabled"));
        EditorGUILayout.IntSlider(bossCostProp, 1, 30, new GUIContent("Boss cost"));
        EditorGUILayout.IntSlider(bossUnlockWaveProp, 1, 10, new GUIContent("Boss unlock wave"));

        if (GUILayout.Button("Restore defaults"))
        {
            bossEnabledProp.boolValue = false;
            bossCostProp.intValue = 20;
            bossUnlockWaveProp.intValue = 10;
        }

        EditorGUILayout.Space();



        serializedObject.ApplyModifiedProperties();
    }
}
