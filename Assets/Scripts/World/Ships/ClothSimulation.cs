using UnityEngine;

public class ClothSimulation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Cloth>().externalAcceleration = -Physics.gravity / 2; //Reduce cloth drop
    }
}
