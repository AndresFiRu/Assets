using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotatePlatform : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Max Rotation in degrees")]
    [Range(10, 180)]
    public int m_maxRotation = 30;

    [Header("Rotation speed")]
    [Range(1, 100)]
    public int m_velocity = 5;

    [Header("Rotation direction (Clockwise - Counterclockwise)")]
    [Range(0, 1)]
    public int m_direction = 1;

    void Start()
    {
        if(m_direction == 0)
        {
            m_direction = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
         if(m_direction == 1 && this.transform.localEulerAngles.z > m_maxRotation && this.transform.rotation.z > 0)
        {
            this.m_direction = -1;
        }
        if(m_direction == -1 && ((360-this.transform.localEulerAngles.z) > m_maxRotation) && this.transform.rotation.z < 0)
        {
            this.m_direction = 1;
        }

        this.transform.Rotate(new Vector3(0, 0, m_velocity * m_direction) * Time.deltaTime);

    }
}
