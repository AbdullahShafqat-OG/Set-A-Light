using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickOrbit : MonoBehaviour
{
    void OnMouseDown()
    {
        transform.root.gameObject.GetComponent<SatelliteRotateNew>().OnMouseDown();
    }
}
