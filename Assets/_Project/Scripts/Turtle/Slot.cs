using UnityEngine;

namespace Controls
{
   public class Slot : MonoBehaviour, IInteractable
   {
       [SerializeField] private Module _module;
       [SerializeField] private GameObject _modulePrefab;
       [SerializeField] private Turtle _turtle;

       public InteractableID GetInteractableID() => InteractableID.Module;
       public bool Interact(Character.Character character = null)
       {
           if (_module != null)
           {
               _module.Interact(character);
               character.transform.SetParent(transform);
               return true;
           }
           GameObject go = Instantiate(_modulePrefab, transform);
           _module = go.GetComponent<Module>();
           _module.Init(_turtle);
           return false;
       }
   }
 
}
