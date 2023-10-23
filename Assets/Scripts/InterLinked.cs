using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InterLinked : MonoBehaviour
{
    public Image interLinked;
    public Image tip;
    private List<Sprite> space;
    private GameObject Cwindow;
    private GameObject Fwindow;
    private GameObject Pwindow;
    // Start is called before the first frame update
    void Awake()
    {
        space = new List<Sprite>();
        space.Add(Resources.Load<Sprite>("space1"));
        space.Add(Resources.Load<Sprite>("space2"));
        space.Add(Resources.Load<Sprite>("space3"));
    }

    // Update is called once per frame
    void Update()
    {
        Cwindow = GameObject.Find("CompleteWindow");
        Fwindow = GameObject.Find("FailedWindow");
        Pwindow = GameObject.Find("PauseWindow");
        if (Cwindow == null && Fwindow == null && Pwindow == null)
        {
            if (Input.GetKey(KeyCode.Space)||Input.GetButton("Fire1"))
            {
                Destroy(tip);
                interLinked.sprite = space[2];
            }
            if (Input.GetKeyUp(KeyCode.Space)|| Input.GetButtonUp("Fire1"))
            {
                interLinked.sprite = space[1];
                interLinked.sprite = space[0];
            }
        }

    }
}
