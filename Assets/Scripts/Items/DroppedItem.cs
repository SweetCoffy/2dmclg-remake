using UnityEngine;
namespace Game.Items
{
    public class DroppedItem : MonoBehaviour
    {
        public ItemStack Stack
        {
            get => _stack;
            set
            {
                _stack = value;
                UpdateItem();
            }
        }
        ItemStack _stack;
        [SerializeField] SpriteRenderer m_Renderer;
        public float PickupTime { get; set; }
        public Rigidbody2D rb { get; private set; }
        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            PickupTime = Time.fixedTime + 0.25f;
        }
        void Start()
        {
            UpdateItem();
        }
        void UpdateItem()
        {
            if (Stack.count <= 0)
            {
                Destroy(gameObject);
            }
            m_Renderer.sprite = Stack.Info.Sprite;
        }
        void OnTriggerEnter2D(Collider2D col)
        {
            if (Time.time < PickupTime) return;
            ItemContainer container = col.GetComponent<ItemContainer>();
            if (!container) return;
            if (!container.allowInput) return;
            Stack = container.AddItem(Stack);
        }
    }
}