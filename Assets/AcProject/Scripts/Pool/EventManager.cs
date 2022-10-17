using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static EventManager instance;
    public Dictionary<string,Action<object>> eventDic=new Dictionary<string,Action<object>>();
    private Dictionary<string,Action> UiDic=new Dictionary<string,Action>();
    private void Awake() 
    {
        if(instance==null)
        {
            instance=this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
/// <summary>
/// 添加事件
/// </summary>
/// <param name="name"></param>
/// <param name="action"></param>
    public void AddEventListener(string name,Action<object> action)
    {
        if(eventDic.ContainsKey(name))
            eventDic[name]+=action;
        else
            eventDic.Add(name,action);

    }
    public void AddEventListener(string name,Action action)
    {
        if(UiDic.ContainsKey(name))
            UiDic[name]+=action;
        else
            UiDic.Add(name,action);

    }
    /// <summary>
    /// 移除事件
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void RemoveEventListener(string name,Action<object> action)
    {
        if(eventDic.ContainsKey(name))
            eventDic[name]-=action;
    }
    public void Clear()
    {
        eventDic.Clear();
    }
/// <summary>
/// 调用事件
/// </summary>
/// <param name="name"></param>
/// <param name="objectinfo"></param>
    public void EventTrigger(string name,object objectinfo)
    {
        if(eventDic.ContainsKey(name))
        {
            eventDic[name]?.Invoke(objectinfo);
        }

    }
    public void EventTrigger(string name)
    {
        //Debug.Log(UiDic.ContainsKey(name));
        if(UiDic.ContainsKey(name))
        {
            UiDic[name].Invoke();
        }

    }
}
