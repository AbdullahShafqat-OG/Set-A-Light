using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
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

    //make it so that the gameobject is named according
    //to the scene that it takes to
    void OnMouseDown()
    {
        AudioManager.instance.Play("ButtonClick");
        SceneManager.LoadScene(this.name);
    }
}
