using UnityEngine;

public enum UnitClass { Swordsman, Ranger };

public class UnitMilitary : Unit
{
    public UnitClass m_UnitClass;

    //Combat Stats
    public float m_Damage = 0;
    public float m_MaxDamage = 0;

    public UnitMilitary(Mesh _Mesh, Material _Material, float _BaseHP = 100,  float _MovementSpeed = 5,
        UnitClass _UnitClass = UnitClass.Swordsman, float _Damage = 10) 
        : base(_Mesh, _Material, _BaseHP, _MovementSpeed)
    {
        m_UnitType = UnitType.Military;
        m_UnitClass = _UnitClass;
        m_Damage = _Damage;
        m_MaxDamage = _Damage + _Damage / 2;
    }

    //Scales the Unit's damage based on the inputted scaler (modified damage is capped at + 50 of base damage)
    public void DamageModifier(float _DamageScaler = 1f)
    {
        if (_DamageScaler < 1f)
            _DamageScaler = 1f;

        m_Damage *= _DamageScaler;

        if (m_Damage > m_MaxDamage)
            m_Damage = m_MaxDamage;
    }
}
