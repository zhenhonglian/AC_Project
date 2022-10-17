using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomPlant : MonoBehaviour
{
    Tilemap tilemap;

    private List<Vector3> randomCells=new List<Vector3>();

    public List<GameObject> Plants=new List<GameObject>();
    int z;
    private void Start() {
        InitializeTileMap();
        CreatPlant();
    }

    void InitializeTileMap()
    {
        tilemap=GetComponent<Tilemap>();
        Vector3Int tmOrg=tilemap.origin;
        //Debug.Log(tmOrg);
        Vector3Int tmSz=tilemap.size;
        //Debug.Log(tmSz);
        for (int i = tmOrg.x; i < tmSz.x; i++)
        {
            for (int y = tmOrg.y; y <tmSz.y; y++)
            {
                if(tilemap.GetTile(new Vector3Int(i,y,0))!=null)
                {
                    Vector3 cellToWorldPos=tilemap.GetCellCenterWorld(new Vector3Int(i,y,0));
                    randomCells.Add(cellToWorldPos);

                }
            }
        }
    }

    void CreatPlant()
    {
        z=randomCells.Count;
        for (int i = 0; i < z*0.8; i++)
        {
            int x=Random.Range(0,randomCells.Count);
            Vector3 spwanPos=randomCells[x];
            int y=Random.Range(0,Plants.Count);
            Instantiate(Plants[y],spwanPos,Quaternion.identity).transform.parent=this.gameObject.transform;
            randomCells.Remove(randomCells[x]);
            //z++;
        }
        //Debug.Log(z);
    }
}
