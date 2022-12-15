using UnityEngine;
using Game.Items;
namespace Game.Buildings
{
    public class ItemDuct : Building
    {
        [SerializeField] protected float m_Speed = 5;
        [SerializeField] protected int m_TransportAmount = 4;
        [SerializeField] protected SpriteRenderer m_Renderer;
        [SerializeField] protected SpriteRenderer m_ItemRenderer;
        protected float nextItem;
        public override void Start()
        {
            base.Start();
            m_ItemRenderer.transform.rotation = Quaternion.identity;
        }
        public virtual void FixedUpdate()
        {
            if (Container.IsEmpty)
            {
                nextItem = Time.fixedTime + (1 / m_Speed);
                m_ItemRenderer.enabled = false;
            }
            else
            {
                float time = nextItem - Time.fixedTime;
                float t = 1 - Mathf.Clamp01(time / (1 / m_Speed));
                m_ItemRenderer.transform.localPosition = Vector2.right * t;
                m_ItemRenderer.enabled = true;
                m_ItemRenderer.sprite = Container.items[0].Info.Sprite;
            }
            if (Time.fixedTime > nextItem)
            {
                nextItem = Time.fixedTime + (1 / m_Speed);
                Building forwardBuilding = AdjacentBuilding<Building>(Facing);
                Building backBuilding = AdjacentBuilding<Building>(-Facing);
                ItemContainer forward = forwardBuilding?.Container;
                ItemContainer back = backBuilding?.Container;
                if (forward?.allowInput == true)
                {
                    forward.AddItem(Container.TakeFirstItem(m_TransportAmount));
                }
                if (back?.allowOutput == true)
                {
                    ItemStack s = back.TakeFirstItem(m_TransportAmount);
                    Container.AddItem(s);
                }
                if (!forwardBuilding)
                {
                    var dropped = ItemManager.SpawnDrop(Position + ((Vector2)Facing / 2), Container.TakeFirstItem(m_TransportAmount));
                    dropped.PickupTime = Time.fixedTime + 0.25f;
                    dropped.rb.velocity = (Vector2)Facing * m_Speed;
                }
            }
        }
    }
}