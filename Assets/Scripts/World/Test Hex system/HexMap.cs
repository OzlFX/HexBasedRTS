using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Base map class to generate a map.
/// Handles basic map data such as heights, materials, 
/// meshes and wrapping (will revisit wrapping in future, may not be wanted).
/// </summary>
public class HexMap : MonoBehaviour
{
    public GameObject m_HexTile;

    public Mesh m_WaterMesh;
    public Mesh m_FlatlandMesh;
    public Mesh m_HillsMesh;
    public Mesh m_MountainMesh;

    public GameObject m_ForestPrefab;
    public GameObject m_JunglePrefab;

    public Material m_OceanMaterial;
    public Material m_GrasslandMaterial;
    public Material m_PlainsMaterial;
    public Material m_MountainMaterial;
    public Material m_DesertMaterial;

    private HexMapData m_MapData;

    [SerializeField]
    protected int m_Columns = 10;

    [SerializeField]
    protected int m_Rows = 10;

    [SerializeField]
    protected float m_HillHeight = .6f;
    
    [SerializeField]
    protected float m_MountainHeight = .85f;
    
    [SerializeField]
    protected float m_FlatlandHeight = 0f;

    [SerializeField]
    protected float m_MoistureForest = .5f;

    [SerializeField]
    protected float m_MoistureRainforest = 1f;

    [SerializeField]
    protected float m_MoistureGrasslands = .0f;

    [SerializeField]
    protected float m_MoisturePlains = -.5f;

    [SerializeField]
    private GameObject m_DebugHexCoords;

    [SerializeField]
    private Canvas m_Canvas;

    private HexTile[,] m_Tiles;
    private Dictionary<HexTile, GameObject> m_TileToGameObjectMap; //Define conversion var for tiles to gameobjects
    private Dictionary<GameObject, HexTile> m_GameObjectToHexMap; //Define conversion var for gameobjects to tiles

    private TileMeshGenerator m_TileMeshGenerator;

    private GameObject[,] m_Labels;

    [SerializeField]
    protected GameObject m_UnitPrefab;

    private Vector3[] m_MapBounds;

    public int getColumns() { return m_Columns; }
    public int getRows() { return m_Rows; }

    private List<Mesh> m_HillMeshVariation;

    // Start is called before the first frame update
    void Start()
    {
        m_TileMeshGenerator = new TileMeshGenerator();
        GenerateMap();
        SetMapBounds();
    }

    public HexTile GetTileAt(int _x, int _y)
    {
        if (m_Tiles == null)
        {
            Debug.LogError("Tiles array not yet instantiated.");
            return null;
        }

        //if (m_MapData.m_AllowWrapEastWest)
        //{
            _x %= m_Columns;
            if (_x < 0)
                _x += m_Columns;
        //}
            
        //if (m_MapData.m_AllowWrapNorthSouth)
        //{
            _y %= m_Rows;
            if (_y < 0)
                _y += m_Rows;
        //}

        try
        {
            return m_Tiles[_x, _y];
        }
        catch
        {
            return null;
        }
    }

    public HexTile GetTileFromGameObject(GameObject _TileGameObject)
    {
        if (m_GameObjectToHexMap.ContainsKey(_TileGameObject))
            return m_GameObjectToHexMap[_TileGameObject];

        return null;
    }

    GameObject GetGameObjectFromTile(HexTile _Tile)
    {
        if (m_TileToGameObjectMap.ContainsKey(_Tile))
            return m_TileToGameObjectMap[_Tile];

        return null;
    }

    //Generares a totally random hex map
    virtual public void GenerateMap()
    {
        m_MapData = new HexMapData(false, false, m_Columns, m_Rows);

        m_Tiles = new HexTile[m_Columns, m_Rows]; //Create new 2D array to contain the tiles of the map at the set size
        m_TileToGameObjectMap = new Dictionary<HexTile, GameObject>(); //Create new dictionary to convert the tile ref to its ingame gameobject

        for (int column = 0; column < m_Columns; column++)
        {
            for (int row = 0; row < m_Rows; row++)
            {
                //Setup Tile position based on its position to the camera (allows for looping)
                HexTile Tile = new HexTile(m_MapData, column, row)
                {
                    m_Elevation = -.5f //Setup initial elevation
                };

                m_Tiles[column, row] = Tile; //Add created tile to the map array

                //Get Tile position based on its position to the camera
                Vector3 Position = Tile.PositionFromCamera(
                    Camera.main.transform.position,
                    m_Rows,
                    m_Columns
                );

                //Instantiate a Hex Tile
                GameObject CopyGO = (GameObject)Instantiate(
                    m_HexTile,
                    Position,
                    Quaternion.identity,
                    this.transform
                );

                m_TileToGameObjectMap[Tile] = CopyGO;

                CopyGO.GetComponent<HexBehaviour>().m_Tile = Tile;
                CopyGO.GetComponent<HexBehaviour>().m_Map = this;

                /*GameObject LabelObject = (GameObject)Instantiate(m_DebugHexCoords, Position, Quaternion.identity, m_Canvas.transform);
                m_Labels[column, row] = LabelObject;

                LabelObject.GetComponent<TMPro.TextMeshProUGUI>().rectTransform.SetParent(m_Canvas.transform, false);
                LabelObject.GetComponent<TMPro.TextMeshProUGUI>().rectTransform.anchoredPosition =
                    new Vector2(Position.x, Position.z);
                LabelObject.GetComponent<TMPro.TextMeshProUGUI>().text = string.Format("{0},{1}", column, row);*/
            }
        }

        /// Use this to prevent map movement
        //StaticBatchingUtility.Combine(this.gameObject);
        UpdateTileGraphics();
    }

