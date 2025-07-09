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
    public Dictionary<int, List<Tile>> _tmp_can_move;    
    List<Tile> temp_move;
    public Material hilight_mat;
    public List<Material> org_mat;
    public bool now_skill;
    HashSet<Tile> alltile;      
    public Mob Mob { get => mob; set => mob = value; }
    public Stat Stat { get => stat; set => stat = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Stat = new Stat(Mob.Stat);
        can_move = Init(Stat.Mov, transform.position);
        //Hilight();
        //string result = string.Join("\n",
        //    can_move.Select(group =>
        //        $"Key {group.Key} ¡æ " +
        //        string.Join(", ", group.Value.Select(tile =>
        //            $"({tile.X},{tile.Z},h:{tile.Height}:{tile.transform.name})"))));
        //Debug.Log(result);

    }

    public Dictionary<int,  List<Tile>> Init(int distance, Vector3 position)
    {
        _tmp_can_move = new Dictionary<int, List<Tile>>();
        temp_move = new List<Tile>();
        temp_move.Add(map.Map[(int)position.z, (int)position.x]);
        //Debug.Log(transform.name + ":" + position.z + ":" + position.x);
        _tmp_can_move.Add(0, temp_move);
        for (int i = 1; i <= distance; i++)
        {
            MoveRange(i, _tmp_can_move[i - 1]);
        }
       // Debug.Log(_tmp_can_move.Values.Sum(list => list.Count));

        return _tmp_can_move;

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
        alltile = new HashSet<Tile>();

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

        for (int i = 0; i < _tmp_can_move.Count; i++)
        {
            for (int j = 0; j < _tmp_can_move[i].Count; j++)
            {
                alltile.Add(_tmp_can_move[i][j]);
            }
        }
        temp_move.RemoveAll(tile => alltile.Contains(tile));
        temp_move = temp_move.Distinct().ToList();
        //Debug.Log("dis_node: " + temp_move.Count);
        _tmp_can_move[distance] = temp_move;//.Where(tile => !alltile.Contains(tile)).ToList();
        //Debug.Log(string.Join("\n", temp_move.Select(tile => $"({tile.X},{tile.Z},h:{tile.Height}:{transform.name})")));

    }
    bool HasDuplicate(List<Tile> list1, List<Tile> list2)
    {
        return list1.Any(t1 => list2.Any(t2 => t1.X == t2.X && t1.Z == t2.Z));
    }


    public void Hilight(Dictionary<int, List<Tile>> mov, string tag, int mat = 1)
    {
        Material org;
        Color tmp;
        for(int i = 0; i < mov.Count; i++)
        {
            for(int j = 0; j < mov[i].Count; j++)
            {                
                render = mov[i][j].GetComponent<MeshRenderer>();
                for(int _k = 0; _k< render.materials.Length; _k++)
                {
                    if (render.materials[_k].color.a == 1.0f)
                    {
                        mov[i][j].GetComponent<Tile>().Now_mat = _k;

                        break;

                    }

                }
                for (int k = 0; k < render.materials.Length; k++)
                {
                    if (k == mat)
                    {
                        tmp = render.materials[k].color;
                        tmp.a = 1f;
                        render.materials[k].color = tmp;
                    }
                    else
                    {
                        org = render.materials[k];
                        tmp = org.color;
                        tmp.a = 0f;
                        org.color = tmp;


                        if (tag.Equals("") == false)
                        {
                            mov[i][j].tag = tag;
                        }
                    }
                }
            }
        }
    }
    public void UnHilight(Dictionary<int, List<Tile>> range, int mat = 0, bool reset = true)
    {
        Material org;
        Color tmp;

        for (int i = 0; i < range.Count; i++)
        {
            for (int j = 0; j < range[i].Count; j++)
            {
                render = range[i][j].GetComponent<MeshRenderer>();
                if (!reset)
                {
                    mat = render.GetComponent<Tile>().Now_mat;
                    for (int k = 0; k < render.materials.Length; k++)
                    {
                        if (k == mat)
                        {
                            org = render.materials[k];
                            tmp = org.color;
                            tmp.a = 1f;
                            org.color = tmp;
                        }
                        else
                        {
                            tmp = render.materials[k].color;
                            tmp.a = 0f;
                            render.materials[k].color = tmp;
                        }
                    }
                }
                //render.materials[0] = render.materials[mat];
                //render.materials[mat] = org;
                else 
                {
                    Reset();
                }
                    //{
                    //    range[i][j].tag = "Tile";
                    //}                
            }
        }


    }
    public void Reset()
    {
        Color tmp;
        foreach (Tile t in map.Map)
        {
            render = t.GetComponent<MeshRenderer>();
            for(int i = 0; i < render.materials.Length; i++) 
            {
                tmp = render.materials[i].color;
                if (i > 0)
                {                    
                    tmp.a = 0f;                    
                }
                else
                {
                    tmp.a = 1.0f;                    
                }
                render.materials[i].color = tmp;
            }
            t.transform.tag = "Tile";
        }

    }


}
