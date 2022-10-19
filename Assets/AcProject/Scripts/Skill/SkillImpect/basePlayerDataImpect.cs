using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class precentPlayerDataImpect : Iimpect
{
    // Start is called before the first frame update
    public void UseSkill(Skill skilldata, GameObject[] target, GameObject skillOwner, int num,bool isRemove)
    {
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
        float nowValue=isRemove?-skill.skillValues[i].nowSkillValue:skill.skillValues[i].nowSkillValue;
        switch(skill.skillValues[i].nowValueType)
        {
            case e_SkillValue.PlayerHp:
                _player.myPercentData.hp+=nowValue;
            break;
            case e_SkillValue.PlayerPower:
                _player.myPercentData.power+=nowValue;
            break;
            case e_SkillValue.PlayerMoveSpeed:
                _player.myPercentData.moveSpeed+=nowValue;
            break;
            case e_SkillValue.WeaponSize:
                _player.closeAtkSize+=nowValue;
            break;
            case e_SkillValue.CloseAtkSpeed:
                 _player.myPercentData.atkSpeed+=nowValue;
            break;
            case e_SkillValue.ExpGet:
                 _player.addExp+=nowValue;
            break;
  
        }
        _player.UpdatePlayerData();
    }
}
