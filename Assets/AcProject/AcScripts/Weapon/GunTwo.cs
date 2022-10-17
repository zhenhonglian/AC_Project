using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTwo : Gun
{
    int x=5;
    int mid;
    protected override void ShootEnemy()
    {
        mid=x/2;
        trueDmg=dmg;
        EventManager.instance.EventTrigger("GunAtk",this);
        Vector2 dir=((targets[0].transform.position-
         transform.position).normalized);
        for (int i = 0; i <x; i++)
        {

          GameObject bullet=  PoolManager.Release(bulletPrefab,this.transform.position,Quaternion.AngleAxis((i-mid)*15,Vector3.forward)*dir);
            bullet.GetComponent<Ac_Bullet>().dmg=this.trueDmg;
         //Debug.Log(transform.forward);
         //bullet.transform.rotation=new Quaternion(0,0,bullet.transform.rotation.x+(i-mid)*5,0);
         //Debug.Log(transform.rotation.z);
         //bullet.transform.rotation=new Quaternion();
            
        }
    }
}
