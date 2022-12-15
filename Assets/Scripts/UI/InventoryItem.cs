using UnityEngine;
using UnityEngine.UI;
namespace Game.UI
{
    public class InventoryItem : MonoBehaviour
    {
        public Inventory inv;
        public int slot;
        [SerializeField] Text m_CountText;
        [SerializeField] Image m_Image;
        [SerializeField] Button m_Button;
        void Start()
        {
            m_Button.interactable = inv.target.canRearrange;
            if (inv.target.canRearrange) m_Button.onClick.AddListener(DoClick);
        }
        public void DoClick()
        {
            if (inv.swap == -1)
            {
                inv.swap = slot;
                return;
            }
            inv.target.Swap(slot, inv.swap);
            inv.swap = -1;
        }
        void Update()
        {
            if (inv.PlayerInventory && inv.PlayerInventory.SelectedSlot == slot)
            {
                transform.localScale = Vector3.one * 1.1f;
            }
            else transform.localScale = Vector3.one;
            var stack = inv.target.items[slot];
            m_Image.enabled = stack.count > 0;
            if (m_Image.enabled) m_Image.sprite = stack.Info.Sprite;
            m_CountText.enabled = m_Image.enabled;
            if (m_CountText.enabled) m_CountText.text = stack.count.ToString();
        }
    }
}