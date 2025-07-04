using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionControl : MonoBehaviour
{    
    public GameObject panel;
    CharCon now_char;
    [SerializeField]
    Transform[] button;
    [SerializeField]
    bool init = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        Debug.Log("aaaa");

    }

    private void OnEnable()
    {
        if (init)
        {
            for (int i = 0; i < now_char.Mob.Skills.Length; i++)
            {
                button[i + 2].gameObject.SetActive(true);
                button[i + 2].GetChild(0).GetComponent<TextMeshProUGUI>().text = now_char.Mob.Skills[i].Name;

            }
        }
        else
        {
            now_char = transform.GetComponent<CharCon>();
            button = new Transform[panel.transform.childCount];
            for(int i = 0; i < panel.transform.childCount; i++)
            {
                button[i] = panel.transform.GetChild(i);
            }
            this.enabled = false;
            init = true;
        }

    }
    private void OnDisable()
    {
        for(int i = 2; i < button.Length; i++)
        {
            button[i].gameObject.SetActive(false);
        }
    }
}
