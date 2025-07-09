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


    public TextMeshProUGUI text;

    public GameObject panel;


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

    }

    public void SetAlpha()
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
                mobdata.Map[i, j].name = i + ":" + j;
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
        //if (_tmp == 0)
        //{
        //    loc.x = Random.Range(0, mobdata.Map_x - mobdata.Ally.Count + 1);
        //    loc.z = 0;
        //    for (int i = 0; i < mobdata.Ally.Count; i++)
        //    {

        //        now_ally.Add(Instantiate(mobdata.Ally[i]));
        //        now_ally[i].transform.position = new Vector3(loc.x + i, mobdata.Map[loc.z, loc.x + i].Height + 1, loc.z);
        //        now_ally[i].transform.GetComponent<ActionControl>().panel = panel;
        //        now_ally[i].name = "ally" + i;
        //        now_ally[i].tag = "Player";
        //        now_ally[i].GetComponent<ActionControl>().enabled = true;
        //        now_ally[i].GetComponent<ActionControl>().enabled = false;

        //        //Debug.Log(tmp.transform.position);
        //    }
        //}
        //else
        //{
        //    loc.z = Random.Range(0, mobdata.Map_y - mobdata.Ally.Count + 1);
        //    loc.x = 0;
        //    for (int i = 0; i < mobdata.Ally.Count; i++)
        //    {
        //        now_ally.Add(Instantiate(mobdata.Ally[i]));
        //        now_ally[i].GetComponent<ActionControl>().panel = panel;
        //        now_ally[i].transform.position = new Vector3(loc.x, mobdata.Map[loc.z + i, loc.x].Height + 1, loc.z + i);
        //        now_ally[i].name = "ally" + i;
        //        now_ally[i].tag = "Player";
        //        now_ally[i].GetComponent<ActionControl>().enabled = true;
        //        now_ally[i].GetComponent<ActionControl>().enabled = false;

        //        //Debug.Log(tmp.transform.position);
        //    }
        //}
        now_ally.Add(Instantiate(mobdata.Ally[0]));
        now_ally[0].GetComponent<ActionControl>().panel = panel;
        now_ally[0].transform.position = new Vector3(5, mobdata.Map[0, 5].Height + 1, 0);
        now_ally[0].name = "ally" + 0;
        now_ally[0].tag = "Player";
        now_ally[0].GetComponent<ActionControl>().enabled = true;
        now_ally[0].GetComponent<ActionControl>().enabled = false;
        for (int i = 0; i < mobdata.Enermy_count; i++)
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
