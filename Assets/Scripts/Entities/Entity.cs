using Entities.Events;
using General;
using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour, IDamageable
    {
        [SerializeField] protected float life;

        private bool IsDead => life <= 0;
        
        public virtual void TakeDamage(float damage)
        {
            if (IsDead)
                return;

            life -= damage;
            if (IsDead)
                Die();
        }

        public virtual void Die()
        {
            EventBus.Raise(new OnEntityDeath() {Entity = this});
            Destroy(gameObject);
        }
    }
}