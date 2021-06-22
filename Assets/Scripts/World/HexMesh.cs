using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour
{
    Mesh m_HexMesh;
    MeshCollider m_MeshCollider;
    List<Vector3> m_Vertices;
    List<int> m_Triangles;
    List<Color> m_Colours;

    void Awake()
    {
        GetComponent<MeshFilter>().mesh = m_HexMesh = new Mesh();
        m_MeshCollider = gameObject.AddComponent<MeshCollider>();
        m_HexMesh.name = "Hex Mesh";
        m_Vertices = new List<Vector3>();
        m_Triangles = new List<int>();
        m_Colours = new List<Color>();
    }

    public void Triangulate(HexCell[] cells)
    {
        m_HexMesh.Clear();
        m_Vertices.Clear();
        m_Triangles.Clear();
        m_Colours.Clear();

        for(int i = 0; i < cells.Length; i++)
        {
            Triangulate(cells[i]);
        }

        m_HexMesh.vertices = m_Vertices.ToArray();
        m_HexMesh.colors = m_Colours.ToArray();
        m_HexMesh.triangles = m_Triangles.ToArray();
        m_HexMesh.RecalculateNormals();
        m_MeshCollider.sharedMesh = m_HexMesh;
    }

    void Triangulate(HexCell cell)
    {
        Vector3 center = cell.transform.localPosition;

        for (int i = 0; i < 6; i++)
        {
            AddTriangle(
            center,
            center + HexMetrices.m_Corners[i],
            center + HexMetrices.m_Corners[i + 1]
            );
            AddTriangleColour(cell.m_Colour);
        }
    }

    void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        int vertexIndex = m_Vertices.Count;
        m_Vertices.Add(v1);
        m_Vertices.Add(v2);
        m_Vertices.Add(v3);
        m_Triangles.Add(vertexIndex);
        m_Triangles.Add(vertexIndex + 1);
        m_Triangles.Add(vertexIndex + 2);
    }

    void AddTriangleColour(Color colour)
    {
        m_Colours.Add(colour);
        m_Colours.Add(colour);
        m_Colours.Add(colour);
    }
}
