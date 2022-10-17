using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    
    [SerializeField]public GameObject prefab;
    Queue<GameObject> queue;
    [SerializeField]int size=1;
    Transform parent;
    public void Initialize(Transform parent)
    {
        queue=new Queue<GameObject>();
        this.parent=parent;
        for (int i = 0; i < size; i++)
        {
            queue.Enqueue(Copy());
        }
    }

    GameObject Copy()
    {
        var copy=GameObject.Instantiate(prefab,parent);
        copy.SetActive(false);
        return copy;
    }
    GameObject GetGameObject()
    {
        GameObject getGameObject=null;
        if(queue.Count>0&&!queue.Peek().activeSelf)
        {getGameObject= queue.Dequeue();}
        else
        {
            getGameObject=Copy();
        }
        queue.Enqueue(getGameObject);
        return getGameObject;
    }

    public GameObject GetPrefab()
    {
        GameObject newPrefab=GetGameObject();  
        if(newPrefab.TryGetComponent<IRestLoad>(out IRestLoad thisrest))
                thisrest.OnRest();
        newPrefab.SetActive(true);
        
      
        return newPrefab;
    }

}
