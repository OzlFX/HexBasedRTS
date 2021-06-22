using UnityEngine;

public enum UnitType { Civilian, Military, Artillery, Vessel, Combat_Vessel };

public class Unit : MonoBehaviour
{
    //Unit Visuals
    protected Mesh m_UnitMesh = null;
    protected Material m_UnitMaterial = null;

    //Basic Stats
    public string m_UnitName = "";
    public UnitType m_UnitType;
    //public UnitClass m_UnitClass;

    //Health Stats
    public float m_BaseHP = 0;
    public float m_CurrentHP = 0;
    protected float m_MaxHP = 0;

    //Movement Stats
    protected float m_MovementSpeed = 0;
    protected float m_MaxMovementSpeed = 0;

    [HideInInspector]
    public bool m_IsSelected = false;

    public Unit(Mesh _Mesh, Material _Material, float _BaseHP = 100, float _MovementSpeed = 5)
    {
        m_UnitMesh = _Mesh;
        m_UnitMaterial = _Material;

        m_BaseHP = _BaseHP;
        m_CurrentHP = _BaseHP;
        m_MaxHP = _BaseHP;

        m_MovementSpeed = _MovementSpeed;
        m_MaxMovementSpeed = _MovementSpeed * 2;
        m_UnitType = 0;
    }

    public Mesh GetMesh()
    {
        return m_UnitMesh;
    }

    public Material GetMaterial()
    {
        return m_UnitMaterial;
    }

    //Scales the Unit's movement based on the inputted scaler (modified movement is capped at double)
    public void MovementModifier(float _MovementScaler = 1f)
    {
        if (_MovementScaler < 1f)
            _MovementScaler = 1f;

        m_MovementSpeed *= _MovementScaler;

        if (m_MovementSpeed > m_MaxMovementSpeed)
            m_MovementSpeed = m_MaxMovementSpeed;
    }

    public void Move()
    {

    }

    public bool IsSelected()
    {
        return m_IsSelected;
    }
}
