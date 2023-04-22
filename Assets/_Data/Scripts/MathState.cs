using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathState
{
    public enum MathOperation
    {
        // 0 1 2 3
        // cong tru nhan chia
        addition,
        subtraction,
        multiplication,
        division,
    }
    [SerializeField]
    public static MathOperation Math;

    public static int MathCaculation(int a,int b, MathOperation math)
    {
        int value = 0;
        switch (math)
        {
            case MathOperation.addition:
                value = addition(a, b);
                Debug.Log($"{a} + {b} = {value}");
                break;
            case MathOperation.subtraction:
                value = subtraction(a, b);
                Debug.Log($"{a} - {b} = {value}");
                break;
            case MathOperation.multiplication:
                value = multiplication(a, b);
                Debug.Log($"{a} * {b} = {value}");
                break;
            case MathOperation.division:
                value = division(a, b);
                Debug.Log($"{a} / {b} = {value}");
                break;
            default:
                break;
        }
        return value;
    }
    public static int addition(int a, int b)
    {
        return a + b;
    }
    public static int subtraction(int a, int b)
    {
        return a - b;
    }
    public static int multiplication(int a, int b)
    {
        return a * b;
    }
    public static int division(int a, int b)
    {
        return a / b;
    }
}
