using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class ChangeButtonCon : MonoBehaviour,IRestLoad
{
    // Start is called before the first frame update
    public static ChangeButtonCon instance;
    public GameObject[] changeButton=new GameObject[5];
    private Text[] changeButtonText=new Text[5];
    private string[] nowButtonStr=new string[5];
    private Button NowButton;
    private int nowID;

    public static bool isChangeButton=false;

    private bool isChoseButton=false;

    public ButtonSet[] buttonSets=new ButtonSet[5];

    bool isUse=false;

    //public string testone="123456";
    void Start()
    {
       
    }

    private void Awake() 
    {
        //SetFirst();
    }
/// <summary>
/// 初始化按键设置
/// </summary>
    public void SetFirst()
    {   
        
         for (int i = 0; i < changeButton.Length; i++)
        {
            if(changeButton[i]!=null)
            { 
                changeButtonText[i]=changeButton[i].GetComponentInChildren<Text>();
                Debug.Log(changeButtonText[i].text);
            }
            
        }
        ChangeButtonStr();
        Debug.Log("按键配置完成");
    }
    private void OnEnable() 
    {
        isChangeButton=true;
        Time.timeScale=0f;
        for (int i = 0; i < changeButton.Length; i++)
        {
            if(changeButton[i]!=null)
            { 
                changeButtonText[i]=changeButton[i].GetComponentInChildren<Text>();
                //Debug.Log(changeButtonText[i].text);
            }
            
        }
        ChangeButtonStr();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeButton();
    }

    private void ChangeButtonStr()
    {
        for (int i = 0; i < nowButtonStr.Length; i++)
        {
            if(changeButtonText[i]!=null)
            {   
                //string str=PlayerPrefs.GetString(i.ToString());
                //Debug.Log(str);
                if(PlayerPrefs.HasKey(i.ToString()))
                {
                    nowButtonStr[i]=PlayerPrefs.GetString(i.ToString());;
                }
                else
                nowButtonStr[i]=changeButtonText[i].text;
                
                //Debug.Log(nowButtonStr[i]);
                buttonSets[i].buttonKecode=nowButtonStr[i];
                changeButtonText[i].text=nowButtonStr[i];
                buttonSets[i].buttonName=i;
            }
        }
        //if(CameraCon.instance!=null)
        //CameraCon.instance.GetButtonSet(buttonSets);
    }
/// <summary>
/// 选择按钮的方法
/// </summary>
/// <param name="num"></param>
    public void ChoseButton(int num)
    {
            nowID=num;
            
            isChoseButton=true;
            for (int i = 0; i < changeButton.Length; i++)
            {
                if(changeButton[i]!=null)
                {
                    if(nowID==i)
                    changeButton[i].GetComponent<Image>().color=Color.red;
                    else
                    changeButton[i].GetComponent<Image>().color=Color.white;
                }
                
            }
    }

    /*private void OnGUI()     
    {
        //isUse=false;
        if(Input.anyKeyDown&&isChoseButton)
        {
            Debug.Log("1");
            Event e=Event.current;
            //Debug.Log(Input.inputString.ToString());
            if(e.isKey&&e!=null&&e.keyCode!=KeyCode.None)
            {   
                //bool isUsed=false;
                isUse=false;
                string str=e.keyCode.ToString();
                //KeyCode E= (KeyCode)Enum.Parse(typeof(KeyCode),str);
                //str=E.ToString();
                Debug.Log(str);
                for (int i = 0; i < changeButtonText.Length; i++)
                {   
                    
                    if(i!=nowID&&changeButtonText[i]!=null)
                    {
                        if(changeButtonText[i].text==str)
                        {
                            Debug.Log("按键重复，请重新设置");
                            isUse=true;
                            break;
                        }

                    } 
                  
                }
                  if(isUse)
                    {
                        //isUse=!isUse;
                        return;
                    }
                    if(!isUse)
                    {    
                        Debug.Log(str);
                        changeButtonText[nowID].text=str;
                        Debug.Log(isUse);
                        Debug.Log("1111");
                        changeButton[nowID].GetComponent<Image>().color=Color.white;
                        isChoseButton=false;
                    } 
            }
        }
    }*/

    public void OnRest()
    {
        isChangeButton=false;
        Debug.Log(isChangeButton);
        Time.timeScale=1f;
    }
/// <summary>
/// 改变按钮的方法
/// </summary>
    private void ChangeButton()
    {
        if(isChoseButton)
        {
            foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
            {
                if(Input.GetKeyDown(key))
                {
                    //Debug.Log(key.ToString());
                    isUse=false;
                string str=key.ToString();
                //KeyCode E= (KeyCode)Enum.Parse(typeof(KeyCode),str);
                //str=E.ToString();
                Debug.Log(str);
                for (int i = 0; i < changeButtonText.Length; i++)
                {   
                    
                    if(i!=nowID&&changeButtonText[i]!=null)
                    {
                        if(changeButtonText[i].text==str)
                        {
                            Debug.Log("按键重复，请重新设置");
                            isUse=true;
                            break;
                        }

                    } 
                  
                }
                    if(isUse)
                    {
                        //isUse=!isUse;
                        return;
                    }
                    if(!isUse)
                    {    
                        //Debug.Log(str);
                        //changeButtonText[nowID].text=str;
                        //Debug.Log(isUse);
                        PlayerPrefs.SetString(nowID.ToString(),str);
                        //PlayerPrefs.Save();
                        changeButtonText[nowID].text=str;
                        Debug.Log("1111");
                        changeButton[nowID].GetComponent<Image>().color=Color.white;
                        ChangeButtonStr();
                        isChoseButton=false;
                    }
                }
            }
        }
    }
}

public struct ButtonSet
{
    public int buttonName;
    public string buttonKecode;
}

