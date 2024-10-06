using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Items", order = 1)]
public class Item : ScriptableObject
{
    public int id;
    public string name;
    public int stackSize;
    public Sprite icon;
    public int count;
}