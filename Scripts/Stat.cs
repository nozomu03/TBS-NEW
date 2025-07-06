using System;
using UnityEngine;
[CreateAssetMenu(fileName = "Ally", menuName = "Scriptable Objects/Stat")]

public class Stat:ScriptableObject
{
    [SerializeField]
    float hp;
    [SerializeField]
    float atk;
    [SerializeField]
    float def;
    [SerializeField]
    float acc;
    [SerializeField]
    int mov;
    [SerializeField]
    bool hill;

    public float Hp { get => hp; set => hp = value; }
    public float Atk { get => atk; set => atk = value; }
    public float Def { get => def; set => def = value; }
    public float Acc { get => acc; set => acc = value; }
    public int Mov { get => mov; set => mov = value; }
    public bool Hill { get => hill; set => hill = value; }
}
