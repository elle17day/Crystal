using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise
{
    // Using perlin noise algorithm (https://rtouti.github.io/graphics/perlin-noise-algorithm, "Raouf's Blog", 2025) create a noise map texture
    // 
    // "width" = Width of the generated noise map
    // "height" = height of the generated noise map
    // "scale" = scale of the noise
    // Returns a texture with a noise map
    public static Texture2D GetNoiseMap(int width, int height, float scale)
    {
        // Creates a new texture and sets its size
        Texture2D noiseMapTexture = new Texture2D(width, height);

        // Iterate over each pixel in the texture
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Generates noise value
                float noiseValue = Mathf.PerlinNoise((float)x / width * scale, (float)y / height * scale);

                // Set the pixel colour based on the noise value
                noiseMapTexture.SetPixel(x, y, new Color(0, noiseValue, 0));
            }
        }

        // Apply changes made to the shown texture
        noiseMapTexture.Apply();

        return noiseMapTexture;
    }
}
