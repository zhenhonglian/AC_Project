using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A星寻路管理器
/// </summary>
public class AstarManager 
{
    //地图的宽高
    private int mapW;
    private int mapH;
    //单例模式
    private static AstarManager astarManager;
    public static AstarManager AStarManager
    {
        get
        {
            if(astarManager==null)
                astarManager=new AstarManager();
            return astarManager;
        }

    }
    //格子
    public AstarNode[,] nodes;
    //开启列表
    private List<AstarNode> openList=new List<AstarNode>();
    //关闭列表
    private List<AstarNode> closeList=new List<AstarNode>();

/// <summary>
/// 初始化地图信息
/// </summary>
/// <param name="w"></param>
/// <param name="h"></param>
    public void InitMapInfp(int w,int h )
    {
        this.mapW=w;
        this.mapH=h;
        nodes=new AstarNode[w,h];
    }
    /// <summary>
    /// 传入NODE参数
    /// </summary>
    /// <param name="w"></param>
    /// <param name="h"></param>
    /// <param name="type"></param>
    public void InitNodesInfo(int w,int h, int type)
    {
        //随机阻挡后续需要从配置文件中获取
        AstarNode node=new AstarNode(w,h,type>=1?astarType.walk:astarType.stop);
        nodes[w,h]=node;  
    }

/// <summary>
/// 寻路方法 外部调用
/// </summary>
/// <param name="startPos"></param>
/// <param name="endPos"></param>
/// <returns></returns>
    public List<AstarNode> FindPath(Vector2 startPos,Vector2 endPos)
    {
        //判断点是否合法
        if(startPos.x<0||startPos.x>=mapW||
            startPos.y<0||startPos.y>=mapH||
            startPos.x<0||startPos.x>=mapW||
            startPos.y<0||startPos.y>=mapH)
            return null;
        //判断是否为阻挡
        AstarNode start=nodes[(int)startPos.x,(int)startPos.y];
        AstarNode end=nodes[(int)endPos.x,(int)endPos.y];
        if(start.type==astarType.stop||end.type==astarType.stop)
        {
            Debug.Log("开始或结束点被阻挡");
            return null;
        }
        //清空关闭和开启列表
        closeList.Clear();
        openList.Clear();
        //把开始点放入关闭列表；
        start.father=null;
        start.f=0;
        start.g=0;
        start.h=0;
        closeList.Add(start);
        //开始寻路
        while (true)
        {
                     //上
        FindNearlyNode(start.x,start.y-1,1,start,end);
        //下
        FindNearlyNode(start.x,start.y+1,1,start,end);
        //左
        FindNearlyNode(start.x-1,start.y,1,start,end);
        //右
        FindNearlyNode(start.x+1,start.y,1,start,end);
        //死路判断
        if(openList.Count==0)
            {
                Debug.Log("死路");
                return null;
            }
        //选择消耗最小的点
        openList.Sort(SortOpenList);
        closeList.Add(openList[0]);      
        start=openList[0];
        openList.RemoveAt(0);
        if(start==end)
            {
                //找完了
                List<AstarNode> path=new List<AstarNode>();
                path.Add(end);
                while (end.father!=null)
                {
                     path.Add(end.father);
                     end=end.father;
                }
                //列表反转
                path.Reverse();
                return path;
            }
        }
    
    }
    /// <summary>
    /// 排序函数
    /// </summary>
    /// <param name="astarNode"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private int SortOpenList(AstarNode a,AstarNode b)
    {
        if(a.f>b.f)
            return 1;
        else
            return -1;

    }

/// <summary>
/// 放入开启列表 并记录F值
/// </summary>
/// <param name="x"></param>
/// <param name="y"></param>
    private void FindNearlyNode(int x,int y,float g,AstarNode father,AstarNode end)
    {
        //边界判断
        if(x<0||x>=mapW||y<0||y>=mapH)
            return;
        AstarNode node=nodes[x,y];
        if(node==null||node.type==astarType.stop
            ||closeList.Contains(node)||openList.Contains(node))
            return;
        //计算f值
        node.father=father;
        node.g=father.g+g;
        node.h=Mathf.Abs(end.x-node.x)+Mathf.Abs(end.y-node.y);
        node.f=node.g+node.h;
        //加入开启列表
        openList.Add(node);

    }

}
