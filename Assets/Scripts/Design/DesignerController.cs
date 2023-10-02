using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class DesignerController : MonoBehaviour
{
    [SerializeField]
    public GameObject BlockPrefeb;
    [SerializeField]
    public GameObject Player;
    [SerializeField]
    private bool onDev;
    [SerializeField]
    [Tooltip("循环执行的时间间隔")]
    private float TimeLength;

    private int beatCount;
    private int ActionCount;
    private PlayerInputHandler inputHandler;
    private bool started;

    private enum DevType
    {
        None,
        SpacePressed,
        SpaceRelease,
        GenerateBlock,
    }

    [System.Serializable]
    struct ActionInfo
    {
        [SerializeField]
        [Tooltip("距离开始的时间间隔数")]
        public int beatsGap;
        [SerializeField]
        [Tooltip("操作类型（按下/松开/生成块）")]
        public DevType operation;
        [Tooltip("注释")]
        public string comment;
    }
    
    [SerializeField]
    private ActionInfo[] Actions;
    
    // Start is called before the first frame update
    void Start()
    {
        beatCount=0;
        ActionCount=0;
        inputHandler=Player.GetComponent<PlayerInputHandler>();
        if(onDev)
            StartTest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTest(InputAction.CallbackContext context)
    {
        if(onDev && context.performed && !started){
            InvokeRepeating("Check", 0.0f, TimeLength);
            started=true;
        }
    }

    public void StartTest()
    {
        InvokeRepeating("Check", 0.0f, TimeLength);
        started=true;
    }

    public void Check()
    {
        if(onDev){
            while(ActionCount<Actions.Length){
                if(beatCount>=Actions[ActionCount].beatsGap){
                    Debug.Log(ActionCount);
                    if(Actions[ActionCount].operation==DevType.SpacePressed){
                        inputHandler.OnSpaceHoldDev(true);
                    }else if(Actions[ActionCount].operation==DevType.SpaceRelease){
                        inputHandler.OnSpaceHoldDev(false);
                    }else if(Actions[ActionCount].operation==DevType.GenerateBlock){
                        GenerateBlock();
                    }
                    ActionCount++;
                }else{
                    beatCount++;
                    break;
                }
            }
        }
    }
    
    public void GenerateBlock()
    {
        if(onDev){
            Vector3 blockPosition=Player.transform.position;
            blockPosition.y-=(Player.GetComponent<BoxCollider2D>().size.y/2+BlockPrefeb.GetComponent<BoxCollider2D>().size.y/2);
            Instantiate(BlockPrefeb,blockPosition,Quaternion.identity);
        }
    }
}
