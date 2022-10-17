using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Buff
{
    // Start is called before the first frame update
    public e_SkillBuffType buffType;
    public float duration;
    public int buffid;
    public float buffDmg;

    public GameObject buffFrom;
}
