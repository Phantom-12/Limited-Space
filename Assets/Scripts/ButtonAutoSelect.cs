using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAutoSelect : MonoBehaviour
{
    [SerializeField]
    Button button;

    void OnEnable()
    {
        button.Select();
    }
}
