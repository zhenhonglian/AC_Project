using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum e_PlayerLevelUp
{
    moveSpeed=0,
    atkSpeed=1,

}
public class AcPlayerCon : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    float baseMoveSpeed=5f;
    public float moveSpeed;
    Vector2 moveDir=new Vector2(0,0);
    Vector2 xiuzheng;
    bool isRunning;
    float dashTime=0f;
    public float dashCD=0f;
    float transmitTime=0f;
    [Header("攻击信息")]
    public GameObject buttle,colseAtk;
    public GameObject[] firePoint=new GameObject[4];
    public float atkSpeed;

    private float atkTime;
    [Header("玩家信息")]//等级、血量、近战速度、移动速度、经验值 攻击强度
    public float playerHp;
    public int playerMaxLevel;
    public float baseHp=50;
    public float nowEXP;
    public int playerNowLevel;
    public float maxHp;
    public float nowHp;
    public float maxExp;
    public float cureHp;//定期回复的血量
    float cureTime=0.5f;//回复间隔
    float moveY,moveX;//角色移动
    public float basePower=500f;
    public float power;
    public float powerLevel;
    public float closeAtkSize;
    public List<GameObject> enemyList=new List<GameObject>();//攻击列表
    public float dis,nowdis,trueDis;
    private float checkTime=0.01f;

    public PercentPlayerData myPercentData=new PercentPlayerData(9999999);//对属性的百分比加成
    public List<PercentPlayerData> myPercentDatas=new List<PercentPlayerData>();
    //状态
    public GameObject diePrefab;
    [SerializeField]public List<NowStartCor> myCoroutines=new List<NowStartCor>();
    private WaitForSeconds cutTime=new WaitForSeconds(0.01f);//计时器协程的间隔时间
    //玩家技能
    public static AcPlayerCon instance;
    private void Awake() {
        
         if(instance==null)
        {
            instance=this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        myPercentDatas.Add(myPercentData);
        maxExp=playerNowLevel*playerNowLevel*1000;
        EventManager.instance.AddEventListener("EnemyDie",EnemyDie);
        animator=GetComponent<Animator>();
        xiuzheng.y=1;
        atkTime=0f;
        trueDis=dis*dis;
        AcUimanager.instance.PlayerUiChange(nowEXP,maxExp,playerNowLevel);
        //Debug.Log(firePoint[0].TryGetComponent<GunTwo>(out GunTwo _gun));
        StartCoroutine(TimeCount());
        UpdatePlayerData();
    }

    // Update is called once per frame
    void Update()
    {   
        //TheMove();
        ChangeAnim();
        if(Input.GetKeyDown(KeyCode.Space)&&dashCD<=0)
        {
            dashTime=0.15f;
            dashCD=3f; 
        }
        //if(dashCD>0)
        //dashCD-=Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.J)&&atkTime<=0)
        {

            atkTime=1/atkSpeed;
            //Debug.Log(atkTime);
            EventManager.instance.EventTrigger("CloseAtk",this);
            Attack();
        }
        if(cureTime<=0&&nowHp<maxHp)
        {
            cureTime=0.5f;
            GetHurt(-cureHp);
        }
        //if(Input.GetKeyDown(KeyCode.Space)&&atkTime<=0)
        Dash();
    }
    private void FixedUpdate() {
        Move(); 
        if(checkTime<=0)
        DistanceCheck();
        //if(enemyList.Count!=0)
        for (int i = 0; i < firePoint.Length; i++)
        {
            if(firePoint[i].activeSelf)
            firePoint[i].GetComponent<Gun>().targets=enemyList;
        }
        
    }
    private void Move()
    {
        moveDir.x=Input.GetAxisRaw("Horizontal");
        xiuzheng.x=moveDir.x;
        moveDir.y=Input.GetAxisRaw("Vertical");
        transform.Translate(xiuzheng*moveDir.normalized*moveSpeed*Time.fixedDeltaTime);
        //Debug.Log(moveDir.normalized);
        if(moveDir.x<0)
        transform.localEulerAngles=new Vector3(0,180,0);
        else if(moveDir.x>0)
        transform.localEulerAngles=new Vector3(0,0,0);
        //else if(moveDir.x*transform.right.x>0) transform.Rotate(0,0,0);
    }

    private void ChangeAnim()
    {
        animator.SetFloat("Speed",moveDir.magnitude);
    }

    private void Dash()
    {
        animator.SetBool("IsDash",true);
        if(dashTime>0)
         {   if(moveDir.magnitude!=0)
            transform.Translate(xiuzheng*moveDir.normalized*moveSpeed*Time.deltaTime*2f);
            else
            transform.Translate(Vector3.right*moveSpeed*Time.deltaTime*2f);
            dashTime-=Time.deltaTime;
         }
         if(dashTime<=0)
         {
             animator.SetBool("IsDash",false);
         }
    }

    void Shoot()
    {
        //Instantiate(buttle,firePoint.transform.position,transform.rotation);
        //PoolManager.Release(buttle,firePoint.transform.position,firePoint.transform.rotation);
    }

    void Attack()
    {
        colseAtk.SetActive(true);
        AudioManager.audioManager.PlayPlayerClip(2);
    }
    void TheMove()
    {
        moveDir.x=Input.GetAxisRaw("Horizontal");
        //xiuzheng.x=moveDir.x;
        moveDir.y=Input.GetAxisRaw("Vertical");
        moveX=Input.GetAxisRaw("Horizontal")*moveSpeed;
        moveY=Input.GetAxisRaw("Vertical")*moveSpeed;
        if(moveX>0)
        transform.eulerAngles=new Vector3(0,0,0);
        if(moveX<0)
        transform.eulerAngles=new Vector3(0,180,0);
    }


    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.CompareTag("Enemy"))
        {
            GetHurt(5);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.CompareTag("Item"))
        {
            //Debug.Log(1);
            other.TryGetComponent<BaseItem>(out BaseItem nowItem);
            nowItem.GetItem(this);
        }
    }

    public void GetHurt(float dmg)
    {
        nowHp-=dmg;
        if(nowHp<0)
        //animator.SetTrigger("IsDie");
        {
            Instantiate(diePrefab,transform.position,Quaternion.identity);
            AudioManager.audioManager.PlayPlayerClip(1);
            this.transform.parent.gameObject.SetActive(false);
        }
        if(nowHp>maxHp)
        {
            nowHp=maxHp;
        }
        AcUimanager.instance.PlayerHPChange(nowHp,maxHp);
    }
