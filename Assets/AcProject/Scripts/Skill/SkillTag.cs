using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum e_SkillTarget
{
    playerSelf,//玩家自身
    tower,//所有防御塔
    enemy,//所有敌人
    gun,//所有的gun
}

public enum e_SkillType 
{
    normal,//直接使用的技能
    trigger,//触发类型的方法
}

public enum e_SkillBuffType
{
    basePlayerData,//改变玩家数值
    precentPlayerData,//百分比玩家数值
    specialPlayerData,//特殊的数值加成（为玩家添加新的PercentPlayerData，独立加成）

    changeGunData,
    slowdown,//减速sliwdownImpect
    setState,//改变状态
    //changeAtkSpwan,//改变攻速changeAtkSpwanImpect
    doubleDmg,//双倍伤害
    damOverTime,//持续伤害
    fire
}

public enum e_SkillValue
{
    Null,
    Gold,//金钱
    Dmg,//防御塔伤害
    Atkspwan,//攻击速度
    PlayerHp,//玩家血量
    PlayerPower,//玩家伤害
    PlayerMoveSpeed,//玩家移速
    WeaponSize,//武器大小
    
}
 public enum StatusEffect
    {
        None,
        Poison,//禁锢
        Slow,//减速
        Mute//沉默
    }

public enum e_DamgeType
    {
        clod,
        fire,//禁锢
        blood,//减速
        normal//沉默
    }
  
    //Now we can make a variable using that set as its type!