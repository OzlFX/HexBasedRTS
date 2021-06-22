using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHandler : MonoBehaviour
{
    GameObject m_UnitGO;
    private Vector3 m_SpawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_UnitGO = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateUnit(Unit _Unit)
    {
        GameObject Unit = Instantiate(
            m_UnitGO,
            m_SpawnPosition,
            Quaternion.identity
        );

        Unit.AddComponent<MeshFilter>();
        Unit.AddComponent<MeshRenderer>();


    }
}
