using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;
using UnityEngine.SceneManagement;

public class RaycastThrow : MonoBehaviour
{
    //this is for the sound system of the game
    private int isRotating = 0;
    private int hasWon = 0;

    //this is for control over the speed of rotating the satellites
    public float rotationSpeed = 50f;
    //the slowing down speed factor
    [Range(0.1f, 1f)]
    public float rotationSpeedSlow = 0.5f;
    private bool isShiftKeyDown;

    //this is the transition image
    private float endLevelWait = 2f;
    
    //camera shake attributes
    private float magnitude = 0.1f;
    private float rough = 5f;
    private float fadeIn = 0.1f;
    private float fadeOut = 0.5f;

    //constant camera shake attributes
    private float c_magnitude = 1f;
    private float c_rough = 0.1f;
    private float c_fadeIn = 0.1f;

    //to stop getting input when won the level
    [HideInInspector]
    public static bool isInputEnabled = true;

    //this is the mutliple sats 
    [HideInInspector]
    public static List<GameObject> satsInFocus = new List<GameObject>();

    //this is still in testing phases
    [Header("Layers ray should collide")]
    public LayerMask strikeLayer;

    public Transform fireTransform;
    public LineRenderer lineRenderer;

    public GameObject moon;
    public Material moonMat;
    
    //the number of reflections and length of the ray
    private int reflections = 30;
    private float maxLength = 100f;
    private float remainingLength;

    private Ray2D ray;
    private RaycastHit2D hit;
    private Vector2 direction;

    void Start()
    {
        CameraShaker.Instance.StartShake(c_magnitude, c_rough, c_fadeIn);

        isShiftKeyDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.enabled = true;
        Reflection();

        //handling the input stuff here
        isShiftKeyDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if(Input.GetAxisRaw("Horizontal") != 0 && satsInFocus != null && isInputEnabled)
        {
            isRotating++;
            RotateSatSync();
        } else if(Input.GetAxisRaw("Horizontal") == 0)
        {
            isRotating = 0;
        }

        if(Input.GetKeyDown(KeyCode.N))
        {
            if(SceneManager.GetActiveScene().name != "Level End")
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            } else
            {
                Debug.Log("quitting");
                Application.Quit();
            }
        }

        //playing the audio for rotation here
        if(isRotating == 1 && satsInFocus.Count > 0)
        {
            AudioManager.instance.Play("SatRotate");
        } else if(isRotating == 0)
        {
            AudioManager.instance.Stop("SatRotate");
        }
    }

    void Reflection()
    {
        ray = new Ray2D(fireTransform.position, fireTransform.right);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, fireTransform.position);

        remainingLength = maxLength;

        int layerMask = ~(LayerMask.GetMask("Location"));

        for(int i = 0; i < reflections; i++)
        {
            if(hit = Physics2D.Raycast(ray.origin, ray.direction, remainingLength, strikeLayer))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector2.Distance(ray.origin, hit.point);

                direction = Vector2.Reflect(ray.direction, hit.normal);
                ray = new Ray2D(hit.point + direction * 0.01f, Vector2.Reflect(ray.direction, hit.normal));
                if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Moon"))
                {
                    //calling the won function only once
                    if(hasWon == 0)
                    {
                        StartCoroutine(WonLevel());
                    }
                    break;
                } else if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Meteor"))
                {
                    break;
                }
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }
        }
    }

    void GoStraight()
    {
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
    }

    void RotateSatSync()
    {
        for(int i = 0; i < satsInFocus.Count; i++)
        {
            if(isShiftKeyDown)
            {
                CameraShaker.Instance.ShakeOnce(magnitude/2, rough, fadeIn, fadeOut);
                satsInFocus[i].transform.Rotate(Vector3.back * Input.GetAxisRaw("Horizontal") * Time.deltaTime * rotationSpeed * rotationSpeedSlow);
            } else
            {
                CameraShaker.Instance.ShakeOnce(magnitude, rough, fadeIn, fadeOut);
                satsInFocus[i].transform.Rotate(Vector3.back * Input.GetAxisRaw("Horizontal") * Time.deltaTime * rotationSpeed);
            }
        }
    }

    IEnumerator WonLevel()
    {
        moon.GetComponent<SpriteRenderer> ().material = moonMat;

        hasWon++;
        if(hasWon == 1)
        {
            AudioManager.instance.Play("LevelComplete");
        }

        moon.transform.Find("Point Light 2D").gameObject.SetActive(true);

        isInputEnabled = false;

        yield return new WaitForSecondsRealtime(endLevelWait);

        hasWon++;

        isInputEnabled = true;

        SceneTransitionController.instance.NextLevel();
    }
}
