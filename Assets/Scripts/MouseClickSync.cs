using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickSync : MonoBehaviour
{
    void OnMouseDown()
    {
        transform.root.gameObject.GetComponent<SatelliteRotateSync>().OnMouseDown();
    }
}
