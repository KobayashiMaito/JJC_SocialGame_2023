using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefineParam : MonoBehaviour
{
    public enum SCENARIO_ID{
        SCENARIO_INVALID=0,
        SCENARIO001,
        SCENARIO002,
        SCENARIO003,
        SCENARIO004,
        SCENARIO005,
        SCENARIO006,
        SCENARIO007,
        SCENARIO008,
        SCENARIO009,
        SCENARIO010,
        SCENARIO011,
        SCENARIO012,
        SCENARIO013,
        SCENARIO014,
        SCENARIO015,

        SCENARIO300=300,
    }

    public enum CHARA_ID{
        CHARA_INVALID = 0,
        CHARA001_KUJO,
        CHARA002_HAKKAISAN,
        CHARA002_SUZUKI,
        CHARA002_CHAGAMA,
        CHARA002_JOANDMYO,
        CHARA002_TIKARA,
        CHARA002_SAKEPPURIN,
        CHARA002_SAKURAMORI,
        CHARA002_KURENAI,
        CHARA002_DRAGON,
        CHARA002_KASUGAYAMA_SOLDIER,
        CHARA002_LILYAN,
        CHARA002_KOJIMA,
        CHARA002_FIORA,
        CHARA002_KEDAMA,

        CHARA300_UESUGI_MACKENYU = 300,
    }

    public enum SCENE_ID{
        Lobby,
        Talk
    }    
}
