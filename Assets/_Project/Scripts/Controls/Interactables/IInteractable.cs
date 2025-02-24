using UnityEngine.TextCore.Text;

namespace Controls
{
    public enum InteractableID
    {
        None,
        Farmable,
        Module,
        PNJ
    };
    public interface IInteractable
    {

        InteractableID GetInteractableID();
        bool Interact(Character.Character character = null);
    }
}
