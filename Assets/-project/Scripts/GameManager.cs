using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject panelPause;
    public Animator aniTransition;
    public Camera cam;
    public Image bar;
    [Header("Level")]
    public float addSpeedPlayer;
    public float addSpeedTimeBar;
    public float shakingDuration;
    [Space]
    public int actualLevel;
    public int nextLevel;

    public static bool victory, death, stopPlayer;
    public static float speedPlayer;
    bool desactive = true, shaking;
    float t = 100;

    void Awake()
    {
        victory = false;
        death = false;
        stopPlayer = true;
        speedPlayer = 0;
    }
    void Start()
    {
        StartCoroutine(StartGame());
    }
    void Update()
    {
        if (!desactive)
        {
            t -= addSpeedTimeBar * Time.deltaTime;
            speedPlayer = addSpeedPlayer;
        }

        if (victory && !desactive)
        {
            Victory();
            panelPause.SetActive(false);
            desactive = true;
        }
        if (t < 0 && !desactive)
        {
            GameOver();
            panelPause.SetActive(false);
            desactive = true;
        }    
        if (death && !desactive)
        {
            GameOver();
            panelPause.SetActive(false);
            desactive = true;
        }

        bar.fillAmount = t / 100;
    }   

    void GameOver()
    {
        StartCoroutine(Tremor());
        FindObjectOfType<Player>().isDeath();
    }
    void Victory()
    {
        StartCoroutine(EndVictory());
        stopPlayer = true;
    }

    IEnumerator Tremor()
    {
        if (shaking)
        {
            yield return null;
        }

        shaking = true;
        Vector3 originalPos = cam.transform.position;
        float temblorCantidad = 0.2f;
        float elapsed = 0;

        while (elapsed < shakingDuration)
        {
            float x = Random.Range(-1f, 1f) * temblorCantidad;
            float y = Random.Range(-1f, 1f) * temblorCantidad;
            cam.transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cam.transform.localPosition = originalPos;
        shaking = false;
        StartCoroutine(EndGameOver());
    }


    IEnumerator StartGame()
    {
        aniTransition.SetBool("Active", true);
        yield return new WaitForSeconds(1);
        stopPlayer = false;
        desactive = false;
        yield return null;
    }
    IEnumerator EndGameOver()
    {
        stopPlayer = true;
        yield return new WaitForSeconds(1);
        aniTransition.SetBool("Active", false);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(actualLevel);
        yield return null;
    }
    IEnumerator EndVictory()
    {
        yield return new WaitForSeconds(1);
        aniTransition.SetBool("Active", false);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nextLevel);
        yield return null;
    }
}
