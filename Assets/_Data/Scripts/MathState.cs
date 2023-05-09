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

    public static double MathCaculation(int a,int b, MathOperation math)
    {
        double value = 0;
        double _a = (double)a, _b = (double)b;

        switch (math)
        {
            case MathOperation.addition:
                value = addition(_a, _b);
                Debug.Log($"{a} + {b} = {value}");
                break;
            case MathOperation.subtraction:
                value = subtraction(_a, _a);
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
    public static double addition(double a, double b)
    {
        return a + b;
    }
    public static double subtraction(double a, double b)
    {
        return a - b;
    }
    public static double multiplication(double a, double b)
    {
        return a * b;
    }
    public static double division(double a, double b)
    {
        return a / b;
    }
}
