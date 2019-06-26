using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newUpgrade", menuName = "Scriptables/Upgrade")]
public class Upgrade : FloatValue
{
    new public string name;
    public FloatReference baseValue;
    public int level;
    public float multPerLevel;
    public Sprite sprite;
    public override float value { get => baseValue.Value * (1 + level * multPerLevel); }
    [TextArea]
    public string description;



    int Fibonacci(int steps)
    {
        if (steps <= 2) return steps <= 0 ? 0 : 1;
        return Fibonacci(steps - 1) + Fibonacci(steps - 2);
    }

    public int Cost { get => Fibonacci(level + 2) * 10; }


}
