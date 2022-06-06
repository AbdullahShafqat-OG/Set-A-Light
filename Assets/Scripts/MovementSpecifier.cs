using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpecifier : MonoBehaviour
{
    public GameObject moveableIndicator;
    [Tooltip("X and Y values increments of 3")]
    public Vector2[] satMovePositions;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < satMovePositions.Length; i++)
        {
            Instantiate(moveableIndicator, satMovePositions[i], Quaternion.identity);
        }
    }
}