    //Updates the tile graphics
    protected void UpdateTileGraphics()
    {
        int HillVariations = 0;

        for (int column = 0; column < m_Columns; column++)
        {
            for (int row = 0; row < m_Rows; row++)
            {
                HexTile Tile = m_Tiles[column, row];
                GameObject TileObject = m_TileToGameObjectMap[Tile];

                MeshRenderer mr = TileObject.GetComponentInChildren<MeshRenderer>();
                MeshFilter mf = TileObject.GetComponentInChildren<MeshFilter>();
                
                if (Tile.m_Elevation >= m_FlatlandHeight && Tile.m_Elevation < m_MountainHeight)
                {
                    if (Tile.m_Moisture >= m_MoistureRainforest)
                    {
                        mr.material = m_GrasslandMaterial;

                        Vector3 ElevatedPos = TileObject.transform.position;

                        if (Tile.m_Elevation >= m_HillHeight)
                        {
                            ElevatedPos.y += .41f;
                        }
                        GameObject.Instantiate(m_JunglePrefab, ElevatedPos, Quaternion.identity, TileObject.transform);
                    }
                    else if (Tile.m_Moisture >= m_MoistureForest)
                    {
                        mr.material = m_GrasslandMaterial;

                        Vector3 ElevatedPos = TileObject.transform.position;

                        if (Tile.m_Elevation >= m_HillHeight)
                        {   
                            ElevatedPos.y += .41f;
                        }
                        GameObject.Instantiate(m_ForestPrefab, ElevatedPos, Quaternion.identity, TileObject.transform);
                    }
                    else if (Tile.m_Moisture >= m_MoistureGrasslands)
                    {
                        mr.material = m_GrasslandMaterial;
                    }
                    else if (Tile.m_Moisture >= m_MoisturePlains)
                    {
                        mr.material = m_PlainsMaterial;
                    }
                    else
                    {
                        mr.material = m_DesertMaterial;
                    }
                }

                if (Tile.m_Elevation >= m_MountainHeight)
                {
                    mr.material = m_MountainMaterial;
                    mf.mesh = m_MountainMesh;
                }
                else if (Tile.m_Elevation >= m_HillHeight)
                {
                    GenerateHillMesh();
                    mf.mesh = m_HillMeshVariation[HillVariations];
                    HillVariations++;
                    ///TODO: Add the tile base to the hill so there isnt a hole in the world
                }
                else if (Tile.m_Elevation >= m_FlatlandHeight)
                {
                    mf.mesh = m_FlatlandMesh;
                }
                else
                {
                    mr.material = m_OceanMaterial;
                    mf.mesh = m_WaterMesh;
                }
            }
        }
    }

    ///TODO: Generate Hill mesh function to manipulate the vertices of the hill mesh based on the noise generator output
    ///Create local copy of the vertices from the mesh and loop through them manipulating the vertex height based on the noise generated
    protected void GenerateHillMesh()
    {
        Mesh HillMesh = m_HillsMesh;
        //Vector2 TileSize = m_Tiles[0,0].GetTileSize();
        m_HillMeshVariation.Add(m_TileMeshGenerator.GenerateTileMesh(
            HillMesh,
            10,
            10,
            25));
    }

    //Set the bounds for the camera on the map based on the size
    protected void SetMapBounds()
    {
        m_MapBounds = new Vector3[4];

        HexTile Tile;
        List<Vector2> CornerPositions = new List<Vector2>
        {
            new Vector2(0, 0),
            new Vector2(0, m_Rows),
            new Vector2(m_Columns, 0),
            new Vector2(m_Columns, m_Rows)
        };

        List<GameObject> CornerObjects = new List<GameObject>();

        for (int i = 0; i < 4; i++)
        {
            Debug.Log("Corner X Pos: " + (int)CornerPositions[i].x + ", Corner Y Pos" + (int)CornerPositions[i].y);
            Tile = GetTileAt((int)CornerPositions[i].x, (int)CornerPositions[i].y);
            GameObject TileObject = GetGameObjectFromTile(Tile);
            Debug.Log(TileObject.transform.position);
            CornerObjects.Add(TileObject);
        }

        for (int i = 0; i < 4; i++)
        {
            m_MapBounds[i] = CornerObjects[i].transform.position;
            Debug.Log(CornerObjects[i].transform.position);
        }
    }

    public Vector3[] GetMapBounds()
    {
        return m_MapBounds;
    }

    public void CheckMapBounds(Camera _CameraToCheck)
    {
        if (!m_MapData.m_AllowWrapEastWest)
        {
            ///TODO: Check Camera's current position with the map bounds position and prevent it from
            ///going past that (may need to copy camera position and return it from the function
            if (_CameraToCheck.transform.position.x >= m_MapBounds[0].x)
            {
                _CameraToCheck.transform.position = m_MapBounds[0];
            }
        }
    }

    //Get the tiles around a defined tile in a radius
    public HexTile[] GetTilesInRadius(HexTile _CentreTile, int _Radius)
    {
        List<HexTile> results = new List<HexTile>();

        //Get each tile surrounding a defined time in the set radius
        for (int x = -_Radius; x < _Radius; x++)
        {
            for (int y = Mathf.Max(-_Radius+1, -x - _Radius); y < Mathf.Min(_Radius, -x + _Radius-1); y++)
            {
                results.Add(GetTileAt(_CentreTile.Column + x, _CentreTile.Row + y));
            }
        }

        return results.ToArray();
    }

    public void SpawnUnitAt(GameObject _Prefab, int _Column, int _Row)
    {
        GameObject SpawnTile = m_TileToGameObjectMap[GetTileAt(_Column, _Row)];

        ///May wish to parent the unit later, will need thought
        Instantiate(_Prefab, SpawnTile.transform.position, Quaternion.identity);
    }
}
