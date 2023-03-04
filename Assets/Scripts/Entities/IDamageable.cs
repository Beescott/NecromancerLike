﻿namespace Entities
{
    public interface IDamageable
    {
        public void TakeDamage(float damage);
        public void Die();
    }
}