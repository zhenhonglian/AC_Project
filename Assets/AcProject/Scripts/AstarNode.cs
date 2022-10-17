using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//格子类型
public enum astarType
{
    walk,
    stop,
}

/// <summary>
/// A星格子类
/// </summary>
public class AstarNode
{
    //格子对象的坐标
    public int x;
    public int y;
    //寻路消耗
    public float f;
    //离起点的距离
    public float g;
    //离终点的距离
    public float h;
    //父对象
    public AstarNode father;
    //格子类型
    public astarType type;
     /// <summary>
     /// 构造函数
     /// </summary>
     /// <param name="x"></param>
     /// <param name="y"></param>
     /// <param name="type"></param>
    public AstarNode(int x,int y,astarType type)
    {
        this.x=x;
        this.y=y;
        this.type=type;
    }


}
