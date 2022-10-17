using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 音效管理
/// </summary>
public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioManager audioManager;
    //public AudioClip shootClip;
    [SerializeField] List<AudioClip> playerClip=new List<AudioClip>();
    //public AudioClip bgmClip;
    AudioSource playerSource,bgmSource;
    //AudioSource bgmSource;
    Dictionary<int,AudioClip> clipDic=new Dictionary<int, AudioClip>();
    private void Awake() 
    {
        if(audioManager==null)
        {
            audioManager=this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        playerSource=gameObject.AddComponent<AudioSource>();
        //bgmSource=gameObject.AddComponent<AudioSource>();
    }
    void Start()
    {
        bgmSource=GetComponent<AudioSource>();

        if(bgmSource!=null&&PlayerPrefs.HasKey("AudioVolume"))
        {
            bgmSource.volume=PlayerPrefs.GetFloat("AudioVolume");
            playerSource.volume=PlayerPrefs.GetFloat("AudioVolume");
        }
        else
        {
            bgmSource.volume=1f;
        }
        AcUimanager.instance.volumeSlider.value=bgmSource.volume;
        for (int i = 0; i < playerClip.Count; i++)
        {
            clipDic.Add(i,playerClip[i]);
        }
    }

    // Update is called once per frame
    public void ChangeVolume(float value)
    {
        if(bgmSource!=null)
        bgmSource.volume=value;
        if(playerSource!=null)
        playerSource.volume=value;

        PlayerPrefs.SetFloat("AudioVolume",value);
    }

    public void PlayPlayerClip(int _id)
    {
        playerSource.clip=clipDic[_id];
        playerSource.Play();
    }
    
}
