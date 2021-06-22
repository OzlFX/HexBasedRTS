using UnityEngine;

public class TileData : MonoBehaviour
{
    public int m_MaxUnitsOnTile = 100;
    public int m_CurrentUnitAmountOnTile = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool CheckCityOnTile(HexTile _Tile)
    {
        ///TODO: Check if the current tile has a city on
        if (true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
