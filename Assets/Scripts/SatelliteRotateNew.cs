using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SatelliteRotateNew : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = transform.Find("Satellite").GetComponent<SpriteRenderer>();
        if (spriteRenderer.material != null)
            spriteRenderer.material = Instantiate(spriteRenderer.material);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(RaycastThrow.satsInFocus.Any())
            {
                if(RaycastThrow.satsInFocus[0] != this.gameObject)
                {
                    spriteRenderer.material.SetFloat("_OutlineShow", 0f);
                }
            }
        }
    }

    public void OnMouseDown()
    {
        spriteRenderer.material.SetFloat("_OutlineShow", 1f);

        RaycastThrow.satsInFocus.Clear();
        RaycastThrow.satsInFocus.Add(this.gameObject);
    }
}
