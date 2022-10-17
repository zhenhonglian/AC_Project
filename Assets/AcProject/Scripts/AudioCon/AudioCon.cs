using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioCon : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource thisAudioSource;
    public Slider volumeSlider;
    private int x;
    void Start()
    {
        thisAudioSource=gameObject.GetComponent<AudioSource>();
        if(thisAudioSource!=null&&PlayerPrefs.HasKey("AudioVolume"))
        {
            thisAudioSource.volume=PlayerPrefs.GetFloat("AudioVolume");
        }
        else
        {
            thisAudioSource.volume=1f;
        }
        volumeSlider.value=thisAudioSource.volume;


    }

    public void ChangeVolume(float value)
    {
        if(thisAudioSource!=null)
        thisAudioSource.volume=value;
        PlayerPrefs.SetFloat("AudioVolume",value);
    }
}
