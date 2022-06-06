using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class SatelliteMove : MonoBehaviour
{
    //camera shake attributes
    public float magnitude = 2f;
    public float rough = 8f;
    public float fadeIn = 0.1f;
    public float fadeOut = 0.2f; 

    private float outline;
    
    private SpriteRenderer spriteRenderer;

    private Vector2 originalPosition;
    public LayerMask locationLayer;
    public LayerMask satLayer;

    void Start()
    {
        originalPosition = transform.position;

        spriteRenderer = transform.Find("Satellite").GetComponent<SpriteRenderer>();
        if (spriteRenderer.material != null)
            spriteRenderer.material = Instantiate(spriteRenderer.material);
    }

    void OnMouseDown()
    {
        originalPosition = transform.position;
    }

    void OnMouseDrag()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float xMousePos = Mathf.Round(mousePos.x);
        float yMousePos = Mathf.Round(mousePos.y);

        //getting kinda grid for x
        if(Modulus(Mathf.Round(mousePos.x), 3) == 1)
        {
            xMousePos--;
        }
        else if(Modulus(Mathf.Round(mousePos.x), 3) == 2)
        {
            xMousePos++;
        }
        
        //getting kinda grid for y
        if(Modulus(Mathf.Round(mousePos.y), 3) == 1)
        {
            yMousePos--;
        }
        else if(Modulus(Mathf.Round(mousePos.y), 3) == 2)
        {
            yMousePos++;
        }

        Vector2 spawnPos = new Vector2(xMousePos, yMousePos);

        if( Physics2D.OverlapCircle(spawnPos, 0.2f, locationLayer) )
        {
            //this is where i am stopping the player from moving the satellite
            if(!Physics2D.OverlapCircle(spawnPos, 0.2f, satLayer) && RaycastThrow.isInputEnabled)
            {
                //this is where i am actually moving the satellite to
                //the new postition if the position is moveable to
                //this is where to apply the camera shake
                CameraShaker.Instance.ShakeOnce(magnitude, rough, fadeIn, fadeOut);

                //playing the audio
                AudioManager.instance.Play("SatMove");

                transform.position = spawnPos;
            }
        }

        //outlining the sprite
        spriteRenderer.material.SetFloat("_OutlineShow", 1f); 
    }

    void OnMouseUp()
    {
        spriteRenderer.material.SetFloat("_OutlineShow", 0f);
    }

    float Modulus(float a,float b)
    {
        return (a % b + b) % b;
    }
}
