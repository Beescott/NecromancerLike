using System.Collections.Generic;
using UnityEngine;

namespace General
{
    [CreateAssetMenu(fileName = "ResetManager", menuName = "Managers/Reset Manager")]
    public class ResetManager : ScriptableObject
    {
        [SerializeField] private List<Object> resettables = new List<Object>();
        
        public bool HasUpdate { get; private set; }
        
        public void Initialize() { HasUpdate = false; }

        public void OnStart()
        {
            foreach (Object resettable in resettables)
            {
                if (resettable is IResettable iResettable)
                    iResettable.Reset();
            }
        }

        public void OnUpdate() { }
    }
}
