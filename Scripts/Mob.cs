using UnityEngine;

[CreateAssetMenu(fileName = "Ally", menuName = "Scriptable Objects/Mob")]
public class Mob : ScriptableObject
{
    [SerializeField]
    StatTemplate stat_temp;
    [SerializeField]
    ClassType type;
    [SerializeField]
    bool moved;
    [SerializeField]
    SkillPair[] skills;
    public StatTemplate Stat { get => stat_temp; set => stat_temp = value; }
    public ClassType Type { get => type; set => type = value; }
    public bool Moved { get => moved; set => moved = value; }
    public SkillPair[] Skills { get => skills; set => skills = value; }
}

[System.Serializable]
public struct SkillPair
{
    public Skill skill;
    public int use;
}