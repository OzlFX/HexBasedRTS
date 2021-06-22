using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMeshGenerator : MonoBehaviour
{
    public int m_TileWidth, m_TileHeight;
    public float m_NoiseScale;

    public int m_Octaves = 4;

    [Range(0, 1)]
    public float m_Persistance = .5f;

    public float m_Lacunarity = 2f;

    public Vector2 m_Offset;

    public Mesh GenerateTileMesh(Mesh _HillMesh, int _TileWidth, int _TileHeight, float _NoiseScale)
    {
        Vector3[] vertices = _HillMesh.vertices;
        int[] triangles = _HillMesh.triangles;

        float[,] noiseMap = Noise.GenerateNoiseMap(_TileWidth, _TileHeight, _NoiseScale, m_Octaves, m_Persistance, m_Lacunarity, m_Offset);
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        int vertIndex = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                vertices[vertIndex] = new Vector3(x, noiseMap[x, y], y);
                vertIndex++;
            }
        }

        Mesh mesh = new Mesh
        {
            vertices = vertices,
            triangles = triangles
        };
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        return mesh;
    }

    void OnValidate()
    {
        if (m_Lacunarity < 1)
            m_Lacunarity = 1;

        if (m_Octaves < 0)
            m_Octaves = 0;
    }
}

public static class Noise
{
    public static float[,] GenerateNoiseMap(int _TileWidth, int _TileHeight, float _Scale, int _Octaves, float _Persistance, float _Lacunarity, Vector2 _Offset)
    {
        float[,] noiseMap = new float[_TileWidth, _TileHeight];

        if (_Scale <= 0)
            _Scale = .0001f;

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = _TileWidth / 2;
        float halfHeight = _TileHeight / 2;

        for (int y = 0; y < _TileHeight; y++)
        {
            for (int x = 0; x < _TileWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < _Octaves; i++)
                {
                    //Control sample point distance based on the frequency (higher frequency = larger distance) to rapidly change the height values
                    float sampleX = (x-halfWidth) / _Scale * frequency + _Offset.x;
                    float sampleY = (y-halfHeight) / _Scale * frequency + _Offset.y;

                    float perlinNoise = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinNoise * amplitude;

                    amplitude *= _Persistance;
                    frequency *= _Lacunarity;
                }

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }
                    
                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < _TileHeight; y++)
        {
            for (int x = 0; x < _TileWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }
                return noiseMap;
    }
}
