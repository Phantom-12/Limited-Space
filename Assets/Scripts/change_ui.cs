using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class change_ui : MonoBehaviour
{
    bool isShowTip;
    public bool Windowshow = false;
    // Start is called before the first frame update
    void Start()
    {
        isShowTip = false;
    }

private  void OnMouseEnter()
    {
        isShowTip = true;
    }

    private void OnGUI()
    {
        if (isShowTip)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 30;
            style.normal.textColor = Color.red;
            GUI.Label(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, 400, 50), "Cube", style);

        }
        if (Windowshow)
            GUI.Window(0, new Rect(30, 30, 200, 100), MyWindow, "Cube");
    }

    private void MyWindow(int id)
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        GUILayout.Label("a");
    }


}
