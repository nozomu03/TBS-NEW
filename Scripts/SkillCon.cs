using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCon : MonoBehaviour
{
    public CharCon now_char;
    Camera cam;
    Tile now_node;
    Dictionary<int, List<Tile>> distance_dic;
    Dictionary<int, List<Tile>> range_dic;
    Button[] buttons;
    Ray ray;
    private void OnEnable()
    {
        cam = Camera.main;
        distance_dic = new Dictionary<int, List<Tile>>();
        range_dic = new Dictionary<int, List<Tile>>();
    }

    private void Update()
    {
        //ray = cam.ScreenPointToRay(Input.mousePosition);
        //if(Physics.Raycast(ray, out RaycastHit hit))
        //{
        //    if(now_node == null || now_node != hit.transform.gameObject)
        //    {
        //        now_node = hit.transform.GetComponent<Tile>();
        //        distance_dic = now_char.Init()
        //    }
        //}
    }

    public void Skill_Click()
    {
        Debug.Log("Å¬¸¯µÊ!");
    }
}
