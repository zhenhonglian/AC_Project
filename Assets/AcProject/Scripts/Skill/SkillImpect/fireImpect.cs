using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireImpect : Iimpect
{
    float t;
    Skill nowSkill;
    int index;
    int mid;
     public WaitForSeconds seconds=new WaitForSeconds(0.5f);
    public void UseSkill(Skill skilldata, GameObject[] target,GameObject skillOwner,int num,bool isRemove)
    {
        nowSkill=skilldata;
        index=num;
        mid=((int)(nowSkill.skillValues[index].nowSkillValue/2));
        if(!isRemove&&nowSkill.skillType==e_SkillType.trigger)
        {   EventManager.instance.AddEventListener(nowSkill.triggerName, Fire);
            //Debug.Log(nowSkill.skillID.ToString());
            EventManager.instance.AddEventListener(nowSkill.skillID.ToString(),RemoveThisSelf);
        }
        if(!isRemove&&nowSkill.skillType==e_SkillType.normal)
        {
            NowStartCor a=new NowStartCor();
            seconds=new WaitForSeconds(nowSkill.skillValues[index].skillSpwanTime);
            a.mycor= AcPlayerCon.instance.StartCoroutine(AutoFire(nowSkill));
            a.id=nowSkill.skillID;
            AcPlayerCon.instance.myCoroutines.Add(a);
        }
        if(isRemove&&nowSkill.skillType==e_SkillType.normal)
        {
            for (int i = 0; i < AcPlayerCon.instance.myCoroutines.Count; i++)
            {
                if (nowSkill.skillID==AcPlayerCon.instance.myCoroutines[i].id)
                {
                    AcPlayerCon.instance.StopCoroutine(AcPlayerCon.instance.myCoroutines[i].mycor);
                    AcPlayerCon.instance.myCoroutines.Remove(AcPlayerCon.instance.myCoroutines[i]);
                    break;
                }
            }
        }


        Debug.Log(EventManager.instance.eventDic.Count);
    }


    public void Fire(object _player)
    {
        
        //if(AcPlayerCon.instance.enemyList.Count>0)
        for (int i = 0; i < nowSkill.skillValues[index].nowSkillValue; i++)
        {
            PoolManager.Release(nowSkill.prefab,(_player as AcPlayerCon).playerPos.transform.position,Quaternion.AngleAxis((i-mid)*nowSkill.skillValues[index].pr,Vector3.forward)*(_player as AcPlayerCon).playerPos.transform.right);
            //Debug.Log(i);
        }
    }

    public void RemoveThisSelf()
    {
        EventManager.instance.RemoveEventListener(nowSkill.triggerName, Fire);
        //Debug.Log("11111111");
    }

    IEnumerator AutoFire(Skill skilldata)
    {
        //yield return 1f;
        //nowEnemy.status=StatusEffect.Slow;
        //nowEnemy.EnemySpeed*=(1-skilldata.skillValue);
        while(true)
        {   
            
            for (int i = 0; i < nowSkill.skillValues[index].nowSkillValue; i++)
            {
            PoolManager.Release(nowSkill.prefab,AcPlayerCon.instance.transform.position,Quaternion.AngleAxis((i-mid)*nowSkill.skillValues[index].pr,Vector3.forward)*AcPlayerCon.instance.transform.right);
            //Debug.Log(i);
            }
            yield return seconds;
         }
         
    }
}
