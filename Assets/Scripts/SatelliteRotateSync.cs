using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteRotateSync : MonoBehaviour
{
    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    private Transform anyChild;

    // Start is called before the first frame update
    void Start()
    {
        int count = 0;
        foreach (Transform child in transform)
        {
            if(!anyChild)
            {
                anyChild = child;
            }

            spriteRenderers.Add(child.GetComponent<SpriteRenderer>());
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(!RaycastThrow.satsInFocus.Contains(anyChild.gameObject))
            {
                for(int i = 0; i < spriteRenderers.Count; i++)
                {
                    spriteRenderers[i].material.SetFloat("_OutlineShow", 0f);
                }
            }
        }
    }

    public void OnMouseDown()
    {
        for(int i = 0; i < spriteRenderers.Count; i++)
        {
            spriteRenderers[i].material.SetFloat("_OutlineShow", 1f);
        }

        RaycastThrow.satsInFocus.Clear();
        foreach (Transform child in transform)
        {
            RaycastThrow.satsInFocus.Add(child.gameObject);
        }
    }
}
