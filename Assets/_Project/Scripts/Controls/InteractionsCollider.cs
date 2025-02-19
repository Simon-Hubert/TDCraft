using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
    public enum InteractionType
    {
        None,
        Talk,
        Farm,
        Defend,
        Interact
    };
    public class InteractionsCollider : MonoBehaviour
    {
        //List<Interactable> _interactables;
        InteractionType _interactionType;
        
        #region Properties
        public InteractionType InteractionType { get => _interactionType; }
        //public List<Interactable> Interactables { get => _interactables}
        #endregion

        private void Awake()
        {
            _interactionType = InteractionType.None;
        }

        public InteractionType GetPossibleInteractions()
        {
            InteractionType result = InteractionType.None;
            return result;
        }

        /*public AddInteractable(Interactable interactable)
        {
            interactables.Add(interactable);
        }*/
    }
}
