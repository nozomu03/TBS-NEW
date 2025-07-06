using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Scriptable Objects/Skill")]
public class Skill : ScriptableObject
{
    [SerializeField]
    Category category;
    [SerializeField]
    ClassType[] can_learn;
    [SerializeField]
    string name;
    [SerializeField]
    int use_count;
    [SerializeField]
    float atk;
    [SerializeField]
    int range;
    [SerializeField]
    float acc;
    [SerializeField]
    int distance;

    public Category Category { get => category; set => category = value; }
    public float Atk { get => atk; set => atk = value; }
    public int Range { get => range; set => range = value; }
    public float Acc { get => acc; set => acc = value; }
    public int Use_count { get => use_count; set => use_count = value; }
    public string Name { get => name; set => name = value; }
    public ClassType[] Can_learn { get => can_learn; set => can_learn = value; }
    public int Distance { get => distance; set => distance = value; }
}
