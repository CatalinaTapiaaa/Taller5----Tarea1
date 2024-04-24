using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject trail, player;
    public Animator aniCinematic, aniPlayer;
    bool desactiveInput, desactiveTrail;
    float timeSpawn;

    void Update()
    {
        if (!desactiveInput)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Cinematic());
                desactiveInput = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
                desactiveInput = true;
            }
        }

        if (!desactiveTrail)
        {
            if (timeSpawn <= 0)
            {
                GameObject instance = Instantiate(trail, player.transform.position, Quaternion.identity);
                Destroy(instance, 1);
                timeSpawn = 0.05f;
            }
            else
            {
                timeSpawn -= Time.deltaTime;
            }
        }      
    }

    IEnumerator Cinematic()
    {
        aniCinematic.SetBool("Cinematic", true);
        yield return new WaitForSeconds(4);
        aniPlayer.SetBool("Stop", true);
        desactiveTrail = true;
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(1);
        yield return null;
    }
}
