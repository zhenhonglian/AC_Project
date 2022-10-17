using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_Dao : MonoBehaviour, UpdateMyData
{
    // Start is called before the first frame update
    public float dam;
    private float trueDmg;
    Vector2 baseSize=new Vector2(3.5f,1.5f);

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.transform.CompareTag("Enemy"))
        {
            //Debug.Log("aaa");
            //gameObject.SetActive(false);
            other.gameObject.GetComponent<Ac_Enemy>().GetHurt(trueDmg);
        }
    }
    public void CloseAnim()
    {
        this.gameObject.SetActive(false);
        //transform.position=new Vector3(0,0,0);
    }

    public void UpdateThisData(AcPlayerCon _player)
    {
        trueDmg=dam*(1+_player.powerLevel);
        transform.localScale=new Vector3(baseSize.x*(1+_player.closeAtkSize),baseSize.y*(1+_player.closeAtkSize),1);
        
    }
}
