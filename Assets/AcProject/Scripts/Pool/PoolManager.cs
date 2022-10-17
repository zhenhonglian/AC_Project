using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IRestLoad
{
    void OnRest();
}

public class PoolManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static PoolManager poolManager;
    [SerializeField]Pool[] enmeyPrefabPools;
    [SerializeField]Pool[] bulletPrefabPools;
    [SerializeField]Pool[] damTextPrefabPool;
    //[SerializeField]Pool[] hitEffectPool;
    static Dictionary<GameObject,Pool>  poolDictionary=new Dictionary<GameObject, Pool>();
    private void Awake() 
    {
         if(poolManager==null)
        {
            poolManager=this;
           // DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }
    void Start()
    {
        poolDictionary.Clear();
        Initialize(enmeyPrefabPools,bulletPrefabPools,damTextPrefabPool);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    void Initialize(Pool[] pools,Pool[] pools1,Pool[] pools2)
    {
        for (int i = 0; i <pools.Length; i++)
        {
            poolDictionary.Add(pools[i].prefab,pools[i]); 
            Transform poolParent= new GameObject("Pool:"+pools[i].prefab.name).transform;
            poolParent.parent=transform;
            pools[i].Initialize(poolParent);

        }
        for (int i = 0; i < pools1.Length; i++)
        {

            poolDictionary.Add(pools1[i].prefab,pools1[i]); 
            Transform poolParent= new GameObject("Pool:"+pools1[i].prefab.name).transform;
            poolParent.parent=transform;
            pools1[i].Initialize(poolParent);
        }
         for (int i = 0; i < pools2.Length; i++)
        {

            poolDictionary.Add(pools2[i].prefab,pools2[i]);       
            Transform poolParent= new GameObject("Pool:"+pools2[i].prefab.name).transform;
            poolParent.parent=transform;
            pools2[i].Initialize(poolParent);
        }
        

    }
 /// <summary>
 /// 
 /// </summary>
 /// <param name="prefab">需要生成的预制体</param>
 /// <param name="pos">生成位置的坐标</param>
 /// <returns></returns>
    public static GameObject Release(GameObject prefab,Vector2 pos)
    {
        //GameObject getGameObject=null;
        GameObject getGameObject=null;
        if(poolDictionary.ContainsKey(prefab))
        {
            getGameObject=poolDictionary[prefab].GetPrefab();
            getGameObject.transform.position=new Vector2(pos.x,pos.y);

        
        }
        else
        {
            Debug.Log("没有对应对象池"+prefab.name);
        }
        

        return getGameObject;
    }
    public static GameObject Release(GameObject prefab,Vector2 pos,Vector3 rot)
    {
        //GameObject getGameObject=null;
        GameObject getGameObject=null;
        if(poolDictionary.ContainsKey(prefab))
        {
            getGameObject=poolDictionary[prefab].GetPrefab();
            getGameObject.transform.position=new Vector2(pos.x,pos.y);
            getGameObject.transform.up=rot;
        }
        else
        {
            Debug.Log("没有对应对象池"+prefab.name);
        }
        return getGameObject;
    }
        public static GameObject Release(GameObject prefab,Vector2 pos,Quaternion rot)
    {
        //GameObject getGameObject=null;
        GameObject getGameObject=null;
        if(poolDictionary.ContainsKey(prefab))
        {
            getGameObject=poolDictionary[prefab].GetPrefab();
            getGameObject.transform.position=new Vector2(pos.x,pos.y);
            getGameObject.transform.rotation=rot;
        }
        else
        {
            Debug.Log("没有对应对象池"+prefab.name);
        }
        return getGameObject;
    }

/*     public GameObject ReleaseBullet(GameObject prefab,Vector2 pos)
    {
        GameObject bullet=poolDictionary[prefab].GetPrefab();
        bullet.transform.position=new Vector2(pos.x,pos.y);
        
        return bullet;
    } */
}
