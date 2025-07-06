using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
public class MapGen : MonoBehaviour
{
    public List<int> random_rate;
    public MapMob mobdata;
    
    public Tile cube;
    
    public List<GameObject> now_ally;
    public List<GameObject> now_enermy;

    public Camera camera;

    private CharCon char_con;
    private ActionControl action_con;
    private Transform now_hit;
    public TextMeshProUGUI text;

    public GameObject panel;

    Ray ray2;

    bool is_block;

    Color _tmp;

    Tile tmp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        mobdata.Map = new Tile[mobdata.Map_y, mobdata.Map_x];
        mobdata.EnermyReset();
        now_ally = new List<GameObject>();
        now_enermy = new List<GameObject>();
    }    
    void Start()
    {
        //map = new int[map_y, map_x];
        MapInit();
        mobdata.EnermyGen();
        MobInit();
        SetAlpha();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (char_con == null)
                {
                    if (hit.transform.tag == "Player")
                    {
                        Debug.Log("클릭");
                        char_con = hit.transform.GetComponent<CharCon>();
                        text.text = hit.transform.name;
                        text.gameObject.SetActive(true);
                        panel.SetActive(true);
                        action_con = hit.transform.GetComponent<ActionControl>();
                        action_con.panel = panel;
                        action_con.enabled = true;

                        char_con.Hilight(char_con.can_move, "CanTile", 1);
                    }
                }
                else
                {
                    if (hit.transform.tag == "CanTile")
                    {
                        char_con.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z);
                        char_con.UnHilight(char_con.can_move);
                        char_con.Init(char_con.Mob.Stat.Mov, char_con.transform.position);
                        char_con.Mob.Moved = true;
                        char_con.can_move = new Dictionary<int, List<Tile>>();
                        //char_con = null;
                        action_con.enabled = true;

                        SetAlpha();
                    }
                    else if(hit.transform.tag == "Player")
                    {
                        Debug.Log("변경");
                        if (action_con.distance_dic != null && action_con.distance_dic.Count > 0)
                        {
                            char_con.UnHilight(action_con.distance_dic, 2);
                        }
                        char_con.UnHilight(char_con.can_move);
                        action_con.enabled = false;
                        char_con = hit.transform.GetComponent<CharCon>();
                        action_con = hit.transform.GetComponent<ActionControl>();
                        text.text = hit.transform.name;
                        action_con.panel = panel;
                        action_con.enabled = true;
                        char_con.Hilight(char_con.can_move, "CanTile", 1);
                    }
                    else if(hit.transform.tag == "Distance")
                    {
                        Debug.Log("스킬 사용");
                        text.gameObject.SetActive(false);
                        Debug.Log(action_con.transform.name);
                        char_con.UnHilight(action_con.distance_dic, 2);
                        action_con.Reset();
                        action_con.enabled = false;
                        panel.SetActive(false);
                        action_con = null;
                        char_con = null;

                    }
                    else
                    {
                        text.gameObject.SetActive(false);
                        char_con.UnHilight(char_con.can_move);
                        action_con.enabled = false;
                        panel.SetActive(false);
                        action_con = null;
                        char_con = null;
                    }
                }
            }
        }
        if (char_con != null)
        {
            if (char_con.now_skill)
            {
                ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray2, out RaycastHit hit2))
                {                   
                    if (hit2.transform.tag == "Distance")
                    {
                           now_hit = hit2.transform;
                            Debug.Log(Input.mousePosition);
                            //char_con.UnHilight(action_con.range_dic, 2);
                            char_con.UnHilight(action_con.range_dic, 1);
                            action_con.range_dic = char_con.Init(action_con.skill.Range, hit2.transform.position);
                            char_con.Hilight(action_con.range_dic, "Tile", 1);
                        
                    }
                }
            }
        }
    }

    void SetAlpha()
    {
        MeshRenderer render;
        for (int i = 0; i < now_ally.Count; i++)
        {
            is_block = Physics.Linecast(camera.transform.position, now_ally[i].transform.position, out RaycastHit hit);
            if (is_block && hit.transform != now_ally[i].transform)
            {
                render = hit.transform.GetComponent<MeshRenderer>();
                _tmp = render.material.color;
                _tmp.a = .2f;
                render.material.color = _tmp;
                render.material = render.materials[0];
                Debug.Log(hit.transform.name);                

            }
        }

    }

    void MapInit()
    {
        int _tmp = 0;
        for (int i = 0; i < mobdata.Map_y; i++)
        {
            for (int j = 0; j < mobdata.Map_x; j++)
            {
                mobdata.Map[i, j] = Instantiate(cube, new Vector3(j, 0, i), Quaternion.Euler(0, 0, 0));
            }

        }
        for (int i = 0; i < mobdata.Map_y; i++)
        {
            for (int j = 0; j < mobdata.Map_x; j++)
            {
                if (j - 1 >= 0)
                {
                    _tmp = PercentRandom(random_rate);
                    //mobdata.Map[i, j - 1] = Instantiate(cube, new Vector3(j - 1, _tmp, i), Quaternion.Euler(0, 0, 0));
                    mobdata.Map[i, j - 1].X = j - 1;
                    mobdata.Map[i, j - 1].Z = i;
                    mobdata.Map[i, j - 1].Height = _tmp;
                    mobdata.Map[i, j - 1].transform.position = new Vector3(j -1 , _tmp, i);
                    mobdata.Map[i, j - 1].name = mobdata.Map[i, j - 1].Height + "";


                }
                if (j + 1 < mobdata.Map_x)
                {
                    _tmp = PercentRandom(random_rate);
                    //mobdata.Map[i, j + 1] = Instantiate(cube, new Vector3(j + 1, _tmp, i), Quaternion.Euler(0, 0, 0));
                    mobdata.Map[i, j + 1].Height = _tmp;
                    mobdata.Map[i, j + 1].X = j + 1;
                    mobdata.Map[i, j + 1].Z = i;
                    mobdata.Map[i, j + 1].transform.position = new Vector3(j + 1, _tmp, i);
                    mobdata.Map[i, j + 1].name = mobdata.Map[i, j + 1].Height + "";

                }
                if (i - 1 >= 0)
                {
                    _tmp = PercentRandom(random_rate);
                   // mobdata.Map[i -1 , j] = Instantiate(cube, new Vector3(j, _tmp, i - 1), Quaternion.Euler(0, 0, 0));
                    mobdata.Map[i - 1, j].Height = _tmp;
                    mobdata.Map[i - 1, j].X = j;
                    mobdata.Map[i - 1, j].Z = i - 1;
                    mobdata.Map[i - 1, j].transform.position = new Vector3(j, _tmp, i - 1);
                    mobdata.Map[i - 1, j].name = mobdata.Map[i - 1, j].Height + "";

                }
                if (i + 1 < mobdata.Map_y)
                {
                    _tmp = PercentRandom(random_rate);
                    //mobdata.Map[i + 1, j] = Instantiate(cube, new Vector3(j, _tmp, i + 1), Quaternion.Euler(0, 0, 0));
                    mobdata.Map[i + 1, j].Height = _tmp;
                    mobdata.Map[i + 1, j].X = j;
                    mobdata.Map[i + 1, j].Z = i + 1;
                    mobdata.Map[i + 1, j].transform.position = new Vector3(j, _tmp, i + 1);
                    mobdata.Map[i + 1, j].name = mobdata.Map[i + 1, j].Height + "";

                }
                //Debug.Log(map[i, j]);
            }
        }
        for (int i = 0; i < mobdata.Map_y; i++)
        {
            for (int j = 0; j < mobdata.Map_x; j++)
            {
                MakeChild(mobdata.Map[i, j].transform, mobdata.Map[i, j].Height);
            }
            //}
            //for(int i = 0; i < map_y; i++)
            //{
            //    for(int j = 0; j < map_x; j++)
            //    {

            //    }
            //}
        }
    }

    void MakeChild(Transform parent_node, int y)
    {
        Tile _tmp;
        if (y > 0)
        {
            for (int i = y - 1; i > -1; i--)
            {
                _tmp = Instantiate(cube);
                _tmp.transform.position = new Vector3(parent_node.position.x, parent_node.position.y - 1, parent_node.position.z);
                _tmp.X = (int)parent_node.position.x;
                _tmp.Z = (int)parent_node.position.z;
                _tmp.Height = i;
                parent_node = _tmp.transform;
            }
        }
    }

    int PercentRandom(List<int> rate)
    {
        int root_rate = 0;
        int min = 999;
        int result = 0;
        root_rate = Random.Range(0, 100);
        //Debug.Log(root_rate);
        for(int i = 0; i < rate.Count; i++)
        {
            if(root_rate <= rate[i])
            {
                if (min > rate[i] -root_rate) {
                    min = rate[i] - root_rate;
                    result = i;
                }
                else if(min == rate[i] - root_rate)
                {
                    result = Random.Range(0, 2);
                    if(result == 0)
                    {
                        min = root_rate - rate[i];
                        result = i;
                    }
                    
                }
            }
        }
        return result;                
    }
    
    void MobInit()
    {
        (int x, int z) loc = new (0, 0);
        int _tmp = 0;
        _tmp = Random.Range(0, 2);        
        if (_tmp == 0)
        {
            loc.x = Random.Range(0, mobdata.Map_x - mobdata.Ally.Count + 1);
            loc.z = 0;
            for (int i = 0; i < mobdata.Ally.Count; i++)
            {

                now_ally.Add(Instantiate(mobdata.Ally[i]));
                now_ally[i].transform.position = new Vector3(loc.x + i, mobdata.Map[loc.z, loc.x + i].Height + 1, loc.z);
                now_ally[i].transform.GetComponent<ActionControl>().panel = panel;
                now_ally[i].name = "ally" + i;
                now_ally[i].tag = "Player";
                now_ally[i].GetComponent<ActionControl>().enabled = true;
                now_ally[i].GetComponent<ActionControl>().enabled = false;

                //Debug.Log(tmp.transform.position);
            }
        }
        else
        {
            loc.z = Random.Range(0, mobdata.Map_y - mobdata.Ally.Count + 1);
            loc.x = 0;
            for (int i = 0; i < mobdata.Ally.Count; i++)
            {
                now_ally.Add(Instantiate(mobdata.Ally[i]));
                now_ally[i].GetComponent<ActionControl>().panel = panel;
                now_ally[i].transform.position = new Vector3(loc.x, mobdata.Map[loc.z + i, loc.x].Height + 1, loc.z + i);
                now_ally[i].name = "ally" + i;
                now_ally[i].tag = "Player";
                now_ally[i].GetComponent<ActionControl>().enabled = true;
                now_ally[i].GetComponent<ActionControl>().enabled = false;

                //Debug.Log(tmp.transform.position);
            }
        }
        for(int i = 0; i < mobdata.Enermy_count; i++)
        {
            loc = GetRandomXZ(loc);
            now_enermy.Add(Instantiate(mobdata.Enermy[i]));
            now_enermy[i].transform.position = new Vector3(loc.x, mobdata.Map[loc.z, loc.x].Height + 1, loc.z);
            now_enermy[i].name = loc.x + ":" + loc.z;
        }
    }
    (int, int) GetRandomXZ((int x, int z) loc)
    {
        loc.x = Random.Range(0, mobdata.Map_x);
        loc.z = Random.Range(0, mobdata.Map_y);
        foreach(GameObject t in now_ally)
        {
            if(t.transform.position.x == loc.x && t.transform.position.z == loc.z)
            {
                Debug.Log("same");
                GetRandomXZ(loc);
                break;
            }
        }
        return loc;
    }
}
