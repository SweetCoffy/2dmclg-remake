using UnityEngine;
namespace Game.Health
{
    public class ObjectHealth : MonoBehaviour, IDamageable, IDamageSource
    {
        [SerializeField] protected float m_MaxHealth = 10;
        public float MaxHealth
        {
            get => m_MaxHealth;
        }
        public float Health { get; protected set; }
        public bool IsAlive { get; protected set; } = true;
        public System.Action<IDamageSource> OnDeath;
        public System.Action<float, IDamageSource> OnDamage;
        [SerializeField] protected GameObject m_DestroyEffect;
        public IDamageable Damageable
        {
            get => this;
        }
        public virtual void Start()
        {
            Health = MaxHealth;
        }
        public virtual void Update()
        {

        }
        public virtual void TakeDamage(float amount, IDamageSource source = null)
        {
            Health -= amount;
            if (OnDamage != null) OnDamage(amount, source);
            if (Health <= 0)
            {
                Health = 0;
                Kill(source);
            }
        }
        public virtual void Kill(IDamageSource source = null)
        {
            if (!IsAlive) return;
            if (OnDeath != null) OnDeath(source);
            if (m_DestroyEffect) Instantiate(m_DestroyEffect, transform.position, transform.rotation);
            Destroy(gameObject);
            IsAlive = false;
        }
    }
}