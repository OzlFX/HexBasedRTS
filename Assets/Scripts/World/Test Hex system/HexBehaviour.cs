using UnityEngine;

public class HexBehaviour : MonoBehaviour
{
    public HexTile m_Tile;
    public HexMap m_Map;

    public void UpdatePosition()
    {
        this.transform.position = m_Tile.PositionFromCamera(
                    Camera.main.transform.position,
                    m_Map.getRows(),
                    m_Map.getColumns()
                ); ;
    }
}
