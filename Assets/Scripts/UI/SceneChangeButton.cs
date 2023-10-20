using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour
{
    public enum SCENE_NAME
    {
        Lobby,
        Talk
    };

    public SCENE_NAME changeSceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (Application.IsEnableUIControl())
        {
            FadeManager.ChangeScene(changeSceneName.ToString());

            //FadeManager.ChangeState(TestFunc);
        }
    }

    public void TestFunc()
    {
        Debug.Log("ChangeState");
    }
}
