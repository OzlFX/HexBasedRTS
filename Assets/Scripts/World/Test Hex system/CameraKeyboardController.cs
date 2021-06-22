using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraKeyboardController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [SerializeField]
    [Range(5, 30)]
    float m_Movespeed = 20f;

    // Update is called once per frame
    void Update()
    {
        Vector3 translate = new Vector3(Input.GetAxis("Horizontal"), 
            0, 
            Input.GetAxis("Vertical"));

        this.transform.Translate(translate * m_Movespeed * Time.deltaTime, Space.World);
    }
}
