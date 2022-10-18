using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AcGameManager instance;
    private float maxX,maxY,minX,minY;
    public List<GameObject> enemyPrefabs=new List<GameObject>();
    public GameObject bossPrefab;
    int bossCome,warriorCome;
    WaitForSeconds time;
    public float spwanTime;
    Vector2 enemyBornPos;
    float fps;
    float oneTime;
    float k;
    public List<GameObject> activeEnemyList=new List<GameObject>();
    float gameTime=0f;//将会影响地图等级
    public float currentTime;
    public static int mapLevel;//将会影响怪物当前的数值和生成怪物的种类
    int maxMapLevl=20;
    int spwanLevel;//影响敌人生成效率 当场景中怪物过多时降低生成速度 尽量保证怪物数量少于1000
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
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
        bossCome=0;
        time=new WaitForSeconds(1/spwanTime);
        Application.targetFrameRate=60;
        //EventManager.instance.AddEventListener("EnemyDie",CreatItem);
        StartCoroutine(CreatEnemyIE());
    }

    // Update is called once per frame
    void Update()
    {
        //EdgeCheck();
        //Debug.Log(1/Time.deltaTime);
        fps+=(1/Time.deltaTime);
        oneTime+=Time.deltaTime;
        k++;
        if(oneTime>=1)
        {
            oneTime=0;
            //Debug.Log(fps/k);
            fps=0;
            k=0;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OpenPanel(AcUimanager.instance.menuPanel);
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            OpenPanel(AcUimanager.instance.playerMenu);
            //CountCount();
        }

        SpwanEnemyCheck();
        //Debug.Log(time);
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

    private void SpwanEnemyCheck()
    {
        gameTime+=Time.deltaTime;
        currentTime+=Time.deltaTime;
        if(gameTime>=45)
        {
            if(mapLevel<maxMapLevl)
            mapLevel++;
            gameTime=0;
            time=new WaitForSeconds(1/(spwanTime+mapLevel*0.5f));
        }
        if(activeEnemyList.Count>=1000)
        {
            time=new WaitForSeconds(1/spwanTime);
        }
        else time=new WaitForSeconds(1/(spwanTime+mapLevel*0.5f));
    }

    IEnumerator CreatEnemyIE()
    {
        //enemyAmount=Mathf.Clamp(enemyAmount,minEnemyCount+enmeyWave,maxEnemyCount);
        
        while(true)
        {   
            
            EdgeCheck();
            GameObject theEnemy;
            if(bossCome<100)
            {
                theEnemy= PoolManager.Release(enemyPrefabs[0],enemyBornPos);
                bossCome++;
                warriorCome++;
                if(warriorCome>30)
                {
                    activeEnemyList.Add(PoolManager.Release(enemyPrefabs[1],enemyBornPos));
                    warriorCome=0;
                }
            }
            else 
            {
                theEnemy=PoolManager.Release(bossPrefab,enemyBornPos);
                bossCome=0;
            }   

            if(!activeEnemyList.Contains(theEnemy))
            activeEnemyList.Add(theEnemy);
            yield return time;
        }
       
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void CloseUi(GameObject _this)
    {
        UICon.CloseUI(_this);
        Time.timeScale=1f;
    }
    private void OpenPanel(GameObject ojb)
    {
        //UICon.ChangeUI(ojb);
        ojb.TryGetComponent<BasePanel>(out BasePanel nowOjb);
        nowOjb.ChangePanel();
        //if(AcUimanager.instance.menuPanel.activeSelf)
        //Time.timeScale=0f;
        //else
        //Time.timeScale=1f;

        Time.timeScale=ojb.activeSelf?0f:1f;
    }


    private void CountCount()
    {
        int x=0;
        int y=0;
        int k=0;
        for (int i = 0; i < 100; i++)
        {
            for (int z = 0; z < 10000; z++)
            {
            if(Random.Range(0,100f)<=5)
            {
                y++;
            }
            x++;
            if(y>=8)
            break;
            }
            k+=x;
            x=0;
            y=0;
        }
        
        Debug.Log(k/100);
    }
}
