using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_FireBullet : Ac_Bullet
{
    // Start is called before the first frame update   
    private List<Ac_Enemy> isAtkEnemys=new List<Ac_Enemy>();

    protected override void Move()
    {
        transform.localScale=new Vector3(transform.localScale.x+1.5f*Time.deltaTime,transform.localScale.y+1.5f*Time.deltaTime,1f);
        transform.Translate(Vector3.up*Speed*0.6f*Time.deltaTime);
        activeTime-=Time.deltaTime;
        if(activeTime<=0)
        BulletDie();
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.CompareTag("Enemy"))
        {
            //Debug.Log("aaa");
            //gameObject.SetActive(false);        
            Debug.Log(gunDmg);
            trueDmg=gunDmg>0?gunDmg:dmg;
            bool canHurt=true;
            other.gameObject.TryGetComponent<Ac_Enemy>(out Ac_Enemy nowEnemy);
            if(isAtkEnemys.Count>0)
            {
                for (int i = 0; i < isAtkEnemys.Count; i++)
                {
                    if (nowEnemy==isAtkEnemys[i])
                    {
                        canHurt=false;
                        break;
                    }
                }
            }
            if(canHurt)
            {
                EventManager.instance.EventTrigger("BulletAtk",this);
                nowEnemy.GetHurt(trueDmg);
                isAtkEnemys.Add(nowEnemy);
            }
            //.GetHurt(dmg);
        }
    }
    public override void OnRest()
    {
        isAtkEnemys.Clear();
        transform.localScale=new Vector3(0.3f,0.3f,1f);
        activeTime=0.8f;
        dmg=5*(1+AcPlayerCon.instance.powerLevel)*0.5f;
        //trueDmg=dmg;

        
    }


}
