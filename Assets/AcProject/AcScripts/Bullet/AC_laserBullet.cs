using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_laserBullet : Ac_Bullet
{
    // Start is called before the first frame update
    private Vector2 muBiao;
    protected GameObject target;
    public Vector2 ChangeAngel,BulletSpeed;
    public float changeAngel,bulletSpeed;

    public GameObject Target{set{target=value;}}
    protected override void Move()
    {
        if(target==null||!target.activeSelf)
        transform.Translate(Vector3.up*bulletSpeed*Time.deltaTime);
        if(target!=null&&target.activeSelf)
        {
            muBiao=target.transform.position;
            Vector2 ojb=transform.position;
            //Vector2 mouse=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _MoveDir=(muBiao-ojb).normalized;
            float angle_1=360-Mathf.Atan2(_MoveDir.x,_MoveDir.y)*Mathf.Rad2Deg;
            //transform.up=MoveDir;
            transform.eulerAngles=new Vector3(0,0,angle_1);
            transform.rotation=transform.rotation*Quaternion.Euler(0,0,changeAngel);
            //Destroy(this.gameObject,2f);
            transform.Translate(Vector3.up*bulletSpeed*Time.deltaTime);
        }
        activeTime-=Time.deltaTime;
        if(activeTime<=0)
        gameObject.SetActive(false);
    }
    public override void OnRest()
    {
        base.OnRest();
        if(AcPlayerCon.instance.enemyList.Count>0)
        {  
            target=AcPlayerCon.instance.enemyList[Random.Range(0,AcPlayerCon.instance.enemyList.Count)];
            changeAngel=Random.Range(ChangeAngel.x,ChangeAngel.y);
            bulletSpeed=Random.Range(BulletSpeed.x,BulletSpeed.y);
        }
        else
        {
            target=null;
            changeAngel=Random.Range(ChangeAngel.x,ChangeAngel.y);
            bulletSpeed=Random.Range(BulletSpeed.x,BulletSpeed.y);
        } 
        
        dmg=AcPlayerCon.instance.colseAtk.GetComponent<AC_Dao>().dam*(1+AcPlayerCon.instance.powerLevel)*0.2f;
    }

}
