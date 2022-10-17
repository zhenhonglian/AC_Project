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
        {   EventManager.instance.AddEventListener(nowSkill.triggerName, DoubleDmg);
            EventManager.instance.AddEventListener(nowSkill.skillID.ToString(),RemoveThisSelf);
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

    public void RemoveThisSelf(object _self)
    {
        EventManager.instance.RemoveEventListener(nowSkill.triggerName, DoubleDmg);
    }
}
