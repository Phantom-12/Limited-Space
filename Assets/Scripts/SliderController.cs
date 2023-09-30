using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    enum OperationType
    {
        MoveLeft,
        MoveRight,
        Jump,
        None,
    }

    [System.Serializable]
    struct FillerInfo
    {
        [SerializeField]
        public Sprite sprite;
        [SerializeField]
        public float length;
        [SerializeField]
        public OperationType operation;
        [SerializeField]
        public Color color;
    }

    [SerializeField]
    FillerInfo[] fillerInfos;
    [SerializeField]
    GameObject fillerPrefab;
    GameObject[] fillers;

    Dictionary<float,OperationType> opList;


    Transform fillingArea;
    RectTransform fillingAreaRect;
    RectTransform pointerRect;
    float pointerWidth;
    bool pointerMovingRight;

    PlayerInputHandler inputHandler;

    [SerializeField]
    float pointerMoveSpeed;

    void Awake()
    {
        fillingArea=transform.Find("Fill Area");
        fillingAreaRect=fillingArea.gameObject.GetComponent<RectTransform>();
        pointerRect=transform.Find("Pointer").gameObject.GetComponent<RectTransform>();
        inputHandler=FindObjectOfType<PlayerInputHandler>();
        pointerWidth=pointerRect.rect.width;
        opList=new Dictionary<float, OperationType>();

        InitFillers();
        InitPointer();
        UpdateFiller();
    }

    void Start()
    {
    }

    void Update()
    {
        //UpdateFiller();
        CheckSpaceDoubleTap();
        UpdatePointer();
        CheckSpacePress();
    }

    void InitFillers()
    {
        fillers=new GameObject[fillerInfos.Length];
        for(int i=0;i<fillerInfos.Length;i++)
        {
            fillers[i]=Instantiate(fillerPrefab,fillingArea);
            fillers[i].GetComponent<UnityEngine.UI.Image>().sprite=fillerInfos[i].sprite;
            fillers[i].GetComponent<UnityEngine.UI.Image>().color=fillerInfos[i].color;
        }
        UpdateFiller();
    }

    void UpdateFiller()
    {
        float width=fillingAreaRect.rect.width;
        float height=fillingAreaRect.rect.height;
        float sumLen=0;
        foreach(var i in fillerInfos)
        {
            sumLen+=i.length;
        }
        float nowX=0;
        for(int i=0;i<fillerInfos.Length;i++)
        {
            float fillerWidth=width*fillerInfos[i].length/sumLen;
            float fillerHeight=height;
            fillers[i].GetComponent<RectTransform>().offsetMin=new Vector2(nowX,0);
            fillers[i].GetComponent<RectTransform>().offsetMax=new Vector2(nowX+fillerWidth,fillerHeight);
            // fillers[i].GetComponent<RectTransform>().anchoredPosition=new Vector2(nowX,0);
            nowX+=fillerWidth;
            opList.TryAdd(nowX,fillerInfos[i].operation);
        }
    }

    void InitPointer()
    {
        pointerRect.anchoredPosition=new Vector2(0,0);
        pointerMovingRight=true;
    }

    void UpdatePointer()
    {
        if(pointerMovingRight)
        {
            float newX=pointerRect.anchoredPosition.x+pointerMoveSpeed*Time.deltaTime;
            pointerRect.anchoredPosition=new Vector2(newX,pointerRect.anchoredPosition.y);
            if(newX+pointerWidth>fillingAreaRect.rect.width)
                pointerMovingRight=false;
        }
        else
        {
            float newX=pointerRect.anchoredPosition.x-pointerMoveSpeed*Time.deltaTime;
            pointerRect.anchoredPosition=new Vector2(newX,pointerRect.anchoredPosition.y);
            if(newX<0)
                pointerMovingRight=true;
        }
    }

    void CheckSpacePress()
    {
        if(!inputHandler.SpaceHoldInput)
        {
            ApplyOperation(OperationType.None);
            return;
        }
        float leftPos=pointerRect.anchoredPosition.x;
        float rightPos=pointerRect.anchoredPosition.x+pointerWidth;
        foreach(var i in opList)
        {
            if(leftPos<i.Key+0.5f)
            {
                ApplyOperation(i.Value);
                break;
            }
        }
        foreach(var i in opList)
        {
            if(rightPos<i.Key+0.5f)
            {
                ApplyOperation(i.Value);
                break;
            }
        }
    }

    void CheckSpaceDoubleTap()
    {
        if(!inputHandler.SpaceDoubleTapInput)
            return;
        pointerMovingRight=!pointerMovingRight;
        inputHandler.UseSpaceDoubleTapInput();
    }

    void ApplyOperation(OperationType op)
    {
        if(op==OperationType.MoveLeft)
            inputHandler.OnMoveInput(Vector2.left);
        else if(op==OperationType.MoveRight)
            inputHandler.OnMoveInput(Vector2.right);
        else if(op==OperationType.Jump)
            inputHandler.OnJumpInput();
        else if(op==OperationType.None)
            inputHandler.OnMoveInput(Vector2.zero);
    }
}
