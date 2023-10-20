using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixDataManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        fixData_charaFixData.Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    FixData_CharaFixData fixData_charaFixData;
    InGameText_CharaName inGameText_charaName;
}
