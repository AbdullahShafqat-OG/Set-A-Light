using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitionController : MonoBehaviour
{
    //this is for making the function of this accessible everywhere
    public static SceneTransitionController instance;

    public float transitionSpeed = 1f;
    public Transform moonTransform;

    private Image theImage;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        theImage = GetComponent<Image>();
        theImage.transform.position = moonTransform.position;

        theImage.material.SetFloat("_Cutoff", 1.1f);
        
        gameObject.SetActive(false);
    }

    IEnumerator MoveTowards(float initialValue, float finalValue, float duration, bool loadNextLevel)
    {
        float counter = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float currentValue = theImage.material.GetFloat("_Cutoff");

            float time = Mathf.Abs(finalValue - currentValue) / (duration - counter) * Time.deltaTime;

            theImage.material.SetFloat("_Cutoff", Mathf.MoveTowards(theImage.material.GetFloat("_Cutoff"), finalValue, time));

            yield return null;
        }

        if(loadNextLevel)
        {
            if(SceneManager.GetActiveScene().name != "Level End")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            } else
            {
                Application.Quit();
            }
        }
    }

    public void NextLevel()
    {
        gameObject.SetActive(true);
        StartCoroutine(MoveTowards(theImage.material.GetFloat("_Cutoff"), -0.1f - theImage.material.GetFloat("_Smoothing"), transitionSpeed, true));
    }
}
