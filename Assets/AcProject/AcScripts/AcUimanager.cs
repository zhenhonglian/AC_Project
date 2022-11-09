using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AcUimanager : MonoBehaviour
{
    public static AcUimanager instance;   
    [Header("玩家UI")]
    public Text uPlayerLevel,mapLevel,gameTime;
    public Text playerExp,playerHp,playerPower,playerAtkSpeed,playerMoveSpeed;
    public Image uNowEXP,uMidEXP,uNowHp,uMidHp;
    public GameObject menuPanel,playerMenu;
    public float expChangeSpeed;
    [Header("音效")]
    public Slider volumeSlider;
    public Slider volumeSliderS;

    //地图信息
    void Awake()
    {
         if(instance==null)
        {
            instance=this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    

    void Start()
    {
        StartCoroutine(UpdatePlayerUi());
    }

    public void PlayerUiChange(float x,float y,int z)
    {
        uMidEXP.fillAmount=x/y;
        playerExp.text=x.ToString()+"/"+y.ToString();
        if(uNowEXP.fillAmount>uMidEXP.fillAmount)
            uNowEXP.fillAmount=uMidEXP.fillAmount;
        uMidEXP.fillAmount=x/y;
        uPlayerLevel.text=z.ToString();
    }
    public void PlayerHPChange(float x,float y)
    {
        uNowHp.fillAmount=x/y;
        playerHp.text=x.ToString()+"/"+y.ToString();
        if(uNowHp.fillAmount>uMidEXP.fillAmount)
            uMidHp.fillAmount=uNowHp.fillAmount;
    }
    IEnumerator  UpdatePlayerUi()
    {

        while(true)
        {
            if(uNowEXP.fillAmount<uMidEXP.fillAmount)
            uNowEXP.fillAmount+=expChangeSpeed;
            if(uNowHp.fillAmount<uMidHp.fillAmount)
            uMidHp.fillAmount-=expChangeSpeed;
            mapLevel.text=AcGameManager.mapLevel.ToString();
            GetTime();
            yield return new WaitForSeconds(0.01f);
        }
        //Debug.Log(playerNowHp.fillAmount);
    }

    void GetTime()
    {


            System.TimeSpan nowTime=new System.TimeSpan(0,0,(int)AcGameManager.instance.currentTime);

            gameTime.text=nowTime.ToString().Substring(3);
    }
    public void UpdatePlayerMeun(AcPlayerCon _player)
    {
        playerHp.text=_player.nowHp.ToString("F2")+"/"+_player.maxHp.ToString("F2");
        playerExp.text=_player.nowEXP.ToString("F2")+"/"+_player.maxExp.ToString("F2");
        playerPower.text=_player.power.ToString("F2");
        playerAtkSpeed.text=_player.AtkSpeed.ToString("F2");
        playerMoveSpeed.text=_player.moveSpeed.ToString("F2");
    }
}
