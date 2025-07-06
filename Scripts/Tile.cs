using UnityEngine;
public class Tile : MonoBehaviour
{
    [SerializeField]
    int height;
    [SerializeField]
    bool can_through;
    [SerializeField]
    int x;
    [SerializeField]
    int z;
    [SerializeField]
    Material org_mat;
    
    public int Height{ get => height; set => height = value; }
    public bool Can_through { get => can_through; set => can_through = value; }
    public int X { get => x; set => x = value; }
    public int Z { get => z; set => z = value; }
    public Material Org_mat { get => transform.GetComponent<MeshRenderer>().material; }
}
