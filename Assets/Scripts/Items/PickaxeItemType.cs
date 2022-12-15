using UnityEngine;
namespace Game.Items
{
    [CreateAssetMenu(fileName = "New Pickaxe Item", menuName = "Pickaxe Item Type")]
    public class PickaxeItemType : ItemType
    {
        [SerializeField] public float m_Tier = 1;
        [SerializeField] public float m_Damage = 25;
    }
}