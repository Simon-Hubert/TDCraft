using System;
using UnityEngine;

namespace Entities
{
    public abstract class Module : MonoBehaviour, IInteractable
    {
        public event Action OnPlaced;
        public event Action OnRemoved;
        
        public InteractableID GetInteractableID() => InteractableID.Module;

        public abstract bool Interact(Character character);

        public virtual void Place() {
            OnPlaced?.Invoke();
        }

        protected virtual void Remove() {
            OnRemoved?.Invoke();
        }
    }
}