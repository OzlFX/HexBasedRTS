using UnityEngine;

/* Ship Systems class will hold data such as; base armor, hull integrity, ship's components (shields, holodeck etc..), the max and current crew size,
 * the effective of the crew (moral and food), ship's speed, ship's overall loyalty, crew's loyalty,
 * the ship's effectiveness (how many crew members the ship has will affect this, the ship will have a minimum crew size to run
 * at optimal efficiency [70%]. Less efficiency means less effectiveness in battles [firerate, shield regen, hull and armor repairs etc..]), ship's cargo hold stats (capacity) */
public class ShipSystems : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ShieldPrefab;

    [SerializeField]
    private int m_HullIntegrity = 500;

    [SerializeField]
    private int m_BaseArmor = 220;

    [SerializeField]
    private int m_MaxCrewSize = 25;

    [SerializeField]
    private int m_CurrentCrewSize;

    [SerializeField]
    private float m_ShipSpeed = 25.0f;

    [SerializeField]
    private float m_ShipLoyalty;

    private void Start()
    {
        Instantiate<GameObject>(m_ShieldPrefab, transform.position, transform.rotation); //Create the shield at the ship's current location with its rotation
        m_ShieldPrefab.transform.parent = transform; //Set the shield's parent to the ship
    }
}
