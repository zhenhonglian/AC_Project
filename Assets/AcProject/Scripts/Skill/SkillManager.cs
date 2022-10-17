using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]Skill[] mySkills=new Skill[4];//玩家进入游戏时自选的4个天赋技能，在游戏开始时便使用的技能
    [SerializeField]List<Skill> gameSkill=new List<Skill>();
    List<int> canImproveSkills=new List<int>();

    List<int> canGetSkill=new List<int>();
    private int nowSkillCount=0;
    public GameObject[] skillButtons=new GameObject[10];
    private SkillDec[] skillDecs=new SkillDec[10];
    public Dictionary<int, Skill> gameSkills=new Dictionary<int, Skill>();
    public int skillPrefabsLength;
    void Start()
    {  
        for (int i = 0; i < skillPrefabsLength; i++)
        {
            if(!gameSkills.ContainsKey(i))
            {
                gameSkills.Add(i,Resources.Load("Skill/"+i.ToString("0000")) as Skill);
                canGetSkill.Add(i);
            }
        }
       for (int i = 0; i < skillButtons.Length; i++)
       {
           skillDecs[i]=skillButtons[i].GetComponent<SkillDec>();
       }

       EventManager.instance.AddEventListener("PlayerLevelUp",GetSkill);
        //mySkills=PlayerSence.mySkill;
        UseSkill(mySkills); 
        UpdateSkillUi();
    }
    

    // Update is called once per frame
    void Update()
    {
    if(Input.GetKeyDown(KeyCode.Z))
    {   
        //useSkill(skillDecs[0].myskill,true);
        GetSkill();
    }

    }
/// <summary>
/// 无特定目标的技能使用，且一次直接使用多个技能
/// </summary>
/// <param name="_skill"></param>
    public void UseSkill(Skill[] _skill)
    {
        for (int i = 0; i < _skill.Length; i++)
        {
            //_skill[i].UseSkill();使用技能释放器 传入技能
            if(_skill[i]!=null)
            {
                SkillUse useSkill=new SkillUse();
                useSkill.CurrentSkill=_skill[i];
                useSkill.UseObject();
            }
        }
    }
    public void useSkill(Skill _skill)
    {
            SkillUse useSkill=new SkillUse();
            useSkill.CurrentSkill=_skill;
            useSkill.UseObject();

    }

    public void useSkill(Skill _skill,bool isRemove)
    {
        SkillUse useSkill=new SkillUse();
        useSkill.CurrentSkill=_skill;
        useSkill.IsRemove=isRemove;
        useSkill.UseObject();
    }
    /// <summary>
    /// 自身传入目标的技能使用，且一次使用一个技能
    /// </summary>
    /// <param name="skill"></param>
    /// <param name="target"></param>
    public static void UseOneSkill(Skill skill,GameObject[] target)
    {
         if(skill!=null&&target!=null)
            {
                SkillUse useSkill=new SkillUse();
                useSkill.Target=target;
                useSkill.CurrentSkill=skill;
                useSkill.UseObject();
            }
    }
    public static void UseOneSkill(Skill skill,GameObject[] target,GameObject owner)
    {
         if(skill!=null&&target!=null)
            {
                SkillUse useSkill=new SkillUse();
                useSkill.Owner=owner;
                useSkill.Target=target;
                useSkill.CurrentSkill=skill;
                useSkill.UseObject();
            }
    }

/// <summary>
/// 游戏开始时加载玩家进入游戏前配置的技能
/// </summary>
    void UpdateSkillUi()
    {
        for (int i = 0; i < mySkills.Length; i++)
        {
            if(mySkills[i]!=null)
            {
                //Debug.Log(mySkills[i].skillDes);
                skillDecs[i].UpdateSkillButton(mySkills[i],i);
            }
        }
    }

    public void GetSkill()
    {
        if(nowSkillCount<skillDecs.Length&&canGetSkill.Count>0)
        {
            float x=Random.Range(0,1f);
            if(x<=0.35&&canImproveSkills.Count>0)
                SkillLevelUp();
            else 
                GetNewSkill(canGetSkill[Random.Range(0,canGetSkill.Count)]);
            
        }
        else
        {
            if(canImproveSkills.Count>0)
            SkillLevelUp();
            else
            Debug.Log("没有可以升级的技能了");
        }
    }
    public void GetNewSkill(int nub)
    {
        int index=0;
        for (int i = 0; i < skillDecs.Length; i++)
        {
            if(skillDecs[i].myskill==null)
            {
                index=i;
                break;
            }
            else index=-1;
        }
        if(index>=0)
        {
            skillDecs[index].UpdateSkillButton(gameSkills[nub],index);
            useSkill(skillDecs[index].myskill);
            if(skillDecs[index].myskill.skillLevel<skillDecs[index].myskill.skillMaxLevel)
            canImproveSkills.Add(nub);

            gameSkills.Remove(nub);
            nowSkillCount++;
            canGetSkill.Remove(nub);
        }
    }

    
    public void SkillLevelUp()
    {
        Debug.Log("技能升级了");
        int index =0;
        index=Random.Range(0,canImproveSkills.Count);
        for (int i = 0; i < skillDecs.Length; i++)
        {
            if(skillDecs[i].myskill.skillID==canImproveSkills[index])
            {
                index=i;
                break;
            }
        }
        useSkill(skillDecs[index].myskill,true);
        if(skillDecs[index].myskill.skillType==e_SkillType.trigger)
        EventManager.instance.EventTrigger(skillDecs[0].myskill.skillID.ToString(),this);
        
        skillDecs[index].myskill=Resources.Load("Skill/"+skillDecs[index].myskill.skillID.ToString("0000")+(skillDecs[index].myskill.skillLevel).ToString())as Skill;
        if(skillDecs[index].myskill.skillLevel>=skillDecs[index].myskill.skillMaxLevel)
        {
            canImproveSkills.Remove(skillDecs[index].myskill.skillID);
        }
        useSkill(skillDecs[index].myskill);


    }

}

public class SkillTrigger
{
    int id;
    Iimpect iimpect;
}