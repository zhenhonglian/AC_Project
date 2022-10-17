using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePoint : MonoBehaviour
{
    [Header("旋转")]
    public float Speed;
    public float startAngle;
    float progess;
    public Transform center;
    public float radius;
    private void Start() 
    {
        progess=startAngle*Mathf.Deg2Rad;
        float x=center.position.x+radius*Mathf.Cos(progess);
        float y=center.position.y+radius*Mathf.Sin(progess);
    }
    void Update()
    {
        Move();
    }
    void Move()
    {
        progess+=Time.deltaTime*Speed;
        if(progess>=2*Mathf.PI) progess=0;
        float x=center.position.x+radius*Mathf.Cos(progess);
        float y=center.position.y+radius*Mathf.Sin(progess);
        this.transform.position=new Vector3(x,y);
    }

    // Update is called once per frame
}
