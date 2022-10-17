using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour,IRestLoad
{
    // Start is called before the first frame update
    public E_Item thisType;
    public float itemValue;
    public SpriteRenderer thisSprite;
    public void GetItem(AcPlayerCon _player)
    {
        switch(thisType)
        {
            case E_Item.ExpItem:
            itemValue=6000;
            _player.GetExp(itemValue);
            this.gameObject.SetActive(false);
            break;
            case E_Item.HpItem:
            itemValue=-10;
            _player.GetHurt(itemValue);
            this.gameObject.SetActive(false);
            break;
            case E_Item.SkillItem:
            //_player.GetHurt(itemValue);
            EventManager.instance.EventTrigger("PlayerLevelUp"); 
            this.gameObject.SetActive(false);
            break;
        }
        
    }

    public void OnRest()
    {
        if(thisSprite==null)
        thisSprite=GetComponent<SpriteRenderer>();
        thisType=(E_Item)int.Parse(Random.Range(0,3).ToString());
        switch(thisType)
        {
            case E_Item.ExpItem:
            
            thisSprite.color=Color.green;
            break;
            case E_Item.HpItem:
            thisSprite.color=Color.red;
            break;
            case E_Item.SkillItem:
            thisSprite.color=Color.yellow;
            break;
        }
    }
}


public enum E_Item
{
    ExpItem=0,//经验物品
    HpItem=1,//血量物品
    SkillItem=2,//技能物品
}