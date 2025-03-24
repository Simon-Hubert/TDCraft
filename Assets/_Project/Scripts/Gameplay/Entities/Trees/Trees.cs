using Gameplay;

namespace Entities.Trees
{
    public class Trees : Farmable, IInteractable
    {
        public InteractableID GetInteractableID() => InteractableID.Farmable;

        public bool Interact(Character character = null)
        {
            OnDamaged?.Invoke();
            _currentLife--;
            if (_currentLife <= 0) DropResource();
            return true;
        }
    }
}


