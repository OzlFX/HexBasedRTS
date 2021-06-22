using UnityEngine;

public class PlanetaryRotation : MonoBehaviour
{
    [Range(0.1f, 60.0f)]
    public float m_RotationYSpeed = 0.5f;

    [Range(0.0001f, 1.0f)]
    public float m_TiltSpeed = 0.01f;

    [Range(5.0f, 25.0f)]
    public float m_MaxTilt = 15.0f;

    [Range(0.001f, 60.0f)]
    public float m_OrbitSpeed = 0.002f;

    public Transform m_RotationPoint;

    // Update is called once per frame
    public void Update()
    {
        transform.Rotate(transform.up, m_RotationYSpeed * Time.deltaTime);
        //transform.Rotate(Vector3.up * (m_RotationYSpeed * Time.deltaTime));
        Tilt();
        RotateAroundPoint(transform.position, m_RotationPoint.position, new Vector3(0, m_OrbitSpeed * Time.deltaTime, 0));
    }
    void Tilt()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles; //Create new rotation from current object rotation
        currentRotation.x = Mathf.Sin(Time.time * m_TiltSpeed) * m_MaxTilt; //Set new rotation's x to sin of time * by tilt speed. The value is multiplied by max tilt
        currentRotation.x = Mathf.Clamp(currentRotation.x, -m_MaxTilt, m_MaxTilt); //Clamp tilt in X axis to the max tilt and the -max tilt
        currentRotation.z = Mathf.Clamp(currentRotation.z, 0, 0); //Clamp z axis to 0
        transform.rotation = Quaternion.Euler(currentRotation);
    }

    void RotateAroundPoint(Vector3 _Point, Vector3 _Pivot, Vector3 _Angles)
    {
        Vector3 dir = _Point - _Pivot; //Obtain direction relative to the pivot
        dir = Quaternion.Euler(_Angles) * dir; //Rotate direction
        _Point = dir + _Pivot; //Calculate rotation
        transform.position = _Point;
    }
}
