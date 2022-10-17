using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
/// <summary>
/// 玩家界面各种UI交互
/// </summary>
public class PlayerSence : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]public static Skill[] mySkill=new Skill[4];//当前玩家的4个技能
    private MySkill skillListdata=new MySkill();//存储的技能
    private LevelInfo levelListdate=new LevelInfo();//存储的技能
    [SerializeField]GameObject[] myNowSkill=new GameObject[4];//当前玩家四个技能的UI控件
    [SerializeField]Sprite defaultSkillImage;//默认技能图标
    [SerializeField]GameObject[] playerPanelS=new GameObject[2];//玩家界面各种子界面（技能、关卡）
    [Header("PlayerSkillDsc")]
    public GameObject playerSkillDsc;
    public Image skillImage;
    public Text skillDsc,skillName;
    public static LevelData[] levelDatas=new LevelData[3];
    public  LevelData[] nowlevelDatas=new LevelData[3];
    public GameObject[] level=new GameObject[3];
    //public List<Skill> skillList=new List<Skill>();
    //用户名称
    public static string playerName;
    public  string playername;
    public GameObject setButton,menuPanel;
    public static PlayerSence instences;

    public Dictionary<int, Skill> skillDict=new Dictionary<int, Skill>();

    public int skillPrefabsLength;//技能总数

    [System.Serializable]
    public struct SkillPrefab
    {
        public int skillid;
        public Skill prefab;
    }

    public SkillPrefab[] skillPrefabs;
    /*private void Update() {
        shuaPiaoJisuan();
    }*/
    void Start()
    {
        skillPrefabs=new SkillPrefab[skillPrefabsLength];
        //UpdateNowSkillUi();
        //playerPanelS=new GameObject[2];
        for (int i = 0; i < skillPrefabs.Length; i++)
        {
            Debug.Log(i.ToString());
            skillPrefabs[i].skillid=i;
            skillPrefabs[i].prefab=Resources.Load(i.ToString()) as Skill;
        }
        for (int i = 0; i < skillPrefabs.Length; i++)
        {
            if(!skillDict.ContainsKey(skillPrefabs[i].skillid))
            {
                skillDict.Add(skillPrefabs[i].skillid,skillPrefabs[i].prefab);
            }
        }
        //playerName=playername;
        menuPanel.SetActive(false);
        playername=playerName;
        LoadData();
        //LevelLoad();
    }

    private void Awake() 
    {
        if(instences==null)
        {
            instences=this;
        }
        else
        {
            Destroy(instences);
        }
    }

    // Update is called once per frame
    public void ChoseSkill(Skill _skill)
    {
        int _index=-1;
        bool canEquit=true;
        for (int i = 0; i <mySkill.Length; i++)
        {
            //if()
            if(mySkill[i]==null)
            {
                _index=i;
               
                break;
            }
        }
        for (int i = 0; i < mySkill.Length; i++)
        {
            if(_skill==mySkill[i])
            {
                Debug.Log("已经携带相同技能");
                //_index=-1;
                canEquit=false;
                break;
            }
        }
        if(_index<0)
        Debug.Log("你已经装备4个技能 无法携带更多技能");
        else
        {
            if(canEquit)
            {
                mySkill[_index]=_skill;
                myNowSkill[_index].GetComponent<Image>().sprite=mySkill[_index].skillImage;
            }
        } 
    }

    public void ResetSkill(int _index)
    {
        if(mySkill[_index]!=null)
        {
            mySkill[_index]=null;
            UpdateNowSkillUi();
        }
    }

    public void UpdateNowSkillUi()
    {
         if(mySkill!=null)
        {
            for (int i = 0; i < mySkill.Length; i++)
            {
                if(mySkill[i]!=null&&mySkill[i].skillImage!=null)
                myNowSkill[i].gameObject.GetComponent<Image>().sprite=mySkill[i].skillImage;
                else
                myNowSkill[i].gameObject.GetComponent<Image>().sprite=defaultSkillImage;
            }
        }
    }

    public void OpenSkillPanel(int x)
    {
        for (int i = 0; i < playerPanelS.Length; i++)
        {
            if(i==x)
            playerPanelS[i].SetActive(true);
            else
            playerPanelS[i].SetActive(false);
        }
    }




    public void LevelLoad()
    {
        if(levelDatas[0]!=null)
        {
            for (int i = 0; i < levelDatas.Length; i++)
            {
            
                if(i==0)
                levelDatas[i].frontLevel=null;
                else
                levelDatas[i].frontLevel=levelDatas[i-1];
                level[i].GetComponent<LevelButton>().thisLevelDate=levelDatas[i];
                level[i].GetComponent<LevelButton>().UpdateLevel();
            }
        }
        else
            {
                for (int i = 0; i < levelDatas.Length; i++)
                {
                    levelDatas[i]=level[i].GetComponent<LevelButton>().thisLevelDate;
                }
                for (int i = 0; i < levelDatas.Length; i++)
                {
            
                    if(i==0)
                    levelDatas[i].frontLevel=null;
                    else
                    levelDatas[i].frontLevel=levelDatas[i-1];
                    level[i].GetComponent<LevelButton>().thisLevelDate=levelDatas[i];
                    level[i].GetComponent<LevelButton>().UpdateLevel();
                }
            }
    }

