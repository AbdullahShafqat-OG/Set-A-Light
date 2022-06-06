using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class AstronautColorHue : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    public float changeTime = 1;

    private float hue;
    private float saturation = 100;
    private float value = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        hue = Random.Range(0f, 360f);
        
        CameraShaker.Instance.StartShake(1f, 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.color = Color.HSVToRGB(hue/360, saturation/100, value/100);
        
        hue = Mathf.MoveTowards(hue, 360, changeTime * Time.deltaTime);
        if(hue >= 360)
        {
            hue = 0;
        }
    }
}
