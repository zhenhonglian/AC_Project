using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close : MonoBehaviour
{
    // Start is called before the first frame update
    public void CloseThisGamobject()
    {
        
        //this.gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }

    public void CloseGameobject()
    {
        transform.gameObject.SetActive(false);
    }
}
