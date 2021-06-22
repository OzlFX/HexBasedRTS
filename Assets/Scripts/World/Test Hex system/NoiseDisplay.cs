using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseDisplay : MonoBehaviour
{
    public Renderer m_TextureRenderer;

    public void DrawNoiseMap(float[,] _NoiseMap)
    {
        int width = _NoiseMap.GetLength(0);
        int height = _NoiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        Color[] colourMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, _NoiseMap[x, y]);
            }
        }

        texture.SetPixels(colourMap);
        texture.Apply();

        m_TextureRenderer.sharedMaterial.mainTexture = texture;
        m_TextureRenderer.transform.localScale = new Vector3(width, 1, height);
    }
}
