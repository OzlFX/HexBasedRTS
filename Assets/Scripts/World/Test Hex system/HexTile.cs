using UnityEngine;

/// <summary>
/// Defines grid and world space positions, sizing and neighbours.
/// Does NOT interact with unity directly
/// </summary>
public class HexTile
{
    public HexTile(HexMapData _MapData, int _Column, int _Row)
    {
        this.m_MapData = _MapData;
        this.Column = _Column;
        this.Row = _Row;
        this.ZeroFactor = -(_Column + _Row);
    }

    // Column + Row + ZeroFactor = 0
    // ZeroFactor = -(Column + Row)
    public readonly int Column;
    public readonly int Row;
    public readonly int ZeroFactor;

    static readonly float WIDTH_MODIFIER = Mathf.Sqrt(3) / 2;
    static readonly float s_Radius = 1f;

    public float m_Elevation;
    public float m_Moisture;

    private HexMapData m_MapData;

    /// <summary>
    /// Calculate the sizing and spacing of each tile to be placed on the map, 
    /// convert this to the world space position based on the column and row and offsetting depending which row it is on
    /// </summary>
    /// <returns>World Space position of this Hex</returns>
    public Vector3 Position()
    {
        return new Vector3(
            TileHorizontalSpacing() * (this.Column + this.Row / 2f), 
            0, 
            TileVerticalSpacing() * this.Row);
    }

    float TileHeight()
    {
        return s_Radius * 2;
    }

    float TileWidth()
    {
        return WIDTH_MODIFIER * TileHeight();
    }

    float TileHorizontalSpacing()
    {
        return TileWidth();
    }

    float TileVerticalSpacing()
    {
        return TileHeight() * .75f;
    }

    //Get the tile size where x is the width and y is the height
    public Vector2 GetTileSize()
    {
        return new Vector2(TileWidth(), TileHeight());
    }

    public Vector3 PositionFromCamera(Vector3 _cameraPosition, float _Rows, float _Columns)
    {
        float mapHeight = _Rows * TileVerticalSpacing(); // Calculate map height
        float mapWidth = _Columns * TileHorizontalSpacing(); // Calculate map width

        Vector3 pos = Position();

        //if (m_MapData.m_AllowWrapEastWest)
        //{
            float Distance = (pos.x - _cameraPosition.x) / mapWidth; // Calculate distance from camera

            //Distance must be between -.5 and .5
            if (Mathf.Abs(Distance) <= .5f)
                return pos;

            if (Distance > 0)
                Distance += .5f;
            else
                Distance -= .5f;

            int Fix = (int)Distance;
            pos.x -= Fix * mapWidth;
        //}

        if (m_MapData.m_AllowWrapNorthSouth)
        {
            float iDistance = (pos.z - _cameraPosition.z) / mapHeight; // Calculate distance from camera

            //Distance must be between -.5 and .5
            if (Mathf.Abs(iDistance) <= .5f)
                return pos;

            if (iDistance > 0)
                iDistance += .5f;
            else
                iDistance -= .5f;

            int iFix = (int)iDistance;
            pos.z -= iFix * mapHeight;
        }
        
        return pos;
    }

    //Get the distance between two tiles. NOTE: doesnt work with wrapping currently
    public static float Distance(HexTile _A, HexTile _B)
    {
        int ColumnDiff = Mathf.Abs(_A.Column - _B.Column);
        if (_A.m_MapData.m_AllowWrapEastWest)
        {
            if (ColumnDiff > _A.m_MapData.s_Columns / 2)
                ColumnDiff = _A.m_MapData.s_Columns - ColumnDiff;
        }

        int RowDiff = Mathf.Abs(_A.Row - _B.Row);
        if (_A.m_MapData.m_AllowWrapNorthSouth)
        {
            if (RowDiff > _A.m_MapData.s_Rows / 2)
                RowDiff = _A.m_MapData.s_Rows - RowDiff;
        }

        //Largest value between the two tiles is the distance
        return Mathf.Max(
            ColumnDiff,
            RowDiff,
            Mathf.Abs(_A.ZeroFactor - _B.ZeroFactor));
    }
}
