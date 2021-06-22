using UnityEngine;

public class Weapons : MonoBehaviour
{
    public GameObject m_Projectile;
    GameObject m_ProjectileSpawn;

    void Start()
    {
        m_ProjectileSpawn = gameObject.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateProjectile();
        }
    }
    
    void CreateProjectile()
    {
        Instantiate(m_Projectile, 
            m_ProjectileSpawn.transform.position, 
            m_ProjectileSpawn.transform.rotation);

        m_Projectile.GetComponent<Projectile>().m_Creator = gameObject;
    }
}
