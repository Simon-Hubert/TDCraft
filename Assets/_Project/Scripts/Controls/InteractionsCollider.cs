using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
    public class InteractionsCollider : MonoBehaviour
    {
        List<IInteractable> _interactables;
        
        #region Properties
        public List<IInteractable> Interactables { get => _interactables;}
        #endregion

        public void AddInteractable(IInteractable interactable)
        {
            _interactables.Add(interactable);
        }
    }
}
