using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private GameObject lockImage;
    [SerializeField]private int levelNum;
    [SerializeField]private int testNum;
    [SerializeField]public LevelData thisLevelDate=new LevelData();
    private string levelName;
    //int isLock;//islock 如果是0.则关卡未解锁，如果为1则表示关卡解锁
    void Start()
    {
        
    }

    public void UpdateLevel()
    {
         if(thisLevelDate.levelID==0)
        {
            thisLevelDate.isLock=false;
            //thisLevelDate.isPushDown=false;
        }
        else if(thisLevelDate.levelID!=0)
        {
            if(thisLevelDate.frontLevel.isPushDown)
                thisLevelDate.isLock=false;
            else 
                thisLevelDate.isLock=true;
        }





        
        if(thisLevelDate.isLock)
            lockImage.SetActive(true);
        else 
            lockImage.SetActive(false);
    }

    // Update is called once per frame
}
