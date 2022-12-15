using UnityEngine;
using System.Collections.Generic;
using System.Linq;
namespace Game.Items
{
    [CreateAssetMenu(fileName = "New Crafting Recipe", menuName = "Crafting Recipe")]
    public class CraftingRecipe : ScriptableObject
    {
        [SerializeField] ItemStack[] m_Input;
        [SerializeField] ItemStack m_Output;
        public ItemStack[] Input => m_Input;
        public ItemStack Output => m_Output;
        public int Craftable(IEnumerable<ItemStack> items)
        {
            return (from req in m_Input
                    from item in items
                    where item.id == req.id
                    select item.count / req.count).Min();
        }
    }
}