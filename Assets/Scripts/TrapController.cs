using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    Player player;
    void Awake()
    {
        player=FindObjectOfType<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            player.Die();
        }
    }
}
