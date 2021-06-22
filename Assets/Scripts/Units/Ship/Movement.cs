using UnityEngine;

public class Movement : MonoBehaviour
{
    RaycastHit m_Hit;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Get the mouse position from a ray
            if (Physics.Raycast(ray, out m_Hit))
                Debug.Log("Mouse Position" + m_Hit.point);
                    //Vector3.MoveTowards(transform.position, ray.direction, 500.0f);
        }
    }
}
