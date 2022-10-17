using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamText : MonoBehaviour,IRestLoad
{
    // Start is called before the first frame update
    [SerializeField]Text myDamText;
    [SerializeField]float activeTime;
    [SerializeField]float moveSpeed;
    Color changeColor;
    float nowMoveSpeed;
    public e_DamgeType damgeType;
    private bool isChangeColor;
    float x;
    private void OnEnable() 
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isChangeColor)
        {
            switch(damgeType)
            {
                case e_DamgeType.blood:
                    changeColor=Color.red;
                break;
                case e_DamgeType.clod:
                    changeColor=Color.blue;
                break;
                case e_DamgeType.normal:
                    changeColor=Color.white;
                break;
                case e_DamgeType.fire:
                    changeColor=Color.yellow;
                break;
            }
            isChangeColor=false;
        }
        x+=Time.deltaTime;
        nowMoveSpeed+=0.5f;
        transform.Translate(Vector2.up*nowMoveSpeed*Time.deltaTime);
        changeColor.a-=0.02f;
        myDamText.color=changeColor;
        if(x>=activeTime)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void ChangeDamText(float dam)
    {
        myDamText.text=dam.ToString();
    }

    public void OnRest()
    {
        x=0f;
        //changeColor=Color.white;
        isChangeColor=true;
        nowMoveSpeed=moveSpeed;
    }
}