/// <summary>
/// 计时器 所有需要计时的变量都可以在这个协程种进行
/// </summary>
/// <returns></returns>
    IEnumerator TimeCount()
    {
        while(true)
        {
            yield return cutTime;
            dashCD-=0.01f;
            atkTime-=0.01f;
            checkTime-=0.01f;
            transmitTime-=0.01f;
            cureTime-=0.01f;
        }
    }

/// <summary>
/// 距离检测 每0.2S执行一次
/// </summary>
    void DistanceCheck()
    {
        checkTime=0.2f;
        transmitTime=0.5f;
        foreach (var enemy in AcGameManager.instance.activeEnemyList)
        {
            nowdis=Mathf.Pow((transform.position.x-enemy.transform.position.x),2) +Mathf.Pow((transform.position.y-enemy.transform.position.y),2);
            //Debug.Log(nowdis);
            //nowdis=Vector2.Distance(transform.position,enemy.transform.position);
            if(nowdis>320&&transmitTime>0)
            {
                //调用怪物的移动代码将怪物传送到玩家附近
                
                enemy.GetComponent<Ac_Enemy>().Transmit();
            }
            if(nowdis<trueDis)
            AddEnemy(enemy);
            else 
            RemoveEnemy(enemy);
        }
        //transmitTime=0.5f;

    }

/// <summary>
/// 加入和移除敌人的代码
/// </summary>
/// <param name="enemyObject"></param>
    public void AddEnemy(GameObject enemyObject)
    {
        
        if(!enemyList.Contains(enemyObject))
        enemyList.Add(enemyObject);
    }
    public void RemoveEnemy(GameObject enemyObject)
    {
        if(enemyList.Contains(enemyObject ))
        enemyList.Remove(enemyObject );
    }

/// <summary>
/// 升级获取经验
/// </summary>
/// <param name="exp"></param>
    public void GetExp(float exp)
    {
        //传入的exp将会和玩家的经验值加成进行计算
        nowEXP+=exp;
        while(nowEXP>=maxExp)
        {
            AudioManager.audioManager.PlayPlayerClip(0);
            //后续调用改变玩家数值以及获取技能等方法
            PlayerLevelUp();
        }
        AcUimanager.instance.PlayerUiChange(nowEXP,maxExp,playerNowLevel);
    }
/// <summary>
/// 监听敌人死亡事件
/// </summary>
/// <param name="enemy"></param>
    public void EnemyDie(object enemy)
    {
        GetExp((enemy as Ac_Enemy).currentExp);
        RemoveEnemy((enemy as Ac_Enemy).gameObject);
    }
/// <summary>
/// 辅助线绘制 一般不用
/// </summary>
    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(transform.position,dis);
    }

    public void PlayerLevelUp()
    {
        nowEXP-=maxExp;
        playerNowLevel++;
        maxExp=playerNowLevel*playerNowLevel*1000;
        int x=Random.Range(0,3);
        //Debug.Log(x);
        switch (x)
        {
            case 0:
                atkSpeed+=0.1f;
            break;
            case 1:
                baseMoveSpeed+=0.1f;
            break;
            case 2:
                baseHp+=2f;
            break;
               

        } 

        basePower+=50;
        EventManager.instance.EventTrigger("PlayerLevelUp");
        if(playerNowLevel==5)
        firePoint[0].SetActive(true);
        UpdatePlayerData();
        AcUimanager.instance.PlayerHPChange(nowHp,maxHp);
    }

    public void UpdatePlayerData()
    {
        power=basePower;
        maxHp=baseHp;
        moveSpeed=baseMoveSpeed;
        for (int i = 0; i < myPercentDatas.Count; i++)
        {
            maxHp=maxHp*(1+myPercentDatas[i].hp);
            power=power*(1+myPercentDatas[i].power);
            moveSpeed=moveSpeed*(1+myPercentDatas[i].moveSpeed);
        }
        

        powerLevel=power/500;

        nowHp=maxHp;
        for (int i = 0; i < firePoint.Length; i++)
        {
            firePoint[i].TryGetComponent<UpdateMyData>(out UpdateMyData _gun);_gun.UpdateThisData(this);
        }

        colseAtk.TryGetComponent<UpdateMyData>(out UpdateMyData _closeAtk);
        _closeAtk.UpdateThisData(this);
        
        AcUimanager.instance.UpdatePlayerMeun(this);
    }
}



public class PercentPlayerData
{
    public float hp;
    public float power;
    public float atkSpeed;
    public float moveSpeed;
    public float exp;
    public int ID;

    public  PercentPlayerData(int id)
    {
        ID=id;
    }
}
[System.Serializable]
public struct NowStartCor
{
    public Coroutine mycor;
    public int id;
}