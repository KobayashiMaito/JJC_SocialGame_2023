using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Application : MonoBehaviour
{
    private void Awake()
    {
        // シングルトン制御.
        if (S == null)
        {
            // このゲームで初めてインスタンスが出来る場合.
            S = this;

            // Applicationが存在するインスタンスを破棄しないように.
            DontDestroyOnLoad(gameObject);
        }
        else if(S == this)
        {
            // 自分自身だったら大丈夫.
        }
        else
        {
            // このゲームで2回目以降にインスタンスが出来た.
            Destroy(gameObject);
        }

    }

    // ---------------- Static Section ---------------- //

    static private Application _S;
    static private Application S
    {
        get
        {
            if (_S == null)
            {
                return null;
            }
            return _S;
        }
        set
        {
            if (_S != null)
            {
                Debug.LogError("_Sは既に設定されています.");
            }
            _S = value;
        }
    }

    private FixDataManager _fixDataManager;
    static public FixDataManager fixDataManager
    {
        get
        {
            if (S != null)
            {
                if (S._fixDataManager == null)
                {
                    S._fixDataManager = new FixDataManager();
                }
                return S._fixDataManager;
            }
            return null;
        }
    }

    static public bool IsEnableUIControl()
    {
        if (FadeManager.IsFading())
        {
            return false;
        }
        return true;
    }
}