/// <summary>
/// 保存数据
/// </summary>
    public void Save()
    {
        //SaveData();
        SkillSave();
        LevelSave();
    }

    private  void LoadData()
    {
        string json;
        //加载技能信息
        string filepath=Application.streamingAssetsPath+"/"+playerName+"/PlayerSkillList.json";
        //Debug.Log(filepath.ToString());
        if(File.Exists(filepath))
        {
               using(StreamReader sr=new StreamReader(filepath))
            {
           
                json=sr.ReadToEnd();
                sr.Close();
           
            }
            skillListdata=JsonUtility.FromJson<MySkill>(json);
  
            for (int i = 0; i <skillListdata.mySkill.Count; i++)
            {
                mySkill[i]=skillDict[skillListdata.mySkill[i].skillID];
            }
        }
        UpdateNowSkillUi();
        //加载关卡信息
        filepath=Application.streamingAssetsPath+"/"+playerName+"/PlayerLevelList.json";
        if(File.Exists(filepath))
        {
               using(StreamReader sr=new StreamReader(filepath))
            {
           
                json=sr.ReadToEnd();
                sr.Close();
           
            }
            levelListdate=JsonUtility.FromJson<LevelInfo>(json);
            for (int i = 0; i <levelListdate.levelData.Count; i++)
            {
                levelDatas[i]=levelListdate.levelData[i];
            }
        }
        LevelLoad();
        
    }
    private void SkillSave()
    {
        skillListdata.mySkill.Clear();
        for (int i = 0; i < mySkill.Length; i++)
        {
            if(mySkill[i]!=null)
            {   skilldata skillnow=new skilldata();
                skillnow.skillID=mySkill[i].skillID;
                skillListdata.mySkill.Add(skillnow);
            }
        }
        string json=JsonUtility.ToJson(skillListdata);
        string filepath=Application.streamingAssetsPath+"/"+playerName+"/PlayerSkillList.json";

        using(StreamWriter sw=new StreamWriter(filepath))
        {
          
                sw.WriteLine(json);
                sw.Close();
                sw.Dispose();
        }

    }
    private void LevelSave()
    {
        levelListdate.levelData.Clear();
        for (int i = 0; i < levelDatas.Length; i++)
        {
            if(levelDatas[i]!=null)
            {
               levelListdate.levelData.Add(levelDatas[i]);
            }
        }
        string json=JsonUtility.ToJson(levelListdate);
        string filepath=Application.streamingAssetsPath+"/"+playerName+"/PlayerLevelList.json";
        using(StreamWriter sw=new StreamWriter(filepath))
        {
                sw.WriteLine(json);
                sw.Close();
                sw.Dispose();
                //GetComponent
        }
    }

    public void ChangePanelActive(GameObject gameObject)
    {
        if(gameObject.activeSelf)
        {
            if(gameObject.TryGetComponent<IRestLoad>(out IRestLoad rest))
            rest.OnRest();

        }
        gameObject.SetActive(!gameObject.activeSelf);
        
    }
/// <summary>
/// 强化次数计算
/// </summary>
    public void cishujisuan()
    {
            if(Input.GetKeyDown(KeyCode.S))
        {
            float time=0;
            for (int i = 0; i < 10000; i++)
            {
                   int x =0;
                int z=0;
                while(x<5)
                {
                    z++;
                    float y= Random.Range(0,100);
                    if(y>=50)
                        x+=1;
                    if(y<50&&x>0)
                        x-=1;
                }
                time+=z;
            }
            Debug.Log(time/10000);
            
        }
    }
/// <summary>
/// 刷商店获取道具计算
/// </summary>
    public void shuaPiaoJisuan()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            float time1=0;
            float time2=0;
            for (int i = 0; i < 199800; i++)
            {
    
                    float y= Random.Range(0,10000);
                    if(y<=17)
                        time1+=1;
                    if(y>17&&y<=66)
                        time2+=1;
    
            }
            Debug.Log("获得神秘："+time1/100+"获得书签："+time2/100);
            
        }
    }
}
[System.Serializable]
public class MySkill
{
    public List<skilldata> mySkill=new List<skilldata>();
}
[System.Serializable]
public class skilldata
{
    public int skillID;
}
[System.Serializable]
public class LevelInfo
{
    public List<LevelData> levelData=new List<LevelData>();
}
[System.Serializable]
public class LevelData
{
    public int levelID;
    public bool isLock;
    public bool isPushDown;
    public LevelData frontLevel;
}
