using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class MobCon : MonoBehaviour
{
    public MapGen map_gen;

    [SerializeField]
    private CharCon char_con;
    [SerializeField]
    private ActionControl action_con;
    [SerializeField]
    private Transform now_hit;
    Ray ray2;

    public TextMeshProUGUI text;
    public GameObject panel;

    List<CharCon> target_list;
    ResultSet result;
    private void Start()
    {
        text = map_gen.text;
        panel = map_gen.panel;
    }

    private void Update()
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
                        if (char_con.Stat.Ap > 0)
                        {
                            text.text = hit.transform.name;
                            text.gameObject.SetActive(true);
                            panel.SetActive(true);
                            action_con = hit.transform.GetComponent<ActionControl>();
                            action_con.panel = panel;
                            action_con.enabled = true;

                            char_con.Hilight(char_con.can_move, "CanTile", 1);
                        }
                    }
                }
                else
                {
                    if (hit.transform.tag == "CanTile")
                    {
                        char_con.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z);
                        char_con.Stat.Ap -= 1;
                        char_con.UnHilight(char_con.can_move);

                        if (char_con.Stat.Ap <= 0)
                        {
                            //char_con.Init(char_con.Stat.Mov, char_con.transform.position);
                            char_con.can_move = new Dictionary<int, List<Tile>>();
                            //char_con = null;
                            action_con.enabled = false;                            
                        }
                        else
                        {
                            char_con.can_move = char_con.Init(char_con.Stat.Mov, char_con.transform.position);
                            //0                           char_con.can_move = char_con.Init(char_con.Mob.Stat.Mov, char_con.transform.position);
                            char_con.Hilight(char_con.can_move, "CanTile", 1);
                            action_con.enabled = true;
                        }
                        map_gen.SetAlpha();
                    }
                    else if (hit.transform.tag == "Player")
                    {
                        if (!char_con.now_skill)
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
                        else
                        {
                            Debug.Log("스킬 사용");

                        }
                    }
                    else if (hit.transform.tag == "Distance" )
                    {
                        Debug.Log("스킬 사용");
                        //    text.gameObject.SetActive(false);
                        //    Debug.Log(action_con.transform.name);
                        //    char_con.UnHilight(action_con.distance_dic, 2);
                        //    action_con.Reset();
                        //    action_con.enabled = false;
                        //    panel.SetActive(false);
                        //    action_con = null;
                        //    char_con = null;

                    }
                    else
                    {
                        text.gameObject.SetActive(false);
                        char_con.UnHilight(char_con.can_move);
                        char_con.UnHilight(action_con.distance_dic, 2);
                        char_con.UnHilight(action_con.range_dic, 1);
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
                    if (hit2.transform.tag == "Distance" || hit2.transform.tag == "Player" || hit2.transform.tag == "Enermy")
                    {
                        now_hit = hit2.transform;
                        //Debug.Log(Input.mousePosition);
                        //char_con.UnHilight(action_con.range_dic, 2);
                        char_con.UnHilight(action_con.range_dic, 1, false);
                        action_con.range_dic = char_con.Init(action_con.skill.Range, hit2.transform.position);
                        char_con.Hilight(action_con.range_dic, "", 1);
                        if (Input.GetMouseButtonDown(0))
                        {
                            target_list = new List<CharCon>(); 
                            GetCharacter();
                            Debug.Log(target_list.Count);
                            char_con.UnHilight(action_con.range_dic, 0, true);
                            text.gameObject.SetActive(false);
                            Debug.Log(action_con.transform.name);
                            char_con.UnHilight(action_con.distance_dic, 2, true);
                            result = action_con.skill.ExecuteSkill(target_list);
                            action_con.Reset();
                            action_con.enabled = false;
                            panel.SetActive(false);
                            action_con = null;
                            char_con = null;                            
                        }

                    }
                }

            }
        }
    }
    public void GetCharacter()
    {
        List<Tile> tmp = action_con.range_dic.Values.SelectMany(list => list).ToList();
        foreach (Tile t in tmp)
        {            
            foreach (GameObject ally in map_gen.now_ally)
            {
                if(ally.transform.position.x == t.transform.position.x && ally.transform.position.z == t.transform.position.z)
                {
                    target_list.Add(ally.GetComponent<CharCon>());
                }
            }
            foreach (GameObject enermy in map_gen.now_enermy)
            {
                if (enermy.transform.position.x == t.transform.position.x && enermy.transform.position.z == t.transform.position.z)
                {
                    target_list.Add(enermy.GetComponent<CharCon>());
                }
            }

        }
    }
}
