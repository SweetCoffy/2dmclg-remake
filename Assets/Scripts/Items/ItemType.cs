using UnityEngine;
namespace Game.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item Type")]
    public class ItemType : ScriptableObject
    {
        public string id;
        public string displayName;
        [SerializeField] Sprite m_Sprite;
        [SerializeField] GameObject m_Place;
        public Sprite Sprite => m_Sprite;
        public GameObject Place => m_Place;
    }
}