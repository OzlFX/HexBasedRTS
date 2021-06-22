using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementHandler : MonoBehaviour
{
    Vector3 m_OldPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_OldPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Code to click and drag, zoom and movement based on input

        CheckIfCameraMoved();
    }

    public void PanToHex(HexTile _Tile)
    {

    }

    void CheckIfCameraMoved()
    {
        if (m_OldPosition != this.transform.position)
        {
            m_OldPosition = this.transform.position; //Something moved the camera

            //TODO: HexMap will handle this in a dictionary later
            HexBehaviour[] Tiles = GameObject.FindObjectsOfType<HexBehaviour>(); //Find all objects that are HexBehaviour

            foreach(HexBehaviour Tile in Tiles)
            {
                //Tile.UpdatePosition();
            }
        }
    }
}
