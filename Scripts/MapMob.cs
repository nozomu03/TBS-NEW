using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapMob", menuName = "Scriptable Objects/MapMob")]
public class MapMob : ScriptableObject
{
    [SerializeField]
    private List<GameObject> ally;
    [SerializeField]
    private List<GameObject> enermy;
    [SerializeField]
    private int enermy_count;
    [SerializeField]
    private int lv;
    [SerializeField]
    private GameObject[] variable_mob;
    [SerializeField]
    private Tile[,] map;
    [SerializeField]
    private int map_x;
    [SerializeField]
    private int map_y;


    public List<GameObject> Ally { get => ally; set => ally = value; }
    public List<GameObject> Enermy { get => enermy; set => enermy = value; }
    public int Enermy_count { get => enermy_count; set => enermy_count = value; }
    public int Lv { get => lv; set => lv = value; }
    public GameObject[] Variable_mob { get => variable_mob; set => variable_mob = value; }
    public Tile[,] Map { get => map; set => map = value; }
    public int Map_x { get => map_x; set => map_x = value; }
    public int Map_y { get => map_y; set => map_y = value; }

    public void EnermyGen()
    {
        int _tmp = 0;
        for(int i = 0; i < enermy_count; i ++)
        {
            _tmp = Random.Range(0, variable_mob.Length);
            enermy.Add(variable_mob[_tmp]);
        }
    }

    public void EnermyReset()
    {
        enermy = new List<GameObject>();
    }
}
