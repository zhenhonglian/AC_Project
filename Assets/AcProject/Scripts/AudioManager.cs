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

    List<AudioSource> gameSources=new List<AudioSource>();
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

        //playerSource=gameObject.AddComponent<AudioSource>();
        //bgmSource=gameObject.AddComponent<AudioSource>();
        for (int i = 0; i < 5; i++)
        {
            AudioSource _freeaudio=gameObject.AddComponent<AudioSource>();
            gameSources.Add(_freeaudio);
        }
    }
    void Start()
    {
        bgmSource=GetComponent<AudioSource>();

        if(bgmSource!=null&&PlayerPrefs.HasKey("BgmVolume"))
        {
            bgmSource.volume=PlayerPrefs.GetFloat("BgmVolume");
        }
        else
        {
            bgmSource.volume=1f;
        }
        if(PlayerPrefs.HasKey("AudioVolume"))
        {
                        for (int i = 0; i < gameSources.Count; i++)
            {
                gameSources[i].volume=PlayerPrefs.GetFloat("AudioVolume");
            }
        }
        else{
                        for (int i = 0; i < gameSources.Count; i++)
            {
                gameSources[i].volume=1f;
            }
        }

        AcUimanager.instance.volumeSlider.value=bgmSource.volume;
        AcUimanager.instance.volumeSliderS.value=gameSources[0].volume;
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
        PlayerPrefs.SetFloat("BgmVolume",value);
    }
    public void ChangeVolumeS(float value)
    {
        for (int i = 0; i < gameSources.Count; i++)
            {
                gameSources[i].volume=value;
            }
        PlayerPrefs.SetFloat("AudioVolume",value);
    }

    public void PlayPlayerClip(int _id)
    {
        playerSource=ChoseAudioSource();
        playerSource.clip=clipDic[_id];
        playerSource.Play();
    }
    
    private AudioSource ChoseAudioSource()
    {
        for (int i = 0; i < gameSources.Count; i++)
        {
            if(!gameSources[i].isPlaying)
            {
                return gameSources[i];
                //break;
            }
        }
        AudioSource _freeaudio=gameObject.AddComponent<AudioSource>();
        _freeaudio.volume=PlayerPrefs.HasKey("AudioVolume")?PlayerPrefs.GetFloat("AudioVolume"):1;
        gameSources.Add(_freeaudio);
        return _freeaudio;
    }
}
