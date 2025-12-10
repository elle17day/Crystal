using UnityEngine;
using UnityEditor;
using System;
using Unity.Mathematics;
using JetBrains.Annotations;
using Random = UnityEngine.Random;

public class ProceduralGeneration : EditorWindow
{
    private Texture2D noiseMapTexture;
    private float density = 0.5f;
    private GameObject prefab;

    // Showing of initial window
    [MenuItem("Tools/Wizards Plant Placement")]
    public static void ShowWindow()
    {
        GetWindow<ProceduralGeneration>("Plant Placement");
    }

    // GUI Creation

    // Creation of the noise texture as the base location for x, y of where the trees are placed
    private void OnGUI()
    {
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

        // Slider to allow the user to select the density of the trees
        density = EditorGUILayout.Slider("Density", density, 0, 1);

        // Area to allow the user to select a prefab
        prefab = (GameObject)EditorGUILayout.ObjectField("Object Prefab", prefab, typeof(GameObject), false);

        if (GUILayout.Button("Place Objects"))
        {
            PlaceObjects(Terrain.activeTerrain, noiseMapTexture, density, prefab);
        }

    }

    // Object place algorithm (Managed how the prefabs are placed in the world)
    public static void PlaceObjects(Terrain terrain, Texture2D noiseMapTexture, float density, GameObject prefab)
    {
        Transform parent = new GameObject("PlacedObjects").transform;

        // Iterate at 1 meter intervals
        for (int i = 0; i < terrain.terrainData.size.x; i++)
        {
            for (int j = 0; j < terrain.terrainData.size.y; j++)
            {
                float noiseMapValue = noiseMapTexture.GetPixel(i, j).g;

                // If value is above threshold, instantiate tree in position
                if (noiseMapValue > 1 - density)
                {
                    // Calculates position for the placement of the prefab
                    Vector3 pos = new Vector3(i / Random.Range(-0.5f, 0.5f), 0, j / Random.Range(-0.5f, 0.5f));
                    pos.y = terrain.terrainData.GetInterpolatedHeight(i / (float)terrain.terrainData.size.x, j / (float)terrain.terrainData.size.y);

                    // Instantiates the prefab and assigns it a parent object to prevent hierarchy cluttering
                    GameObject go = Instantiate(prefab, pos, Quaternion.identity);
                    go.transform.SetParent(parent);
                }
            }
        }
    }
}
