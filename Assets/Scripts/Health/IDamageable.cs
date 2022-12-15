namespace Game.Health
{
    public interface IDamageable
    {
        public bool IsAlive { get; }
        public void TakeDamage(float amount, IDamageSource source = null);
        public void Kill(IDamageSource source = null);
    }
}