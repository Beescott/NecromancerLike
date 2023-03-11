using Entities.Events;
using UnityEngine;

namespace Entities.Players
{
    public class Player : Entity
    {
        public override void Die()
        {
            Debug.Log($"Player died");
        }
    }
}