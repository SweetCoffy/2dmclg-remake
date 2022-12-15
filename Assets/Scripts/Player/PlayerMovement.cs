using UnityEngine;
namespace Game.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] protected float m_Speed = 10;
        [SerializeField] protected float m_Acceleration = 10;
        protected bool onGround;
        protected PlayerInput input;
        protected Rigidbody2D rb;
        public virtual void Start()
        {
            input = GetComponent<PlayerInput>();
            rb = GetComponent<Rigidbody2D>();
        }
        public virtual void FixedUpdate()
        {
            Vector2 vel = rb.velocity;
            vel.x = Mathf.Lerp(vel.x, m_Speed * input.Movement.x, m_Acceleration * Time.fixedDeltaTime);
            rb.velocity = vel;
        }
    }
}