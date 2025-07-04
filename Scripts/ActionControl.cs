using System.Collections.Generic;
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
    Skill skill;
    Dictionary<int, List<Tile>> range_dic;
    Dictionary<int, List<Tile>> distance_dic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Debug.Log("aaaa");

    }

    private void OnEnable()
    {
        if (init)
        {
            buttons[0].gameObject.SetActive(true);
            buttons[1].gameObject.SetActive(true);
            for (int i = 0; i < now_char.Mob.Skills.Length; i++)
            {
                buttons[i + 2].gameObject.SetActive(true);
                buttons[i + 2].GetChild(0).GetComponent<TextMeshProUGUI>().text = now_char.Mob.Skills[i].Name;
                buttons[i + 2].GetComponent<SkillCon>().skill = now_char.Mob.Skills[i];

            }
        }
        else
        {
            now_char = transform.GetComponent<CharCon>();
            buttons = new Transform[panel.transform.childCount];
            for(int i = 0; i < panel.transform.childCount; i++)
            {
                buttons[i] = panel.transform.GetChild(i);   
            }
            this.enabled = false;
            init = true;
        }        
        foreach(Button b in panel.GetComponentsInChildren<Button>())
        {
            Button a = b;
            a.onClick.AddListener(() => Skill_Click(a));
        }
    }
    private void OnDisable()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
    }

    public void Skill_Click(Button button)
    {
        skill = button.transform.GetComponent<SkillCon>().skill;
        Debug.Log(skill.Name);
        distance_dic = now_char.Init(skill.Distance);
        range_dic = now_char.Init(skill.Range);
    }

    private void Update()
    {
        if(distance_dic != null && range_dic != null)
        {
            now_char.Hilight(distance_dic, "Distance", 2);
        }
    }
}
