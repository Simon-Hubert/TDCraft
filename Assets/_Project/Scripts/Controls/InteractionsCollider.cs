using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controls
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
        
        void OnTriggerEnter(Collider other)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                _interactables.Add(interactable);
            }
        }

        void OnTriggerExit(Collider other)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                _interactables.Remove(interactable);
            }
        }
    }
}
