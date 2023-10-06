using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    ExitDoor door;

    void OnTriggerEnter2D(Collider2D other)
    {
       door.GetKey();
       gameObject.SetActive(false);
    }
}
