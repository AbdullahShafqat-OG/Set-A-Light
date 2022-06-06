using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DottedLineDemo : MonoBehaviour
{
    public Transform endPoint;
    
    void Update()
    {
        //Creating a dotted line
        DottedLine.DottedLine.Instance.DrawDottedLine(transform.position, endPoint.position);
    }
}
