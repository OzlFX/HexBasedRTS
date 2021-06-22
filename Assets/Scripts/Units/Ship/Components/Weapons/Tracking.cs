using UnityEngine;

public class Tracking : MonoBehaviour
{
    [Range(10.0f, 180.0f)]
    public float m_Range = 10.0f;

    private GameObject m_DetectedObject;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SphereCollider>().radius = m_Range;
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_DetectedObject = collision.gameObject;
    }

    public GameObject GetDetectedObject()
    {
        return m_DetectedObject;
    }
}
