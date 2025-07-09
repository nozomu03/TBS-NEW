using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Objects/Skill")]
public abstract class Skill : ScriptableObject
{
    [SerializeField]
    Category category;
    [SerializeField]
    ClassType[] can_learn;
    [SerializeField]
    string name;
    [SerializeField]
    int atk;
    [SerializeField]
    int range;
    [SerializeField]
    float acc;
    [SerializeField]
    int distance;
    [SerializeField]
    int duration;

    public Category Category { get => category; set => category = value; }
    public int Atk { get => atk; set => atk = value; }
    public int Range { get => range; set => range = value; }
    public float Acc { get => acc; set => acc = value; }
    public string Name { get => name; set => name = value; }
    public ClassType[] Can_learn { get => can_learn; set => can_learn = value; }
    public int Distance { get => distance; set => distance = value; }
    public int Duration { get => duration; set => duration = value; }

    public abstract ResultSet ExecuteSkill(List<CharCon> char_con);
}


[CreateAssetMenu(menuName = "Scriptable Objects/Skill/Support")]
public class Support : Skill
{
    public override ResultSet ExecuteSkill(List<CharCon> char_con)
    {
        switch (Category)
        {
            case Category.Acc_Buff:
                foreach(CharCon c in char_con)
                    c.Stat.Acc += Atk;
                break;
            case Category.Acc_Debuff:
                foreach (CharCon c in char_con)
                    c.Stat.Acc -= Atk;
                break;
            case Category.Def_Buff:
                foreach (CharCon c in char_con)
                    c.Stat.Def += Atk;
                break;
            case Category.Def_Debuff:
                foreach (CharCon c in char_con)
                    c.Stat.Def -= Atk;
                break;
            case Category.Use_Buff:
                foreach (CharCon c in char_con)
                    for(int i = 0; i < c.Mob.Skills.Length; i++)
                    {
                        c.Mob.Skills[i].use += Atk;
                    }
                break;
            case Category.Use_Debuff:
                foreach (CharCon c in char_con)
                    for (int i = 0; i < c.Mob.Skills.Length; i++)
                    {
                        c.Mob.Skills[i].use -= Atk;
                    }
                break;
        }
        return new ResultSet(Atk, Duration);
    }
}

// Èú ½ºÅ³
[CreateAssetMenu(menuName = "Scriptable Objects/Skill/Direct")]
public class Direct : Skill
{
    
    public override ResultSet ExecuteSkill(List<CharCon> char_con)
    {
        foreach(CharCon c in char_con)
        {
            c.Stat.Hp -= Atk;
        }
        return new ResultSet(Atk, 0);
    }
}

public class ResultSet
{
    int change_value;
    int duration;

    public ResultSet(int change_value, int duration)
    {
        this.change_value = change_value;
        this.duration = duration;
    }
}
