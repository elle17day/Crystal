using UnityEngine;
using System.Collections;

public static class falloffGenerator
{
    public static float[,] GenerateFalloffMap(int size)
    {
        // Create the map
        float[,] map = new float[size, size];

        // Populate map with values
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                float x = i / (float)size * 2 - 1;
                float y = j / (float)size * 2 - 1;

                float value =  Mathf.Min (Mathf.Abs (x), Mathf.Abs (y));
                map [i, j] = value;
            }
        }

        return map;
    }
}
