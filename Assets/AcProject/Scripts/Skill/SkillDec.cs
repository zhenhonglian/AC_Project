using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillDec : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    // Start is called before the first frame update
    public Skill myskill;
    public int buttonNub;
    public GameObject skillDec;
    Image myImage;

    public bool isGet;

    public void OnPointerEnter(PointerEventData eventData)
    {   
        if(myskill!=null)
        {
            skillDec.SetActive(true);
            skillDec.GetComponentInChildren<Text>().text=myskill.skillName+"LV"+(myskill.skillLevel).ToString()+"\n"+myskill.skillDes;
            myImage.color=new Color(myImage.color.r,myImage.color.g,myImage.color.b,1f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //if(myskill!=null)
        
        skillDec.SetActive(false);
        if(myskill!=null)
        myImage.color=new Color(myImage.color.r,myImage.color.g,myImage.color.b,0.15f);
        //Debug.Log(myImage.color.a);
        
    }

    void Start()
    {
        myImage=GetComponent<Image>();
           
    }
    public void UpdateSkillButton(Skill _skill,int i,bool _isGet)
    {

            myskill=_skill;
            if(myImage==null)
            {
                myImage=GetComponent<Image>();
            }
            myImage.sprite=myskill.skillImage;
            myImage.color=new Color(myImage.color.r,myImage.color.g,myImage.color.b,0.15f);
            buttonNub=i;
            isGet=_isGet;

    }
    // Update is called once per frame

}
