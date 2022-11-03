using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doubleDmgImpect : Iimpect
{
    // Start is called before the first frame update
    float t;
    Skill nowSkill;
    int index;
    public void UseSkill(Skill skilldata, GameObject[] target,GameObject skillOwner,int num,bool isRemove)
    {
        nowSkill=skilldata;
        index=num;

        if(!isRemove)

        {   if(nowSkill.triggerName=="GunAtk")
            {
                EventManager.instance.AddEventListener(nowSkill.triggerName, DoubleDmg);
                EventManager.instance.AddEventListener(nowSkill.skillID.ToString(),RemoveThisSelf);
            }
            if(nowSkill.triggerName=="BulletAtk")
            {
                EventManager.instance.AddEventListener(nowSkill.triggerName, DoubleDmgT);
                EventManager.instance.AddEventListener(nowSkill.skillID.ToString(),RemoveThisSelfT);
            }
        }


        Debug.Log(EventManager.instance.eventDic.Count);
    }


    public void DoubleDmg(object _thisGun)
    {
        t=
        (_thisGun as Gun).trueDmg;
        if(Random.Range(0,1f)<nowSkill.skillValues[index].pr)
        {
            (_thisGun as Gun).trueDmg=t*(1+nowSkill.skillValues[index].nowSkillValue);
        }
    }
    public void DoubleDmgT(object _thisGun)
    {
        t=
        (_thisGun as Ac_Bullet).trueDmg;
        if(Random.Range(0,1f)<nowSkill.skillValues[index].pr)
        {
            (_thisGun as Ac_Bullet).trueDmg=t*(1+nowSkill.skillValues[index].nowSkillValue);
            //Debug.Log(t);
        }
    }

    public void RemoveThisSelf()
    {
        EventManager.instance.RemoveEventListener(nowSkill.triggerName, DoubleDmg);
    }
    public void RemoveThisSelfT()
    {
        EventManager.instance.RemoveEventListener(nowSkill.triggerName, DoubleDmgT);
        //Debug.Log("11111111111111111111111");
    }
}
