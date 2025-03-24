using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class InteractionsCollider : MonoBehaviour
    {
        #region Fields
        [SerializeField] CircleCollider2D _cCollider2D;
        

        #endregion
        
        #region Variables
        List<IInteractable> _interactables = new List<IInteractable>();
        #endregion
        
        #region Properties
        public List<IInteractable> Interactables { get => _interactables;}
        #endregion
        
        void OnTriggerEnter2D(Collider2D other)
        {
            Debug.LogWarning("FIND" + other.name);
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                _interactables.Add(interactable);
                Debug.LogWarning("Add");
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            Debug.LogWarning("LOST" + other.name);
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                _interactables.Remove(interactable);
                Debug.LogWarning("Remove");
            }
        }
    }
}
