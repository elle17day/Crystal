using System.Collections.Generic;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour
{
    // Area for storing the tree prefabs
    public GameObject[] treePrefabs;

    // Defining bounds for tatal number of trees as well as how many times the game should try and place a tree (in-case of collision)
    public int numberOfPrefabInstances = 100;
    public int maxPlacementAttempts = 10;

    // Area the trees should generate in
    public Vector3 generationAreaSize = new Vector3(100f, 1f, 100f);

    // What the trees are stored in within the heirarchy to prevent it being flooded with instances
    public Transform parentContainer;

    // Where the trees can be placed in the given area as well as the radius in which another tree can't be spawned
    public LayerMask placementBlockerMask;
    public float treeRadius = 1.5f;

    // Height of terrain
    public float absoluteGroundLevel = 0f; 

    bool CanPlaceTree(Vector3 position)
    {
        // Checks when trying to place a tree whether it impeded upon another tree or object
        Collider[] hits = Physics.OverlapSphere(position, treeRadius, placementBlockerMask, QueryTriggerInteraction.Collide);

        return hits.Length == 0;
    }

    void Start()
    {
        // If no parentContainer provided, instances will be generated as children of the generator.
        if (parentContainer == null)
        {
            parentContainer = transform.root;
        }
        // sets the tree position to a random position in the given area
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, absoluteGroundLevel, gameObject.transform.position.z);

        // spawns a tree
        Generate();

    }

    void Generate()
    {
        // Loop for the number of trees you want
        for (int i = 0; i < numberOfPrefabInstances; i++)
        {
            // grabs a random prefab from the list
            GameObject chosenPrefab = treePrefabs[Random.Range(0, treePrefabs.Length)];

            // Tried to place a tree for the maximum number of attempts provided
            for (int attempt = 0; attempt < maxPlacementAttempts; attempt++)
            {
                // Gets a random position from the area
                Vector3 randomPosition = GetRandomPositionInGenerationArea();

                // If it can't place the tree end
                if (!CanPlaceTree(randomPosition))
                    continue;

                // Otherwise save the location as a Quaternion
                Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

                //Instantiate(prefab, randomPosition, randomRotation);
                Instantiate(chosenPrefab, randomPosition, randomRotation, parentContainer.transform);

                break;
            }
        }
    }

    Vector3 GetRandomPositionInGenerationArea()
    {
        // Utilising the area and height enterred earlier, grab a random x, y and z co-ordinate
        Vector3 randomPosition = new Vector3(
           Random.Range(-generationAreaSize.x / 2, generationAreaSize.x / 2),
           0f,
           Random.Range(-generationAreaSize.z / 2, generationAreaSize.z / 2)
       );

        // Return the location
        return transform.position + randomPosition;
    }

    // Draw Gizmo to visualize the generation area
    void OnDrawGizmosSelected()
    {
        // Draw a coloured box to visualize area
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, generationAreaSize);
    }
}
