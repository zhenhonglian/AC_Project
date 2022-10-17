using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
/// <summary>
/// 技能释放器
/// </summary>
public class SkillUse
{
    // Start is called before the first frame update
    private IselectTarget selectTarget;
    private Iimpect[] skillImpect;
    private Skill currentSkill;
    private GameObject[] nowTarget;
    private GameObject skillOwner;
    private bool isRemove=false;
    public GameObject[] Target
    {
        set{
            nowTarget=value;
        }
    }
    public GameObject Owner
    {
        set{
            skillOwner=value;
        }
    }
    public bool IsRemove
    {
        set{
            isRemove=value;
        }
    }

    public Skill CurrentSkill
    {
        get
        {
            return currentSkill;
        }
        set
        {
            currentSkill=value;
            //Initialize();

        }
    }
    /// <summary>
    /// 需要自身选取目标的技能使用
    /// </summary>
    private void Initialize()
    {
        //currentSkill.skillTarget
        //创建算法对象
        //选取技能目标
        selectTarget=DeployerConfig.CreatSelectTarget(currentSkill);
        //技能效果发生
        //skillImpect=new Iimpect[currentSkill.skillBuffType.Length]();
        skillImpect=DeployerConfig.CreatImpects(currentSkill);

    }
    /// <summary>
    /// 传入目标的技能使用
    /// </summary>
    private void HaveTargetInitialize()
    {
        skillImpect=DeployerConfig.CreatImpects(currentSkill);
    }
    /// <summary>
    /// 使用技能 在传递了技能数据等信息后调用
    /// </summary>
    public void UseObject()
    {
        if(nowTarget==null)
        {
            Initialize();
            GameObject[] target=  selectTarget.FindTarget(currentSkill);  
            //Debug.Log(target[0].name);
            for (int i = 0; i < skillImpect.Length; i++)
            {
             skillImpect[i].UseSkill(currentSkill,target,skillOwner,i,isRemove);
             Debug.Log("成功执行");
            }
            
        }
        else
        {
            HaveTargetInitialize();
            for (int i = 0; i < skillImpect.Length; i++)
            {
             skillImpect[i].UseSkill(currentSkill,nowTarget,skillOwner,i,isRemove);
             //Debug.Log("成功执行");
            }
        }
        
      
    }


}
