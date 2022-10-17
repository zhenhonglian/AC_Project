using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    public string Fathername;//如果是同一父名 则表示两个UI界面之间的关系为平行，当一个打开另一个需要关闭

    private bool isFirstTime=true;
    // Start is called before the first frame update
    private void Awake() {
        if(Fathername!=null)
        EventManager.instance.AddEventListener(Fathername,ClosePaner);
    }

    public void ChangePanel()
    {
        if(isFirstTime)
        this.gameObject.SetActive(true);
        //hasOpen=true;
        EventManager.instance.EventTrigger(Fathername,this);
       
    }
    public void ClosePaner(object objectinfo)
    {
        //Debug.Log((objectinfo as BasePanel)==this);
        if((objectinfo as BasePanel)==this&&!isFirstTime)
        {
            
            this.gameObject.SetActive(!gameObject.activeSelf);
        }
        else if((objectinfo as BasePanel)==this&&isFirstTime)
        {
            //this.gameObject.SetActive(isFirstTime);
            isFirstTime=false;
        }
        else
        {this.gameObject.SetActive(false);}
        //Debug.Log("有兄弟激活了");
    }

}
