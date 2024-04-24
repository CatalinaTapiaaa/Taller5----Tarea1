using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, 100 * Time.deltaTime);
        }
    }
}
