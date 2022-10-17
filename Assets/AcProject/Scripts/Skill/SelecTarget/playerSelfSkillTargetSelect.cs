using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSelfSkillTargetSelect : IselectTarget
{
    public GameObject[] FindTarget(Skill skilldata)
    {
        return GameObject.FindGameObjectsWithTag("Player");
    }
}
