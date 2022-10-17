using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialPlayerDataImpect : Iimpect
{
    PercentPlayerData nowdata;
    public void UseSkill(Skill skilldata, GameObject[] target, GameObject skillOwner, int num, bool isRemove)
    {
        Debug.Log(1);
        for (int i = 0; i < target.Length; i++)
        {
            if(target[i].TryGetComponent<AcPlayerCon>(out AcPlayerCon nowTarget))
            {
                //nowTarget.myMoney+=skilldata.skillValues[num].nowSkillValue;

                ChangeData(nowTarget,skilldata,num,isRemove);

            }
            
        }
    }
    public void ChangeData(AcPlayerCon _player,Skill skill,int i,bool isRemove)
    {
        
        for (int y = 0; y < _player.myPercentDatas.Count; y++)
            {
                if(_player.myPercentDatas[y].ID==skill.skillID)
                {
                    nowdata= _player.myPercentDatas[y];
                }
            }
        if(isRemove)
            {
                 _player.myPercentDatas.Remove(nowdata);
            }
        else{
             nowdata=new PercentPlayerData(skill.skillID);
            switch(skill.skillValues[i].nowValueType)
            {
                case e_SkillValue.PlayerHp:
                nowdata.hp+=skill.skillValues[i].nowSkillValue;
                break;

                case e_SkillValue.PlayerPower:
                nowdata.power+=skill.skillValues[i].nowSkillValue;
                break;
            }
            _player.myPercentDatas.Add(nowdata);
        }
        


        _player.UpdatePlayerData();
    }

    // Start is called before the first frame update
}
