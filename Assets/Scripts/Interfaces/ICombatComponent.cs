namespace Interfaces
{
    public interface ICombatComponent
    {
        void TakeDamage(float damageAmount);
        void PlayHitSound();
    }
}