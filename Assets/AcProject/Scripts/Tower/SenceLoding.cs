using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// 异步加载
/// </summary>
public class SenceLoding : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]Slider lodingSlider;
    [SerializeField]Text lodingText;
    [SerializeField]GameObject lodingPanel;
    private string levelName;
    //public float addFill=0.2f;
    public void EnterLevelSence(string _levelName)
    {
        //SceneManager.LoadScene(levelName);
        levelName=_levelName;
        StartCoroutine(LoadSence());
    }
    public void Back(int x)
    {
        SceneManager.LoadScene(--x);
    }

    IEnumerator LoadSence()
    {
        yield return null;
        lodingPanel.SetActive(true);
        AsyncOperation _async=SceneManager.LoadSceneAsync(levelName);
        _async.allowSceneActivation=false;
        while(!_async.isDone)
        {
            lodingSlider.value=_async.progress+0.1f;
            lodingText.text=(_async.progress+0.1f)*100+"%";
            if(_async.progress>=0.9f)
            {
                //lodingSlider.value=1;
                lodingText.text="Press Space";
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    //lodingPanel.SetActive(false);
                    _async.allowSceneActivation=true;
                }
 
                

            }
            yield return null;
        }
    }

}
