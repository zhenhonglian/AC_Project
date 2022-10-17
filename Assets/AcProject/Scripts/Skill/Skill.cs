using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="newSkill",fileName ="Skill")]
public class Skill : ScriptableObject
{
    // Start is called before the first frame update
    public int skillID;
    public string skillName;
    public e_SkillType skillType;
    public string triggerName;
    public e_SkillTarget skillTarget;
    public e_SkillBuffType[] skillBuffType;
    public skillValue[] skillValues;//
    public int skillLevel;
    //public float skillValue;
    public int skillMaxLevel;
    //public int skillTime;//技能效果持续时间
    public Sprite skillImage;
    [TextArea(1,8)]
    public string skillDes;
    public bool isUnlocked;
    public e_DamgeType damgeType;
    public GameObject prefab=null;

}


[System.Serializable]
public class skillValue
{

    public e_SkillValue nowValueType;//改变数值种类
    public float nowSkillValue;//技能数值
    public float skillTime;//持续时间
    public float skillSpwanTime;//触发间隔

    public float pr;//触发概率（有需要就使用）
}
