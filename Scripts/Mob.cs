using UnityEngine;

[CreateAssetMenu(fileName = "Ally", menuName = "Scriptable Objects/Mob")]
public class Mob : ScriptableObject
{
    [SerializeField]
    Stat stat;
    [SerializeField]
    ClassType type;
    [SerializeField]
    bool moved;
    [SerializeField]
    Skill[] skills;
    public Stat Stat { get => stat; set => stat = value; }
    public ClassType Type { get => type; set => type = value; }
    public bool Moved { get => moved; set => moved = value; }
    public Skill[] Skills { get => skills; set => skills = value; }
}
