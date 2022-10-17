using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("对话系统")]
    public Image characterImage;
    public Text nowText;
    public GameObject talkUi;
    private string[] thisLevelTalk;
    public int index;

    Dictionary<string,Color> chartacterImageDic=new Dictionary<string, Color>();
    void Start()
    {
        thisLevelTalk=LoadSpeakText();
        chartacterImageDic.Add("A@",Color.red);
        chartacterImageDic.Add("B@",Color.blue);
        index=0;
    }

    private void OnEnable() 
    {
        //Time.timeScale=0f;
    }
    // Update is called once per frame
    void Update()
    {
        OnAndOffTalkUi();
        UpdateTalkUi();
    }

    private void OnAndOffTalkUi()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            talkUi.SetActive(!talkUi.activeSelf);
            if(talkUi.activeSelf)
                Time.timeScale=0f;
            if(!talkUi.activeSelf)
                Time.timeScale=1f;
            index=0;
            switch(thisLevelTalk[index])
            {
                case"A@":
                    characterImage.color=chartacterImageDic[thisLevelTalk[index]];
                    index++;
                break;
                case"B@":
                    characterImage.color=chartacterImageDic[thisLevelTalk[index]];
                    index++;
                break;  
            }
            nowText.text=thisLevelTalk[index];
            index++;
        }
    }

    private void UpdateTalkUi()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(index>=thisLevelTalk.Length-1)
            {
                Time.timeScale=1f;
                talkUi.SetActive(false);
                return;
            }
            switch(thisLevelTalk[index])
            {
                case"A@":
                    characterImage.color=chartacterImageDic[thisLevelTalk[index]];
                    index++;
                break;
                case"B@":
                    characterImage.color=chartacterImageDic[thisLevelTalk[index]];
                    index++;
                break;  
            }
            nowText.text=thisLevelTalk[index];
            index++;
        }
    }

    private string[] LoadSpeakText()
    {
        TextAsset bindData=Resources.Load("levelOneTalk") as TextAsset;
        string data=bindData.text.Replace(Environment.NewLine,string.Empty);
        return data.Split('-');
    }
}
