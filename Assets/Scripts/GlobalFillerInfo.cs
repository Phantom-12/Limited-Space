using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newGlobalFillerInfo",menuName ="Data/Slider Data/Global Filler Info")]
public class GlobalFillerInfo : ScriptableObject
{
    [System.Serializable]
    public struct Item
    {
        [SerializeField]
        public OperationType operation;
        [SerializeField]
        [Tooltip("填充区域的sprite")]
        public Sprite fillerSprite;
        [SerializeField]
        [Tooltip("指示器的sprite")]
        public Sprite indicatorSprite;
        [SerializeField]
        [Tooltip("填充区域的颜色")]
        public Color color;
    }

    [SerializeField]
    Item[] items;

    public Dictionary<OperationType,Item> info;

    public void CreateDic()
    {
        if(info is not null)
            return;
        info=new Dictionary<OperationType, Item>();
        foreach(var i in items)
        {
            info.TryAdd(i.operation,i);
        }
    }
}
