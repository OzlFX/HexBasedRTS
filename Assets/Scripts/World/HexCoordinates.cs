using UnityEngine;

[System.Serializable]
public struct HexCoordinates
{
    [SerializeField]
    private int m_X, m_Z;

    public int getX { get { return m_X; } }
    public int getZ { get { return m_Z; } }
    public int getY { get { return -m_X - m_Z; } }

    public HexCoordinates(int x, int z)
    {
        this.m_X = x;
        this.m_Z = z;
    }

    public static HexCoordinates FromOffsetCoordinates(int x, int z)
    {
        return new HexCoordinates(x - z / 2, z);
    }

    public static HexCoordinates FromPosition(Vector3 position)
    {
        float x = position.x / (HexMetrices.m_InnerRadius * 2.0f);
        float y = -x;

        float offset = position.z / (HexMetrices.m_OuterRadius * 3.0f);
        x -= offset;
        y -= offset;

        int iX = Mathf.RoundToInt(x);
        int iY = Mathf.RoundToInt(y);
        int iZ = Mathf.RoundToInt(-x - y);

        //Prevent hex coords error
        if (iX + iY + iZ != 0)
        {
            float dX = Mathf.Abs(x - iX);
            float dY = Mathf.Abs(y - iY);
            float dZ = Mathf.Abs(-x -y - iZ);

            if (dX > dY && dX > dZ)
            {
                iX = -iY - iZ;
            }
            else if (dZ > dY)
            {
                iZ = iX - iY;
            }
        }

        return new HexCoordinates(iX, iZ);
    }

    public override string ToString()
    {
        return "(" + m_X.ToString() + ", " + getY.ToString() + ", " + m_Z.ToString() + ")";
    }

    public string ToStringOnSeparateLines()
    {
        return m_X.ToString() + "\n" + getY.ToString() + "\n" + m_Z.ToString();
    }
}
