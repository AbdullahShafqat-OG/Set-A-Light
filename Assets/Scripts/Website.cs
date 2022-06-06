using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Website : MonoBehaviour
{
    //"https://am-educated-vegtable.itch.io/"
    //"https://www.cade-conkle.com/"
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnMouseEnter()
    {
        AudioManager.instance.Play("ButtonHover");
        spriteRenderer.color = new Color(165f/255f, 159f/255f, 43f/255f);
    }

    void OnMouseExit()
    {
        spriteRenderer.color = Color.white;
    }

    void OnMouseDown()
    {
        Application.OpenURL(this.name);
    }
}
