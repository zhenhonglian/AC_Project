using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveS
{
       private static Save save;
    public static Save _save
    {
        get
        {
            if(save==null)
                save=new Save();
            return save;
        }

    }
    public static void SaveLevel(LevelData[] _levelDatas)
    {
        LevelInfo levelListdate=new LevelInfo();
        for (int i = 0; i < _levelDatas.Length; i++)
        {
            if(_levelDatas[i]!=null)
            {
               levelListdate.levelData.Add(_levelDatas[i]);
            }
        }
        string json=JsonUtility.ToJson(levelListdate);
        string filepath=Application.streamingAssetsPath+"/"+PlayerSence.playerName+"/PlayerLevelList.json";
        using(StreamWriter sw=new StreamWriter(filepath))
        {
                sw.WriteLine(json);
                sw.Close();
                sw.Dispose();

        }
    }
}
