using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    //public 
    float x;
    private void Start() {
        x=this.gameObject.transform.localScale.x;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.gameObject.transform.localScale=new Vector3(x*1.1f,1.1f,1.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.gameObject.transform.localScale=new Vector3(x,1f,1f);
    }
}
