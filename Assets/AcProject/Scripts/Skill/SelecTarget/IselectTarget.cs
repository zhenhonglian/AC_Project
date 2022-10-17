using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 选取目标
/// </summary>
public interface IselectTarget 
{
    // Start is called before the first frame update

    GameObject[] FindTarget(Skill skilldata);
}
