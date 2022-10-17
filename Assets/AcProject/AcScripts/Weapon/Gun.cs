using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour,UpdateMyData
{
    // Start is called before the first frame update
    public List<GameObject> targets=new List<GameObject>();
    public GameObject bulletPrefab;
    Vector2 MoveDir;
    bool shootEnemy;


    public WaitForSeconds spwanTime;
    public float atkSpeed;
    public float baseDmg=5;
    protected float dmg;
    public float trueDmg;
    private void Start() 
    {
        spwanTime=new WaitForSeconds(1/atkSpeed);
        //dmg=baseDmg;
        StartCoroutine(Shoot());
    }
    void Update()
    {
        //Move();
        if(targets.Count>0)
        {
                      
            Vector2 ojb=transform.position;
            Vector2 targetDir=targets[0].transform.position;
            MoveDir=(targetDir-ojb).normalized;
            transform.up=MoveDir;
            shootEnemy=true;
        }
       
    }

    IEnumerator Shoot()
    {
        while(true)
        {
            shootEnemy= targets.Count==0?false:true;     

            if(shootEnemy)
            {  
                ShootEnemy();
            }
            //SendFatherMsg(newBullet,nowDam);
            //Debug.Log(target.gameObject.transform.position);
            //Debug.Log(targets.Count);
            yield return spwanTime;
        }
    }

    protected virtual void ShootEnemy()
    {   
        trueDmg=dmg;
        EventManager.instance.EventTrigger("GunAtk",this);//部分技能效果会在武器发动攻击时生效 所以需要调用事件中心中对应事件，在技能获得时添加事件 技能移除时移除事件
         PoolManager.Release(bulletPrefab,this.transform.position,(targets[0].transform.position-
         transform.position).normalized).GetComponent<Ac_Bullet>().dmg=this.trueDmg;
    }


    public void UpdateThisData(AcPlayerCon _player)
    {
        dmg=baseDmg*(1+_player.powerLevel);
        
    }
}
