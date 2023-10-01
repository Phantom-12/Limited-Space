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
    private float beatTimeLength;

    private int beatCount;
    private int ActionCount;
    private PlayerInputHandler inputHandler;
    private bool started;

    [System.Serializable]
    struct ActionInfo
    {
        [SerializeField]
        [Tooltip("距离上次动作的节拍数")]
        public int beatsGap;
        [SerializeField]
        [Tooltip("操作类型（按下/松开/生成块）")]
        public int type;
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
            InvokeRepeating("Check", 0.0f, beatTimeLength);
            started=true;
        }
    }

    public void StartTest()
    {
        InvokeRepeating("Check", 0.0f, beatTimeLength);
        started=true;
    }

    public void Check()
    {
        if(onDev){
            while(ActionCount<Actions.Length){
                if(beatCount==Actions[ActionCount].beatsGap){
                    if(Actions[ActionCount].type==1){
                        inputHandler.OnSpaceHoldDev(true);
                    }else if(Actions[ActionCount].type==2){
                        inputHandler.OnSpaceHoldDev(false);
                    }else if(Actions[ActionCount].type==3){
                        GenerateBlock();
                    }
                    ActionCount++;
                    beatCount=0;
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
