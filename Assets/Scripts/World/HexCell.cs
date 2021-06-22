using UnityEngine;

public class HexCell : MonoBehaviour
{
    [SerializeField]
    HexCell[] m_Neighbors;

    public HexCoordinates m_Coords;
    public Color m_Colour;

    public HexCell GetNeighbor (HexDirection direction)
    {
        return m_Neighbors[(int)direction];
    }

    public void SetNeighbor (HexDirection direction, HexCell cell)
    {
        m_Neighbors[(int)direction] = cell;
        cell.m_Neighbors[(int)direction.Opposite()] = this;
    }
}
