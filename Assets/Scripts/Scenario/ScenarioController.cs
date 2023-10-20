using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScenarioController : MonoBehaviour
{
    /*
    public int m_currentScenarioScriptRowIndex;

    // Start is called before the first frame update
    void Start()
    {
        m_currentScenarioScriptRowIndex = 0;
        SetRowIndex(m_currentScenarioScriptRowIndex);
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetMouseButtonDown(0)){
            m_currentScenarioScriptRowIndex++;
            SetRowIndex(m_currentScenarioScriptRowIndex);
        }
    }

    void SetRowIndex(int rowIndex){
        ScenarioScriptLoader scenarioScriptLoader = GetComponent<ScenarioScriptLoader>();

        if(!scenarioScriptLoader.IsValidRowIndex(m_currentScenarioScriptRowIndex)){
            UnityAction scenarioPopOutCallbackFunc = SceneChangeManager.Instance.GetScenarioPopOutCallbackFunc();
            if(scenarioPopOutCallbackFunc != null){
                scenarioPopOutCallbackFunc();
            }
            return;
        }

        ScenarioScriptLoader.ScenarioScriptRow scenarioScriptRow = scenarioScriptLoader.GetScenarioScriptRow(m_currentScenarioScriptRowIndex);

        if(scenarioScriptRow.m_commandStr != ""){
            if(scenarioScriptRow.m_commandStr == "@char"){
                string[] lineArray = scenarioScriptRow.m_rawText.Split(',');
                string charaName = "";
                int posX = 0;
                int posY = 0;

                for(int i = 1; i < lineArray.Length; i++){
                    if(lineArray[i][0] == '-'){
                        if(lineArray[i] == "-pos"){
                            string posStr = lineArray[i + 1];

                            if(posStr == "left"){
                                posX = -4;
                            }else if(posStr == "right"){
                                posX = 4;
                            }
                            i += 1;
                        }
                    }else{
                        charaName = lineArray[i];
                    }
                }
                GameObject.Find("NamePlate/Text").GetComponent<Text>().text = charaName;

                int charaId = FixManager.Instance.GetFixCharaManager().GetCharaIdFromCharaName(charaName);
                if(FixManager.Instance.GetFixCharaManager().IsValidCharaId(charaId)){
                    if(GameObject.Find(charaName) == null){
                        string spritePath = FixManager.Instance.GetFixCharaManager().GetFixCharaData(charaId).m_tachiePath;
                        Debug.Log(spritePath);
                        GameObject tachiePrefab = Resources.Load<GameObject>("Prefab/Talk/Tachie");
                        GameObject tachieInstance = Instantiate(tachiePrefab);
                        GameObject tachieParent = GameObject.Find("TalkObject3D");
                        tachieInstance.transform.SetParent(tachieParent.transform);
                        tachieInstance.name = charaName;
                        tachieInstance.transform.position = new Vector3(posX, posY, 0);
                        tachieInstance.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spritePath);;
                    }else{
                        GameObject tachieInstance = GameObject.Find(charaName);
                        tachieInstance.transform.position = new Vector3(posX, posY, 0);
                    }
                }
            }

            if(scenarioScriptRow.m_commandStr == "@actor"){
                string[] lineArray = scenarioScriptRow.m_rawText.Split(',');
                string actorName = "";
                int posX = 0;
                int posY = 0;
                int face_emote_id = 0;

                for(int i = 1; i < lineArray.Length; i++){
                    if(lineArray[i][0] == '-'){
                        if(lineArray[i].Equals("-pos")){
                            string posStr = lineArray[i + 1];

                            if(posStr.Equals("left")){
                                posX = -4;
                            }else if(posStr.Equals("right")){
                                posX = 4;
                            }else if(posStr.Equals("center")){
                                posX = 0;
                            }
                            i += 1;
                        }else if(lineArray[i].Equals("-face_emotion")){
                            string faceEmoteStr = lineArray[i + 1];

                            if(faceEmoteStr.Equals("normal")){
                                face_emote_id = 0;
                            }else if(faceEmoteStr.Equals("joy")){
                                face_emote_id = 1;
                            }else if(faceEmoteStr.Equals("dash")){
                                face_emote_id = 2;
                            }
                            i += 1;
                        }
                    }else{
                        actorName = lineArray[i];
                    }
                }
                GameObject.Find("NamePlate/Text").GetComponent<Text>().text = actorName;
                        Debug.Log("name"+ actorName);

                int actorId = FixManager.Instance.GetFixActorManager().GetActorIdFromActorName(actorName);
                if(FixManager.Instance.GetFixActorManager().IsValidId(actorId)){
                    if(GameObject.Find(actorName) == null){
                        string spritePath = FixManager.Instance.GetFixActorManager().GetFixData(actorId).m_normalPath;
                        Debug.Log("faceEmote"+ face_emote_id.ToString());
                        switch(face_emote_id){
                            case 0:
                            spritePath = FixManager.Instance.GetFixActorManager().GetFixData(actorId).m_normalPath;
                            break;
                            case 1:
                            spritePath = FixManager.Instance.GetFixActorManager().GetFixData(actorId).m_joyPath;
                            break;
                            case 2:
                            spritePath = FixManager.Instance.GetFixActorManager().GetFixData(actorId).m_dashPath;
                            break;
                        }
                        Debug.Log(spritePath);
                        GameObject tachiePrefab = Resources.Load<GameObject>("Prefab/Talk/Tachie");
                        GameObject tachieInstance = Instantiate(tachiePrefab);
                        GameObject tachieParent = GameObject.Find("TalkObject3D");
                        tachieInstance.transform.SetParent(tachieParent.transform);
                        tachieInstance.name = actorName;
                        tachieInstance.transform.position = new Vector3(posX, posY, 0);
                        tachieInstance.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spritePath);
                    }else{
                        GameObject tachieInstance = GameObject.Find(actorName);
                        tachieInstance.transform.position = new Vector3(posX, posY, 0);
                        string spritePath = FixManager.Instance.GetFixActorManager().GetFixData(actorId).m_normalPath;
                        switch(face_emote_id){
                            case 0:
                            spritePath = FixManager.Instance.GetFixActorManager().GetFixData(actorId).m_normalPath;
                            break;
                            case 1:
                            spritePath = FixManager.Instance.GetFixActorManager().GetFixData(actorId).m_joyPath;
                            break;
                            case 2:
                            spritePath = FixManager.Instance.GetFixActorManager().GetFixData(actorId).m_dashPath;
                            break;
                        }
                        tachieInstance.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spritePath);
                    }
                }
            }
            
            if(scenarioScriptRow.m_commandStr == "@hide"){
                string[] lineArray = scenarioScriptRow.m_rawText.Split(',');

                string charaName = "";
                for(int i = 1; i < lineArray.Length; i++){
                    if(lineArray[i][0] == '-'){

                    }else{
                        charaName = lineArray[i];
                    }
                }
                GameObject tachieInstance = GameObject.Find(charaName);
                if(tachieInstance != null){
                    Destroy(tachieInstance);
                }
            }

            m_currentScenarioScriptRowIndex++;
            SetRowIndex(m_currentScenarioScriptRowIndex);
        }else{
            // コマンド無しの、通常のセリフ.
            string talkStr = scenarioScriptRow.m_rawText;

            // 置換.
            talkStr = talkStr.Replace("{USERNAME}",UserDataManager.Instance.GetUserName());
            talkStr = talkStr.Replace("{BR}","\n");

            GameObject.Find("TextPlate/Text").GetComponent<Text>().text = talkStr;
        }
    }    
    */
}
