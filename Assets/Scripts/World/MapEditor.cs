using UnityEngine;
using UnityEngine.EventSystems;

public class MapEditor : MonoBehaviour
{
    public Color[] m_Colours;
    public HexGrid m_HexGrid;
    private Color m_ActiveColour;

    void Awake()
    {
        SelectColour(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)
            && !EventSystem.current.IsPointerOverGameObject())
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(inputRay, out hit))
            m_HexGrid.TouchTile(hit.point, m_ActiveColour);
    }

    public void SelectColour(int index)
    {
        m_ActiveColour = m_Colours[index];
    }
}
