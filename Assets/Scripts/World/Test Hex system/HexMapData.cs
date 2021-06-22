/// <summary>
/// A struct containing public data for usage in both the map and tile classes. (Temporary solution)
/// </summary>
public struct HexMapData
{
    public HexMapData(bool _AllowWrapEastWest, bool _AllowWrapNorthSouth, int _Columns, int _Rows)
    {
        m_AllowWrapEastWest = _AllowWrapEastWest;
        m_AllowWrapNorthSouth = _AllowWrapNorthSouth;
        s_Columns = _Columns;
        s_Rows = _Rows;
    }

    public bool m_AllowWrapEastWest, m_AllowWrapNorthSouth;
    public readonly int s_Columns, s_Rows;
}
