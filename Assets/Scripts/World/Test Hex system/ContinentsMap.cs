using UnityEngine;

public class ContinentsMap : HexMap
{
    [SerializeField]
    private int m_NumContinents = 1;

    [SerializeField]
    private float m_Scale;

    public override void GenerateMap()
    {
        //Call base first to obtain necessary data
        base.GenerateMap();
        base.SetMapBounds();

        int ContinentSpacing = m_Columns / m_NumContinents; //Determine spacing based on the number of columns and continents defined

        CreateLand(m_NumContinents, ContinentSpacing); //Generate land
        SetElevation();
        SetMoisture();
        //Update Tile graphics to match new generated data
        UpdateTileGraphics();
        SpawnUnitAt(m_UnitPrefab, 10, 10);
    }

    //Elevate the land at a certain point in the map
    void ElevateLand(int _Column, int _Row, int _Radius, float _CenterHeight = .8f)
    {
        HexTile Center = GetTileAt(_Column, _Row); //Get the center tile for the landmass to generate from 
        HexTile[] AreaTiles = GetTilesInRadius(Center, _Radius);

        foreach(HexTile Tile in AreaTiles)
        {
            Tile.m_Elevation = _CenterHeight * Mathf.Lerp(1f, .25f, 
                Mathf.Pow(HexTile.Distance(Center, Tile) / _Radius, 2f)); //Set the tile elevation based on the distance from the center tile
        }
    }

    //Generate the Land for the map based on the number of continents and spacing
    void CreateLand(int _NumContinents, int _ContinentSpacing)
    {
        Random.InitState(0); //Force same seed for testing
        for (int c = 0; c < _NumContinents; c++)
        {
            int NumLandAreas = Random.Range(4, 8); //Randomly generate how many large areas of land are on the map
            for (int i = 0; i < NumLandAreas; i++)
            {
                int range = Random.Range(5, 8);
                int y = Random.Range(range, m_Rows - range);
                int x = Random.Range(0, 10) - y / 2 + (c * _ContinentSpacing);

                ElevateLand(x, y, range);
            }
        }
    }

    //Add noise to the map based on the resolution and scale provided.
    float AddNoise(float _Resolution, float _Scale, int _AtColumn, int _AtRow)
    {
        Vector2 Offset = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));

        float noise = Mathf.PerlinNoise(
                    ((float)_AtColumn / Mathf.Max(m_Columns, m_Rows) / _Resolution) + Offset.x,
                    ((float)_AtRow / Mathf.Max(m_Columns, m_Rows) / _Resolution) + Offset.y) - .5f; //Apply noise based on resolution plus the offset
        return noise * _Scale; //Multiply noise by the scale value and add it to the current tile's elevation
    }

    //Increase scale for more islands and lakes
    void SetElevation()
    {
        //Loop through tiles on the map
        for (int column = 0; column < m_Columns; column++)
        {
            for (int row = 0; row < m_Rows; row++)
            {
                HexTile Tile = GetTileAt(column, row); //Get the tile at loop location
                Tile.m_Elevation += AddNoise(.01f, 2f, column, row);
            }
        }
    }

    void SetMoisture()
    {
        //Loop through tiles on the map
        for (int column = 0; column < m_Columns; column++)
        {
            for (int row = 0; row < m_Rows; row++)
            {
                HexTile Tile = GetTileAt(column, row); //Get the tile at loop location
                Tile.m_Moisture = AddNoise(.05f, 2f, column, row);
                Debug.Log("Moisture at Tile: " + column + "," + row + " is " + Tile.m_Moisture);
            }
        }
    }

    static float[,] GenerateNoise(int _Rows, int _Columns, float _Scale)
    {
        float[,] noise = new float[_Rows, _Columns];

        if (_Scale <= 0)
            _Scale = 0.0001f;

        for (int y = 0; y < _Columns; y++)
        {
            for (int x = 0; x < _Rows; x++)
            {
                float sampleX = x / _Scale;
                float sampleY = y / _Scale;

                noise[x, y] = Mathf.PerlinNoise(sampleX, sampleY);
            }
        }

        return noise;
    }
}
