using UnityEngine;
namespace Game.Player
{
    public abstract class PlayerInput : MonoBehaviour
    {
        public abstract Vector2 Movement { get; protected set; }
        public abstract Vector2 LocalCursorPosition { get; protected set; }
        public abstract Vector2 WorldCursorPosition { get; }
        public abstract float Hotbar { get; protected set; }
        public abstract bool Ctrl { get; protected set; }
        public abstract System.Action OnRotate { get; set; }
        public abstract System.Action OnUse { get; set; }
        public abstract System.Action OnDrop { get; set; }
    }
}