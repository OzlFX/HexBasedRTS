using UnityEngine;

public class Turret : MonoBehaviour
{
    private bool m_Active;

    // Start is called before the first frame update
    void Start()
    {
        m_Active = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject ObjectInRange = gameObject.transform.parent.gameObject.GetComponent<Tracking>().GetDetectedObject().gameObject;

        if (ObjectInRange != null)
        {
            gameObject.transform.LookAt(ObjectInRange.transform.position);
        }
    }
}
