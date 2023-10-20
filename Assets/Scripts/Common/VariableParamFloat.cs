using UnityEngine;
using System.Collections;

public class VariableParamFloat
{
    public float nowValue;
    private float maxValue;

    public float GetNowValue()
    {
        return nowValue;
    }
    public void SetNowValue(float value)
    {
        nowValue = value;
        if (nowValue > maxValue)
        {
            nowValue = maxValue;
        }
        else if (nowValue < 0)
        {
            nowValue = 0;
        }
    }
    public void AddNowValue(float add)
    {
        nowValue += add;
        if (nowValue > maxValue)
        {
            nowValue = maxValue;
        }
        else if (nowValue < 0)
        {
            nowValue = 0;
        }
    }
    public float GetMaxValue()
    {
        return maxValue;
    }
    public void SetMaxValue(float value)
    {
        maxValue = value;
    }

    public void SetFullValue()
    {
        nowValue = maxValue;
    }
    public float GetRate()
    {
        return (float)nowValue / maxValue;
    }
    public bool IsFull()
    {
        return nowValue.Equals(maxValue);
    }
    public bool IsEmpty()
    {
        return nowValue.Equals(0);
    }
}
