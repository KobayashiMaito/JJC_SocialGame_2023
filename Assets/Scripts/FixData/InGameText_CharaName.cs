using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InGameText_CharaName
{
    public void Load()
    {
        string path = "Fix/InGameText.xlsm_CharaName";

        TextAsset csvFile;
        m_fixActorDataList = new List<FixActorData>();

        {
            csvFile = Resources.Load<TextAsset>(path);
            StringReader reader = new StringReader(csvFile.text);

            int id = 0;

            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                string[] lineArray = line.Split(',');

                FixActorData currentRow = new FixActorData();
                currentRow.m_id = id;
                currentRow.m_name = lineArray[0];

                m_fixActorDataList.Add(currentRow);

                id++;
            }
        }

        path = "Fix/会話アクターリソース";
        int FixDataNum = GetFixNum();
        {
            csvFile = Resources.Load<TextAsset>(path);
            StringReader reader = new StringReader(csvFile.text);

            int id = 0;

            while (reader.Peek() != -1)
            {
                if (id >= FixDataNum)
                {
                    Debug.Assert(false, "会話アクターリソース.csvの行が多すぎる");
                    break;
                }

                string line = reader.ReadLine();
                string[] lineArray = line.Split(',');

                m_fixActorDataList[id].m_normalPath = lineArray[0];
                m_fixActorDataList[id].m_joyPath = lineArray[1];
                m_fixActorDataList[id].m_dashPath = lineArray[2];

                id++;
            }

            if (id < FixDataNum)
            {
                Debug.Assert(false, "会話アクターリソース.csvの行が少なすぎる");
            }
        }
    }

    public FixActorData GetFixData(int actorId)
    {
        if (actorId >= GetFixNum())
        {
            return null;
        }
        return m_fixActorDataList[actorId];
    }

    public int GetFixNum()
    {
        return m_fixActorDataList.Count;
    }

    public int GetActorIdFromActorName(string actorName)
    {
        for (int i = 0; i < GetFixNum(); i++)
        {
            FixActorData data = GetFixData(i);
            if (data.m_name.Equals(actorName))
            {
                return i;
            }
        }
        return INVALID_ID;
    }

    public bool IsValidId(int Id)
    {
        return 0 <= Id && Id < GetFixNum();
    }

    public void DB_Disp()
    {
        for (int i = 0; i < GetFixNum(); i++)
        {
            FixActorData data = GetFixData(i);
            Debug.Log(data.m_id + "," + data.m_name + "," + data.m_normalPath + "," + data.m_joyPath + "," + data.m_dashPath);
        }
    }

    public class CharaName
    {
        public int m_id;
        public string m_name;
    }

    List<CharaName> m_fixList;
    public const int INVALID_ID = 0;

}
