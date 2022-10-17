using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public  class UICon:MonoBehaviour
{
    // Start is called before the first frame update
    public static  void CloseUI(GameObject _this)
    {
        if(_this.activeSelf)
        _this.SetActive(false);
    }

    public static   void OpenUI(GameObject _this)
    {
        if(!_this.activeSelf)
        {
            _this.SetActive(true);
        }
    }

    public static  void ChangeUI(GameObject _this)
    {
        _this.SetActive(!_this.activeSelf);
    }

    public static void ChangeText<T>(Text _this,T _str)//T为字符串、数值得类型
    {
            _this.text=_str.ToString();
    }
}

