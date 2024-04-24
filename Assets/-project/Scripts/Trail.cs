using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public GameObject trail;
    public float startTimeSpawn;
    public float selfDestruct;
    float timeSpawn;
    Rigidbody2D rb2D;
    Player player;
    public float velocidad;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }
    void Update()
    {

        if (player.speed > 200)
        {
            startTimeSpawn -= 0.05f * velocidad * Time.deltaTime;
            startTimeSpawn = Mathf.Max(startTimeSpawn, 0.05f);
        }


        if (player.speed > 100 && player.speed < 200)
        {
            if (player.isGrounded())
            {
                startTimeSpawn = 0.25f;
            }
            else
            {
                startTimeSpawn = 0.1f;
            }
        }

        if (!GameManager.stopPlayer)
        {
            if (rb2D.velocity.magnitude > 0.1f)
            {
                if (timeSpawn <= 0)
                {
                    GameObject instance = Instantiate(trail, transform.position, Quaternion.identity);
                    Destroy(instance, selfDestruct);
                    timeSpawn = startTimeSpawn;
                }
                else
                {
                    timeSpawn -= Time.deltaTime;
                }
            }
        }
    }
}
