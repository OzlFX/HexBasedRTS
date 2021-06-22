using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonRotation : PlanetaryRotation
{
    // Start is called before the first frame update
    void Start()
    {
        m_OrbitSpeed = m_RotationPoint.GetComponent<PlanetaryRotation>().m_OrbitSpeed + m_RotationYSpeed;
    }
}
