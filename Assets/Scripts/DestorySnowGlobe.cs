using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DestorySnowGlobe : MonoBehaviour
{
    Image SG;
    private void Start()
    {
        SG = GameObject.Find("SG").GetComponent<Image>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SG.color = new Color(255, 255, 255, 1);
            Destroy(gameObject);
        }
    }
}
