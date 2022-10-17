using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ac_Enemy : MonoBehaviour,IRestLoad
{
    // Start is called before the first frame update
    GameObject target;
    public float moveSpeed;
    public int maxHp;
    public float hp;
    e_DamgeType damgeType=e_DamgeType.normal;
    public GameObject damTextPrefab;
    Rigidbody2D rb;
    float dis;//距离
    public bool IsinAREA;
    Vector2 distence;
    public Vector3 startScal;
    public float exp;
    public float currentExp;
    bool isHurt;
    float goBackTime=0f;
    //传送
    float maxX,maxY,minX,minY;
    Vector2 enemyBornPos;
    // Update is called once per frame
    [SerializeField]List<GameObject> items=new List<GameObject>();
    private void FixedUpdate() {
        if(target!=null)
        {   
            gameObject.transform.position=Vector3.MoveTowards(transform.position,target.transform.position,moveSpeed*Time.deltaTime);
            Filp();
            //DistanceCheck();
        }
        if(goBackTime>0)
        goBackTime-=Time.fixedDeltaTime;
    }
    public void OnRest()
    {
        currentExp=exp*(AcGameManager.mapLevel*0.25f+1);
        if(FindObjectOfType<AcPlayerCon>()!=null)
        target=FindObjectOfType<AcPlayerCon>().gameObject;
        else target=null;

        hp=maxHp*(1+AcGameManager.mapLevel*0.5f);
    }

    public void GetHurt(float dmg)
    {
        int _dmg=(int)dmg;
        //dmg=(int)dmg;
        hp-=_dmg;
        PoolManager.Release(damTextPrefab,transform.position).TryGetComponent<DamText>(out DamText nowDamText);
        nowDamText.ChangeDamText(_dmg);
        nowDamText.damgeType=damgeType;
        nowDamText.gameObject.transform.localScale=new Vector3(0.05f,0.05f,0);
        //PoolManager.Release(hitPrefab,transform.position);
        //if(hp>0)
            //ChangeEnemyUi();
        if(hp<=0)
            Death();
        isHurt=true;
        if(isHurt&&goBackTime<=0)
        {   
            isHurt=false;
            goBackTime=1f;
            Vector2  hurt=new Vector2(transform.position.x-target.transform.position.x,transform.position.y-target.transform.position.y);
                hurt=hurt.normalized;
 
            transform.position=new Vector2(transform.position.x+hurt.x*0.5f,transform.position.y+hurt.y*0.5f);
        }
           
       // return _dmg;
    }
    public void Transmit()
    {
        EdgeCheck();
        transform.position=new Vector2(enemyBornPos.x,enemyBornPos.y);
    }
    void Death()
    {
        gameObject.SetActive(false);
        AcGameManager.instance.activeEnemyList.Remove(this.gameObject);
        EventManager.instance.EventTrigger("EnemyDie",this);
        CreatItem();
    }
    void Filp()
    {
        if(target.transform.position.x>transform.position.x)
         transform.localScale=new Vector3(startScal.x,startScal.y,1);
        if(target.transform.position.x<transform.position.x)
         transform.localScale=new Vector3(-startScal.x,startScal.y,1);
    }
    private void EdgeCheck()
    {
        Vector3 posOne=Camera.main.ViewportToWorldPoint(new Vector3(0,0));
        Vector3 posTwo=Camera.main.ViewportToWorldPoint(new Vector3(1,1));
        maxX=posTwo.x;maxY=posTwo.y;minX=posOne.x;minY=posOne.y;  
        float c= Random.Range(0,1f);
        if(c<=0.35f)
        {
            enemyBornPos.x =Random.Range(0, 1f)<=0.5f ? Random.Range(minX-5, minX-1) : Random.Range(maxX+1,maxX+5);
            enemyBornPos.y=Random.Range(minY-5,maxY+5); 
        }
        else 
        {
            enemyBornPos.y =Random.Range(0, 1f)<=0.5f ? Random.Range(minY-5, minY-1) : Random.Range(maxY+1,maxY+5);
            enemyBornPos.x=Random.Range(minX-5,maxX+5);
        }   
    }


    private void CreatItem()
    {
        int x=Random.Range(0,100);
        if(x<3&&items.Count>0)
        {
            //Instantiate(items[Random.Range(0,items.Count)],transform.position,Quaternion.identity);
            PoolManager.Release(items[Random.Range(0,items.Count)],transform.position,Quaternion.identity);
        }

    }
}
