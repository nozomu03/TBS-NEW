using UnityEngine;

[CreateAssetMenu(fileName = "Ally", menuName = "Scriptable Objects/Mob")]
public class Mob : ScriptableObject
{
    [SerializeField]
    Stat stat;
    [SerializeField]
    ClassType type;
    [SerializeField]
    bool selected;
    public Stat Stat { get => stat; set => stat = value; }
    public ClassType Type { get => type; set => type = value; }
    public bool Selected { get => selected; set => selected = value; }
}
