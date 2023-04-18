using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathState
{
    enum MathOperation
    {
        // 0 1 2 3
        // cong tru nhan chia
        addition,
        subtraction,
        multiplication,
        division,
    }
    static MathOperation Math { get; set; }

    public static int MathCaculation(int a,int b)
    {
        int value = 0;
        switch (Math)
        {
            case MathOperation.addition:
                value = addition(a, b);
                break;
            case MathOperation.subtraction:
                value = subtraction(a, b);
                break;
            case MathOperation.multiplication:
                value = multiplication(a, b);
                break;
            case MathOperation.division:
                value = division(a, b);
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
