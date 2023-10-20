using UnityEngine;
using System.Collections;
using System;

// テンプレートはね、むりだ。四則演算ができないから。.

public class VariableParam {
    public int nowValue;
    private int maxValue;

    public int GetNowValue()
    {
        return nowValue;
    }
    public void SetNowValue(int value)
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
    public void AddNowValue(int add)
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
    public int GetMaxValue()
    {
        return maxValue;
    }
    public void SetMaxValue(int value)
    {
        maxValue = value;
    }
    public void AddMaxValue(int add)
    {
        maxValue += add;
        if (maxValue < 0)
        {
            maxValue = 0;
        }
        if (nowValue > maxValue)
        {
            nowValue = maxValue;
        }
    }

    public void SetFullValue()
    {
        nowValue = maxValue;
    }
    public float GetRate(){
        return (float)nowValue / maxValue;
    }
    public bool IsFull()
    {
        return (nowValue == maxValue);
    }
    public bool IsEmpty()
    {
        return (nowValue == 0);
    }
    public void SetCopy(VariableParam sParam)
    {
        maxValue = sParam.GetMaxValue();
        nowValue = sParam.GetNowValue();
    }
}



public class VariableParam_Double
{
    public double nowValue;
    private double maxValue;

    public double GetNowValue()
    {
        return nowValue;
    }
    public void SetNowValue(double value)
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
    public void AddNowValue(double add)
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
    public double GetMaxValue()
    {
        return maxValue;
    }
    public void SetMaxValue(double value)
    {
        maxValue = value;
    }
    public void AddMaxValue(double add)
    {
        maxValue += add;
        if (maxValue < 0)
        {
            maxValue = 0;
        }
        if (nowValue > maxValue)
        {
            nowValue = maxValue;
        }
    }

    public void SetFullValue()
    {
        nowValue = maxValue;
    }
    public double GetRate()
    {
        return (double)nowValue / maxValue;
    }
    public bool IsFull()
    {
        return (nowValue == maxValue);
    }
    public bool IsEmpty()
    {
        return (nowValue == 0);
    }
    public void SetCopy(VariableParam_Double sParam)
    {
        maxValue = sParam.GetMaxValue();
        nowValue = sParam.GetNowValue();
    }
}



#if false
public class VariableParam_Template<T> where T: IComparable
{
    public T nowValue;
    private T maxValue;
    private T minValue;

    public void Initialize(T min, T max)
    {
        maxValue = max;
        minValue = min;
    }
    public void SetNowValue(T value)
    {
        nowValue = value;
        if (nowValue.CompareTo(maxValue) > 0)
        {
            nowValue = maxValue;
        }
        else if (nowValue.CompareTo(minValue) < 0)
        {
            nowValue = minValue;
        }
    }
    public void SetMaxValue(T value)
    {
        maxValue = value;
        // maxValueが、指定した値よりも前にある.
        if (maxValue.CompareTo(nowValue) < 0)
        {
            nowValue = maxValue;
        }
        // maxValueが、指定した値よりも前にある.
        if (maxValue.CompareTo(minValue) < 0)
        {
            maxValue = minValue;
        }
    }
    public void SetMinValue(T value)
    {
        maxValue = value;
    }


    public T GetNowValue()
    {
        return nowValue;
    }
    public void AddNowValue(T add)
    {
        // 四則演算できなかった.
        Debug.Assert(false, "sisoku enzan dekinaiyo VariableParam_Template<T> AddNowValue");
    }
    public T GetMaxValue()
    {
        return maxValue;
    }

    public void AddMaxValue(int add)
    {
        // 四則演算できなかった.
        Debug.Assert(false, "sisoku enzan dekinaiyo VariableParam_Template<T> AddMaxValue");

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
        return (nowValue == maxValue);
    }
    public bool IsEmpty()
    {
        return (nowValue == 0);
    }
    public void SetCopy(VariableParam sParam)
    {
        maxValue = sParam.GetMaxValue();
        nowValue = sParam.GetNowValue();
    }
}
#endif