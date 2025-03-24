namespace Entities
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
        bool Interact(Character character = null);
    }
}
