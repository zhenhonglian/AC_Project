using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ac_Bullet : MonoBehaviour,IRestLoad
{
    // Start is called before the first frame update
    protected float Speed=20f;
    public float dmg=0f;
    public float trueDmg;
    public float gunDmg;
    protected float activeTime=0.5f;

    public virtual void OnRest()
    {
        activeTime=0.3f;
        

    }


    // Update is called once per frame
    void Update()
    {
       Move();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.transform.CompareTag("Enemy"))
        {
            //Debug.Log("aaa");
            trueDmg=gunDmg>0?gunDmg:dmg;//判断子弹是否直接从玩家发出或从武器发射
            EventManager.instance.EventTrigger("BulletAtk",this);//在造成伤害前计算子弹的加成伤害
            //Debug.Log(trueDmg);
            other.gameObject.GetComponent<Ac_Enemy>().GetHurt(trueDmg);
            BulletDie();
        }
    }

    protected virtual void Move()
    {
        transform.Translate(Vector3.up*Speed*Time.deltaTime);
        activeTime-=Time.deltaTime;
        if(activeTime<=0)
        BulletDie();
    }

    protected virtual void BulletDie()
    {
        if(gunDmg>0)gunDmg=0;
        gameObject.SetActive(false); 
    }
}
