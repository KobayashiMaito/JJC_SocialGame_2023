using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScenarioScriptLoader : MonoBehaviour
{/*
    void Awake(){
        m_scenarioId = SceneChangeManager.Instance.GetScenarioId();
        LoadScript();
    }

    void Start(){
        
    }

    private void LoadScript(){
        string path = "ScenarioScript/SC" + m_scenarioId.ToString("000");

        TextAsset csvFile;
        m_scenarioScriptRowList = new List<ScenarioScriptRow>();

        csvFile = Resources.Load<TextAsset>(path);
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1)
        {
            ScenarioScriptRow currentRow = new ScenarioScriptRow();
            string line = reader.ReadLine();
            currentRow.m_rawText = line;

            string[] lineArray = line.Split(',');
            char rawTextFirstChar = lineArray[0][0];
            if(rawTextFirstChar == '@'){
                currentRow.m_commandStr = lineArray[0];
            }else{
                currentRow.m_commandStr = "";
            }

            m_scenarioScriptRowList.Add(currentRow);
        }
    }

    void PrintDebugLog(ScenarioScriptRow row){
        Debug.Log(row.m_rawText);
    }

    public ScenarioScriptRow GetScenarioScriptRow(int rowIndex){
        return m_scenarioScriptRowList[rowIndex];
    }

    public bool IsValidRowIndex(int rowIndex){
        if(m_scenarioScriptRowList == null){
            return false;
        }

        if(rowIndex < 0 || rowIndex >= m_scenarioScriptRowList.Count){
            return false;
        }
        return true;
    }

    public struct ScenarioScriptRow{
        public string m_commandStr;
        public string m_rawText; // 生テキスト.
    }

    public int m_scenarioId;
    List<ScenarioScriptRow> m_scenarioScriptRowList = null;*/
}
