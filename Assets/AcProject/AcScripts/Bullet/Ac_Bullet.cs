using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ac_Bullet : MonoBehaviour,IRestLoad
{
    // Start is called before the first frame update
    protected float Speed=20f;
    public float dmg=0f;
    protected float activeTime=0.5f;

    public virtual void OnRest()
    {
        activeTime=0.3f;
    }


    // Update is called once per frame
    void Update()
    {
       Move();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.transform.CompareTag("Enemy"))
        {
            //Debug.Log("aaa");
            gameObject.SetActive(false);
            other.gameObject.GetComponent<Ac_Enemy>().GetHurt(dmg);
        }
    }

    protected virtual void Move()
    {
        transform.Translate(Vector3.up*Speed*Time.deltaTime);
        activeTime-=Time.deltaTime;
        if(activeTime<=0)
        gameObject.SetActive(false);
    }
}
