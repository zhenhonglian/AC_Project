using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 使用技能的接口，
/// </summary>
public interface Iimpect 
{
    // Start is called before the first frame update
     void UseSkill(Skill skilldata,GameObject[] target,GameObject skillOwner,int num,bool isRemove);

}
/// <summary>
/// 更新自身数据的接口
/// </summary>
public interface UpdateMyData
{
    void UpdateThisData(AcPlayerCon _player);
}
