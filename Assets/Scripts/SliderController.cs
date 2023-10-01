using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    enum OperationType
    {
        //角色移动相关
        MoveLeft,
        MoveRight,
        Jump,
        //条移动相关
        Invert,
        Replay,

        None,
    }

    [System.Serializable]
    struct FillerInfo
    {
        [SerializeField]
        [Tooltip("填充区域的sprite")]
        public Sprite fillerSprite;
        [SerializeField]
        [Tooltip("指示器的sprite")]
        public Sprite indicatorSprite;
        [SerializeField]
        public float length;
        [SerializeField]
        public OperationType operation;
        [SerializeField]
        [Tooltip("填充区域的颜色")]
        public Color color;
    }

    [SerializeField]
    FillerInfo[] fillerInfos;
    [SerializeField]
    GameObject fillerPrefab;
    GameObject[] fillers;
    [SerializeField]
    GameObject indicatorPrefab;
    GameObject[] indicators;

    Dictionary<float,OperationType> opList;

    RectTransform fillingAreaRect;
    RectTransform indicatorAreaRect;
    RectTransform pointerRect;
    float pointerWidth;
    bool pointerMovingRight;

    PlayerInputHandler inputHandler;

    bool inverted=false,replayed=false;

    [SerializeField]
    float pointerMoveSpeed;

    void Awake()
    {
        fillingAreaRect=transform.Find("Fill Area").gameObject.GetComponent<RectTransform>();
        indicatorAreaRect=transform.Find("Indicator Area").gameObject.GetComponent<RectTransform>();
        pointerRect=transform.Find("Fill Area/Pointer").gameObject.GetComponent<RectTransform>();
        inputHandler=FindObjectOfType<PlayerInputHandler>();
        pointerWidth=pointerRect.rect.width;
        opList=new Dictionary<float, OperationType>();

        InitFillers();
        InitPointer();
        UpdateFillerAndIndicator();
    }

    void Start()
    {
    }

    void Update()
    {
        //UpdateFiller();
        //CheckSpaceDoubleTap();
        UpdatePointer();
        CheckSpacePress();
    }

    void InitFillers()
    {
        fillers=new GameObject[fillerInfos.Length];
        indicators=new GameObject[fillerInfos.Length];
        for(int i=0;i<fillerInfos.Length;i++)
        {
            fillers[i]=Instantiate(fillerPrefab,fillingAreaRect);
            fillers[i].GetComponent<UnityEngine.UI.Image>().sprite=fillerInfos[i].fillerSprite;
            fillers[i].GetComponent<UnityEngine.UI.Image>().color=fillerInfos[i].color;
            indicators[i]=Instantiate(indicatorPrefab,indicatorAreaRect);
            indicators[i].GetComponent<UnityEngine.UI.Image>().sprite=fillerInfos[i].indicatorSprite;
            indicators[i].GetComponent<UnityEngine.UI.Image>().preserveAspect=true;
        }
        UpdateFillerAndIndicator();
        
    }

    void UpdateFillerAndIndicator()
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
            indicators[i].GetComponent<RectTransform>().anchoredPosition=new Vector2((2*nowX+fillerWidth)/2,0);
            nowX+=fillerWidth;
            opList.TryAdd(nowX,fillerInfos[i].operation);
        }
    }

    void InitPointer()
    {
        pointerRect.anchoredPosition=new Vector2(0,0);
        pointerMovingRight=true;
        pointerRect.SetAsLastSibling();
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
            ApplyOperation(OperationType.None,OperationType.None);
            return;
        }
        float leftPos=pointerRect.anchoredPosition.x;
        float rightPos=pointerRect.anchoredPosition.x+pointerWidth;
        OperationType leftOp=OperationType.None,rightOp=OperationType.None;
        foreach(var i in opList)
        {
            if(leftPos<i.Key+0.5f)
            {
                leftOp=i.Value;
                break;
            }
        }
        foreach(var i in opList)
        {
            if(rightPos<i.Key+0.5f)
            {
                rightOp=i.Value;
                break;
            }
        }
        ApplyOperation(leftOp,rightOp);
    }

    void CheckSpaceDoubleTap()
    {
        if(!inputHandler.SpaceDoubleTapInput)
            return;
        pointerMovingRight=!pointerMovingRight;
        inputHandler.UseSpaceDoubleTapInput();
    }

    void ApplyOperation(OperationType op1,OperationType op2)
    {
        if(op1==op2 && op2==OperationType.None)
        {
            replayed=inverted=false;
        }

        //角色移动相关
        Vector2 moveDir=Vector2.zero;
        bool jump=false;

        if(op1==OperationType.MoveLeft)
            moveDir+=Vector2.left;
        else if(op1==OperationType.MoveRight)
            moveDir+=Vector2.right;
        else if(op1==OperationType.Jump)
            jump=true;

        if(op2==OperationType.MoveLeft)
            moveDir+=Vector2.left;
        else if(op2==OperationType.MoveRight)
            moveDir+=Vector2.right;
        else if(op2==OperationType.Jump)
            jump=true;

        inputHandler.OnMoveInput(moveDir);
        if(jump)
            inputHandler.OnJumpInput();

        //条移动相关
        bool replay=false;
        bool invert=false;
        if(op1==OperationType.Replay)
            replay=true;
        else if(op1==OperationType.Invert)
            invert=true;
        if(op2==OperationType.Replay)
            replay=true;
        else if(op2==OperationType.Invert)
            invert=true;
        if(replay && !replayed)
        {
            replayed=true;
            PointerReplay();
        }
        if(invert && !inverted)
        {
            inverted=true;
            PointerInvert();
        }
    }

    void PointerReplay()
    {
        pointerRect.anchoredPosition=new Vector2(0,pointerRect.anchoredPosition.y);
        pointerMovingRight=true;
    }

    void PointerInvert()
    {
        pointerMovingRight=!pointerMovingRight;
    }
}
