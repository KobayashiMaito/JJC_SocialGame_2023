using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallbackTimer : MonoBehaviour {

    private void Awake()
    {
        // インスタンスを生成
        if (m_unityEvent == null)
            m_unityEvent = new UnityEvent();

        // ↓これはイベント実行テスト用コード.
        // m_unityEvent.AddListener(TestRun);

        m_Second = new VariableParamFloat();
        m_Second.SetNowValue(m_fCurrentTime);
        m_Second.SetMaxValue(m_fMaxTime);
    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        if ( m_isPause )
        {
            return; // ポーズ中は一切タイマを動かさない.
        }
        if ( m_Second.IsFull() )
        {
            m_isClearCallbacks = false;
            m_unityEvent.Invoke();
            if (!m_isClearCallbacks)
            {
                // Invokeの中でClearしていたら、ここではClearしなくていい.
                ClearAllCallback(); // イベントが残っていると、無限にイベント実行してる.
            }
        }
        else
        {
            m_Second.AddNowValue(Time.deltaTime); // 加算タイマ.
        }

#if DEBUG_KUWABARA
        m_fMaxTime = m_Second.GetMaxValue();
        m_fCurrentTime = m_Second.GetNowValue();
#endif
    }

    // 今の進行状況. 0から始まって、1で終わり.
    public float GetRate()
    {
        return m_Second.GetRate();
    }

    // タイマー終わり？.
    public bool IsTimerEnd()
    {
        return m_Second.IsFull();
    }

    // 呼び出す関数を全部消す.
    public void ClearAllCallback()
    {
        m_unityEvent.RemoveAllListeners();
        m_isClearCallbacks = true;
    }

    // タイマーが終わったら呼び出す関数を指定.
    public void AddCallback(UnityAction pFunc)
    {
        m_unityEvent.AddListener(pFunc);
    }

    // 何秒のタイマーか設定する. タイマーが終わったら何をするかも設定する.
    public void ResetTimer(float second, UnityAction pFunc)
    {
        m_Second.SetNowValue(0);
        m_Second.SetMaxValue(second);
        ClearAllCallback();
        AddCallback(pFunc);
    }

    void TestRun()
    {
        Debug.Log("タイマーが実行したよ");
    }

    public void EnablePause(bool bEnable){ // ポーズ（true）したらタイマ更新が止まる. ポーズ解除（false）で動き出す.
        m_isPause = bEnable;
    }

    public UnityEvent m_unityEvent;
    // ↓Editorから指定するためだけの変数.
    public float m_fCurrentTime;
    public float m_fMaxTime;
    public VariableParamFloat m_Second;
    bool m_isClearCallbacks; // ResetTimer -> ClearCallbackが完了していたら、２回目以降はclearしない.
    bool m_isPause; // 一時停止中.
}
