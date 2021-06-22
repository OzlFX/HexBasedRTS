using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Shields : MonoBehaviour
{
    [SerializeField]
    protected int m_HitPoints;

    [SerializeField]
    protected int m_EnergyCost;

    [SerializeField]
    protected float m_CreditCost;

    [SerializeField]
    protected string m_ShieldName = "";

    [SerializeField]
    protected AudioClip m_ShieldsDownAudio;

    void Start()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Projectile>() && !collision.gameObject.GetComponent<Projectile>().m_Creator == gameObject.transform.parent)
        {
            m_HitPoints -= collision.gameObject.GetComponent<Projectile>().m_Damage;
        }
    }

    private void Update()
    {
        if (m_HitPoints <= 0)
            DestroyShields();
    }

    /* Once a proper regen system is in place this will be changed to regenerate the shields out of combat at a cost */
    private void DestroyShields()
    {
        Destroy(gameObject); //Destroy the shields
    }
}
