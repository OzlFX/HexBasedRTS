using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaders_Base : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        m_Traits = new Traits_Base[3];
        m_IsAssigned = false;
    }

    public string m_Name = "";
    public Traits_Base[] m_Traits;
    Dictionary<string, Traits_Base> m_StringToTraits;

    protected bool m_IsAssigned;

    protected void Assign()
    {

    }

    protected bool IsAssigned()
    {
        return m_IsAssigned;
    }

    public void DisplayOnUI()
    {

    }
}
