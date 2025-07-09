using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionControl : MonoBehaviour
{    
    public GameObject panel;
    CharCon now_char;
    [SerializeField]
    Transform[] buttons;
    [SerializeField]
    bool init = false;
    Button button;
    public Skill skill;
    SkillCon skill_con;
    public Dictionary<int, List<Tile>> range_dic;
    public Dictionary<int, List<Tile>> distance_dic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Debug.Log("aaaa");
        Dictionary<int, List<Tile>> range_dic = new Dictionary<int, List<Tile>>();
        Dictionary<int, List<Tile>> distance_dic = new Dictionary<int, List<Tile>>();

}

    private void OnEnable()
    {
        if (init)
        {
            if (now_char.Stat.Ap > 0)
            {
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(true);
                buttons[0].GetComponent<SkillCon>().skill = now_char.Mob.Skills[0].skill;
                for (int i = 2; i < now_char.Mob.Skills.Length; i++)
                {
                    buttons[i].gameObject.SetActive(true);
                    buttons[i].GetChild(0).GetComponent<TextMeshProUGUI>().text = now_char.Mob.Skills[i].skill.Name;
                    buttons[i].GetComponent<SkillCon>().skill = now_char.Mob.Skills[i].skill;

                }
            }
            else
            {
                for(int i = 0; i< now_char.Mob.Skills.Length; i++)
                {
                    buttons[i].gameObject.SetActive(false);
                }
            }

        }
        else
        {
            now_char = transform.GetComponent<CharCon>();
            buttons = new Transform[panel.transform.childCount];
            for (int i = 0; i < panel.transform.childCount; i++)
            {
                buttons[i] = panel.transform.GetChild(i);
            }
            this.enabled = false;
            init = true;
        }
        range_dic = new Dictionary<int, List<Tile>>();
        distance_dic = new Dictionary<int, List<Tile>>();
        foreach (Transform b in buttons)
        {
            if (b == buttons[0])
            {
                buttons[0].GetComponent<Button>().onClick.AddListener(() => Attack());
            }
            else if (b == buttons[1])
            {
                buttons[1].GetComponent<Button>().onClick.AddListener(() => Defence());
            }
            else
            {
                skill_con = b.GetComponent<SkillCon>();
                Button a = b.GetComponent<Button>();
                a.onClick.RemoveAllListeners();
                a.onClick.AddListener(() => Skill_Click(a));
            }
        }
    }    

    public void Attack()
    {
        buttons[0].GetComponent<SkillCon>().skill = now_char.Mob.Skills[0].skill;
        now_char.Mob.Skills[0].skill.Atk = now_char.Mob.Stat.Atk;
        now_char.Mob.Skills[0].skill.Distance = now_char.Mob.Stat.Distance;
        now_char.Mob.Skills[0].skill.Range = 0;
        Skill_Click(buttons[0].GetComponent<Button>());
    }

    public void Defence()
    {
        buttons[1].GetComponent<SkillCon>().skill = now_char.Mob.Skills[1].skill;
        now_char.Mob.Skills[1].skill.Atk = now_char.Mob.Stat.Atk;
        now_char.Mob.Skills[1].skill.Distance = 0;
        now_char.Mob.Skills[1].skill.Range = 0;

    }

    private void OnDisable()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            Reset();
            buttons[i].gameObject.SetActive(false);
        }
    }

    public void Skill_Click(Button button)
    {
        skill = button.transform.GetComponent<SkillCon>().skill;
        Debug.Log(skill.Distance);
        //Debug.Log("click char:" + now_char.name);
        distance_dic = now_char.Init(skill.Distance, transform.position);
        //Debug.Log("distance: " + distance_dic.Values.Sum(list => list.Count));
        //Debug.Log(distance_dic.Count);
        //range_dic = now_char.Init(skill.Range);
        now_char.UnHilight(now_char.can_move);
        now_char.Hilight(distance_dic, "Distance", 2);
        now_char.now_skill = true;

    }

    public void Reset()
    {
        if (distance_dic != null)
        {
            distance_dic.Clear();
        }
        if (range_dic != null)
        {
            range_dic.Clear();
        }
        now_char.now_skill = false;
        this.enabled = false;
    }

    private void Update()
    {
    }
}
