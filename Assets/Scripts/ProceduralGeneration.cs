using UnityEngine;
using UnityEditor;
using System;
using Unity.Mathematics;
using JetBrains.Annotations;
using Random = UnityEngine.Random;
using System.Runtime.CompilerServices;
using UnityEditor.SceneManagement;
using Unity.VisualScripting;

public class ProceduralGeneration : EditorWindow
{
    private Texture2D noiseMapTexture;
    private GameObject prefab;
    private PlacementGenes genes;

    private static string GenesSaveName
    {
        get { return $"Vegetationwizard_{Application.productName}_{EditorSceneManager.GetActiveScene().name}"; }
    }

    // Showing of initial window
    [MenuItem("Tools/Wizards/Plant Placement")]
    public static void ShowWindow()
    {
        GetWindow<ProceduralGeneration>("Plant Placement");
    }

    private void OnEnable()
    {
        genes = PlacementGenes.Load(GenesSaveName);
    }

    private void OnDisable()
    {
        PlacementGenes.Save(GenesSaveName, genes);
    }

    // GUI Creation

    // Creation of the noise texture as the base location for x, y of where the trees are placed
    private void OnGUI()
    {
        if (Terrain.activeTerrain == null)
        {
            EditorGUILayout.HelpBox("No active terrain found in the scene!", MessageType.Error);
            return;
        }

        EditorGUILayout.BeginHorizontal();
        noiseMapTexture = (Texture2D)EditorGUILayout.ObjectField("Noise Map Texture", noiseMapTexture, typeof(Texture2D), false);
        if (GUILayout.Button("Generate Noise"))
        {
            int width = (int)Terrain.activeTerrain.terrainData.size.x;
            int height = (int)Terrain.activeTerrain.terrainData.size.y;
            float scale = 5;
            noiseMapTexture = Noise.GetNoiseMap(width, height, scale);
        }
        EditorGUILayout.EndHorizontal();

        genes.maxHeight = EditorGUILayout.Slider("Max Height", genes.maxHeight, 0, 1000);
        genes.maxSteepness = EditorGUILayout.Slider("Max Steepness", genes.maxSteepness, 0, 90);
        genes.spacing = EditorGUILayout.Slider("Spacing", genes.spacing, 0, 1);

        // Slider to allow the user to select the density of the trees
        genes.density = EditorGUILayout.Slider("Density", genes.density, 0, 1);

        // Area to allow the user to select a prefab
        prefab = (GameObject)EditorGUILayout.ObjectField("Object Prefab", prefab, typeof(GameObject), false);

        if (GUILayout.Button("Place Objects"))
        {
            PlaceObjects(Terrain.activeTerrain, noiseMapTexture, genes, prefab);
        }

    }

    // REFACTOR: provide a fitness param struct for objects, to minimise aamount of data being passed through methods
    // Object place algorithm (Managed how the prefabs are placed in the world)
    public static void PlaceObjects(Terrain terrain, Texture2D noiseMapTexture, PlacementGenes genes, GameObject prefab)
    {
        Transform parent = new GameObject("PlacedObjects").transform;

        int heightRes = terrain.terrainData.heightmapResolution;
        int widthRes = heightRes;   // terrain heightmap is square

        for (int x = 0; x < widthRes; x++)
        {
            for (int z = 0; z < widthRes; z++)
            {

                // Allows the trees to be spaced out to prevent clumping
                if (Random.value > genes.spacing)
                    continue;

                if (Fitness(terrain, noiseMapTexture, genes.maxHeight, genes.maxSteepness, x, z) > 1 - genes.density)
                {
                    // Convert heightmap coordinates to normalised terrain coordinates
                    float normX = x / (float)heightRes;
                    float normZ = z / (float)heightRes;

                    // Convert to world space
                    float worldX = terrain.transform.position.x + normX * terrain.terrainData.size.x;
                    float worldZ = terrain.transform.position.z + normZ * terrain.terrainData.size.z;
                    float worldY = terrain.terrainData.GetInterpolatedHeight(normX, normZ);

                    Vector3 pos = new Vector3(worldX, worldY, worldZ);

                    GameObject go = GameObject.Instantiate(prefab, pos, Quaternion.identity);
                    go.transform.SetParent(parent);
                }
            }
        }
    }

    private static float Fitness(Terrain terrain, Texture2D noiseMapTexture, float maxHeight, float maxSteepness, int x, int z)
    {
        if (noiseMapTexture == null)
        {
            EditorGUILayout.HelpBox("Error, You must generate terrain before placing trees", MessageType.Error);
            return -999f;
        }
        else
        {

            float fitness = noiseMapTexture.GetPixel(x, z).g;

            fitness += Random.Range(-0.25f, 0.25f);

            float steepness = terrain.terrainData.GetSteepness(x / terrain.terrainData.size.x, z / terrain.terrainData.size.z);
            if (steepness > maxSteepness)
            {
                fitness -= 0.7f;
            }

            float height = terrain.terrainData.GetHeight(x, z);
            if (height > maxHeight)
            {
                fitness -= 0.7f;
            }

            return fitness;
        }
    }

    [Serializable]
    public struct PlacementGenes
    {
        public float spacing;
        public float density;
        public float maxHeight;
        public float maxSteepness;

        internal static PlacementGenes Load(string saveName)
        {
            PlacementGenes genes;
            string saveData = EditorPrefs.GetString(saveName);

            if (string.IsNullOrEmpty(saveData))
            {
                genes = new PlacementGenes();
                genes.density = 0.5f;
                genes.maxHeight = 100;
                genes.maxSteepness = 25;
                genes.spacing = 0.5f;
            } else
            {
                genes = JsonUtility.FromJson<PlacementGenes>(saveData);
            }

            return genes;
        }

        internal static void Save(string saveName, PlacementGenes genes)
        {
            EditorPrefs.SetString(saveName, JsonUtility.ToJson(genes)); 
        }
    }
}
