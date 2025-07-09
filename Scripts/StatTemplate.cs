using System;
using UnityEngine;
[CreateAssetMenu(fileName = "Ally", menuName = "Scriptable Objects/Stat")]

public class StatTemplate:ScriptableObject
{
    [SerializeField]
    float hp;
    [SerializeField]
    int atk;
    [SerializeField]
    float def;
    [SerializeField]
    float acc;
    [SerializeField]
    int mov;
    [SerializeField]
    int distance;
    [SerializeField]
    int ap;

    public float Hp { get => hp; set => hp = value; }
    public int Atk { get => atk; set => atk = value; }
    public float Def { get => def; set => def = value; }
    public float Acc { get => acc; set => acc = value; }
    public int Mov { get => mov; set => mov = value; }
    public int Distance { get => distance; set => distance = value; }
    public int Ap { get => ap; set => ap = value; }
}
