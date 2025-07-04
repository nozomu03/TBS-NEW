using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharCon : MonoBehaviour
{    
    [SerializeField]
    Material mat;
    [SerializeField]
    Mob mob;
    [SerializeField]
    Stat stat;
    [SerializeField]
    MapMob map;
    [SerializeField]
    MeshRenderer render;    
    public Dictionary<int, List<Tile>> can_move;
    List<Tile> temp_move;
    public Material hilight_mat;
    public List<Material> org_mat;

    public Mob Mob { get => mob; set => mob = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stat = Mob.Stat;
        Init();
        //Hilight();
        //string result = string.Join("\n",
        //    can_move.Select(group =>
        //        $"Key {group.Key} ¡æ " +
        //        string.Join(", ", group.Value.Select(tile =>
        //            $"({tile.X},{tile.Z},h:{tile.Height}:{tile.transform.name})"))));
        //Debug.Log(result);

    }

    public void Init()
    {
        can_move = new Dictionary<int, List<Tile>>();
        temp_move = new List<Tile>();
        temp_move.Add(map.Map[(int)transform.position.z, (int)transform.position.x]);
        can_move.Add(0, temp_move);
        for (int i = 1; i <= stat.Mov; i++)
        {
            MoveRange(i, can_move[i - 1]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if(Physics.Raycast(ray, out RaycastHit hit))
        //    {
        //        if (hit.transform == transform)
        //        {
        //            Debug.Log("Å¬¸¯");
        //            Hilight();
        //        }
        //        if(hit.transform.tag == "CanTile")
        //        {
        //            transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z);
        //        }
        //    }
        //}
    }

    public void MoveRange(int distance, List<Tile> tiles)
    {
        temp_move = new List<Tile>();
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].X + 1 < map.Map_x)
            {
                temp_move.Add(map.Map[tiles[i].Z, tiles[i].X + 1]);
                render = map.Map[tiles[i].Z, tiles[i].X + 1].GetComponent<MeshRenderer>();
            }
            if (tiles[i].X - 1 >= 0)
            {
                temp_move.Add(map.Map[tiles[i].Z, tiles[i].X - 1]);
                render = map.Map[tiles[i].Z, tiles[i].X - 1].GetComponent<MeshRenderer>();

            }
            if (tiles[i].Z + 1 < map.Map_y)
            {
                temp_move.Add(map.Map[tiles[i].Z + 1, tiles[i].X]);
                render = map.Map[tiles[i].Z + 1, tiles[i].X].GetComponent<MeshRenderer>();

            }
            if (tiles[i].Z - 1 >= 0)
            {
                temp_move.Add(map.Map[tiles[i].Z - 1, tiles[i].X]);
                render = map.Map[tiles[i].Z - 1, tiles[i].X].GetComponent<MeshRenderer>();
            }

        }
        can_move[distance] = temp_move;
        Debug.Log(string.Join("\n", temp_move.Select(tile => $"({tile.X},{tile.Z},h:{tile.Height}:{transform.name})")));

    }

    public void Hilight()
    {
        Material org;
        Color tmp;
        for(int i = 0; i < can_move.Count; i++)
        {
            for(int j = 0; j < can_move[i].Count; j++)
            {
                render = can_move[i][j].GetComponent<MeshRenderer>();
                org = render.materials[0];
                tmp = org.color;
                tmp.a = 0f;
                org.color = tmp;
               
                tmp = render.materials[1].color;
                tmp.a = 1f;
                render.materials[1].color = tmp;

                render.materials[0] = render.materials[1];
                render.materials[1] = org;
                can_move[i][j].tag = "CanTile";
            }
        }
    }
    public void UnHilight()
    {
        Material org;
        Color tmp;

        for (int i = 0; i < can_move.Count; i++)
        {
            for (int j = 0; j < can_move[i].Count; j++)
            {
                render = can_move[i][j].GetComponent<MeshRenderer>();
                org = render.materials[0];
                tmp = org.color;
                tmp.a = 1f;
                org.color = tmp;

                tmp = render.materials[1].color;
                tmp.a = 0f;
                render.materials[1].color = tmp;
                render.materials[0] = render.materials[1];
                render.materials[1] = org;

                can_move[i][j].tag = "Tile";
            }
        }

    }
}
