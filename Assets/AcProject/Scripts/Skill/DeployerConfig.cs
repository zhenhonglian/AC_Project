using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 释放器配置工厂
/// </summary>
public class DeployerConfig
{
    public static IselectTarget CreatSelectTarget(Skill skilldata)
    {        
        string className=string.Format("{0}SkillTargetSelect",skilldata.skillTarget);
    
        return CreatObject<IselectTarget>(className);
    }
    public static Iimpect[] CreatImpects(Skill skilldata)
    {
        Iimpect[] iimpects=new Iimpect[skilldata.skillBuffType.Length];
       
        for (int i = 0; i < skilldata.skillBuffType.Length; i++)
        {
            string classNameone=string.Format("{0}Impect",skilldata.skillBuffType[i]);
            iimpects[i]=CreatObject<Iimpect>(classNameone);
        }
        return iimpects;
    }

    

    private static T CreatObject<T>(string className)where T:class
    {
        Type type= Type.GetType(className);
        return Activator.CreateInstance(type)as T;
    }

}
