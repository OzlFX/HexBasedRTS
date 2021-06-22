using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour
{
    public int m_Width = 6;
    public int m_Height = 6;

    public HexCell m_CellPrefab;

    public Text m_TileLabelPrefab;

    Canvas m_GridCanvas;
    HexMesh m_HexMesh;

    HexCell[] m_Tiles;

    public Color m_DefaultColour = Color.white;
    //public Color m_TouchedColour = Color.magenta;

    void Awake()
    {
        m_GridCanvas = GetComponentInChildren<Canvas>();
        m_HexMesh = GetComponentInChildren<HexMesh>();

        m_Tiles = new HexCell[m_Height * m_Width];

        for (int z = 0, i = 0; z < m_Height; z++)
        {
            for (int x = 0; x < m_Width; x++)
            {
                CreateTile(x, z, i++);
            }
        }
    }

    void Start()
    {
        m_HexMesh.Triangulate(m_Tiles);
    }

    public void TouchTile(Vector3 position, Color colour)
    {
        position = transform.InverseTransformPoint(position);
        HexCoordinates Coords = HexCoordinates.FromPosition(position);
        int index = Coords.getX + Coords.getZ * m_Width + Coords.getZ / 2;
        HexCell tile = m_Tiles[index];
        tile.m_Colour = colour;
        m_HexMesh.Triangulate(m_Tiles);
        Debug.Log("Touched at " + position);
    }

    void CreateTile(int x, int z, int i)
    {
        Vector3 m_Position;
        m_Position.x = (x + z * 0.5f - z / 2) * (HexMetrices.m_InnerRadius * 2.0f);
        m_Position.y = 0.0f;
        m_Position.z = z * (HexMetrices.m_OuterRadius * 1.5f);

        HexCell tile = m_Tiles[i] = Instantiate<HexCell>(m_CellPrefab);
        tile.transform.SetParent(transform, false);
        tile.transform.localPosition = m_Position;
        tile.m_Coords = HexCoordinates.FromOffsetCoordinates(x, z);
        tile.m_Colour = m_DefaultColour;

        //Set up directionally connected cells in east and west
        if (x > 0)
            tile.SetNeighbor(HexDirection.W, m_Tiles[i - 1]);

        //Set up directionally connected cells for remaining directions
        if (z > 0)
        {
            if ((z & 1) == 0)
            {
                tile.SetNeighbor(HexDirection.SE, m_Tiles[i - m_Width]);
                if (x > 0)
                    tile.SetNeighbor(HexDirection.SW, m_Tiles[i - m_Width - 1]);
            }
            else
            {
                tile.SetNeighbor(HexDirection.SW, m_Tiles[i - m_Width]);
                if (x < m_Width - 1)
                    tile.SetNeighbor(HexDirection.SE, m_Tiles[i - m_Width - 1]);
            }
        }

        Text label = Instantiate<Text>(m_TileLabelPrefab);
        label.rectTransform.SetParent(m_GridCanvas.transform, false);
        label.rectTransform.anchoredPosition =
            new Vector2(m_Position.x, m_Position.z);
        label.text = tile.m_Coords.ToStringOnSeparateLines();
    }
}
