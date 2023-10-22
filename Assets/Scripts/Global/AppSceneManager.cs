using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class AppSceneManager : MonoBehaviour
{
    public DefineParam.SCENE_ID currentSceneId;

    public DefineParam.SCENARIO_ID currentScenarioId;
    public DefineParam.CHARA_ID currentCharaId;

    UnityAction scenarioPopOutCallbackFunc;

    void Awake(){
        currentScenarioId = DefineParam.SCENARIO_ID.SCENARIO_INVALID;
        currentCharaId = DefineParam.CHARA_ID.CHARA_INVALID;
        scenarioPopOutCallbackFunc = TestFunc;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScenarioId(DefineParam.SCENARIO_ID scenarioId){
        currentScenarioId = scenarioId;
    }

    public DefineParam.SCENARIO_ID GetScenarioId(){
        return currentScenarioId;
    }    

    public void ChangeScene(DefineParam.SCENE_ID sceneId){
        currentSceneId = sceneId;

        FadeManager.ChangeScene(sceneId.ToString());
    }

    public UnityAction GetScenarioPopOutCallbackFunc(){
        return scenarioPopOutCallbackFunc;
    }

    public void TestFunc(){
        Debug.Log("シナリオが終わったよ");
        ChangeScene(DefineParam.SCENE_ID.Lobby);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }    
}
