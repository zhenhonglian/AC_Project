using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcCameraCon : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    // Update is called once per frame

    private void Awake() 
    {
        player=FindObjectOfType<AcPlayerCon>().gameObject;

        
    }
    void Update()
    {
        Move();
    }

    void Move()
    {
        if(player!=null)
        this.transform.position=new Vector3(player.transform.position.x,player.transform.position.y,-10f);
    }
}
