using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public int m_Damage = 5;

    [Range(2.0f, 20.0f)]
    public float m_AliveTime = 4.0f;

    [HideInInspector]
    public GameObject m_Creator;

    [SerializeField]
    [Range(200.0f, 800.0f)]
    private float m_ProjectileSpeed = 200.0f;

    private Rigidbody m_Rigidbody;

    private float m_TimeAlive;
    private float m_RotationPoint;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_RotationPoint = transform.eulerAngles.y;
        m_TimeAlive = Time.time;
    }

    void Update()
    { 
        Destroy(gameObject, m_AliveTime); //When the projectile has been alive for too long, delete it
        m_Rigidbody.AddForce(transform.forward * m_ProjectileSpeed);
    }

    float CheckTimeAlive()
    {
        return Time.time - m_TimeAlive;
    }

    //Detect projectile collision with another object
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);
        if (collision.gameObject == m_Creator)
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>(), true);
            Destroy(gameObject);
        }
    }
}
