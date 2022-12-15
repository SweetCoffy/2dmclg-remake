using UnityEngine;
namespace Game.Player
{
    public class PlayerCore : MonoBehaviour
    {
        public static PlayerCore Local { get; private set; }
        public PlayerHealth Health { get; private set; }
        public PlayerInventory Inventory { get; private set; }
        public PlayerMovement Movement { get; private set; }
        public PlayerInput Input { get; private set; }
        public PlayerBuilding Building { get; private set; }
        void Awake()
        {
            Local = this;
            Health = GetComponent<PlayerHealth>();
            Inventory = GetComponent<PlayerInventory>();
            Movement = GetComponent<PlayerMovement>();
            Input = GetComponent<PlayerInput>();
            Building = GetComponent<PlayerBuilding>();
        }
    }
}