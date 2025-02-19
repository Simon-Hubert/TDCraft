using System;
using System.Reflection;
using UnityEngine;

namespace Controls
{
    public class InteractionController : MonoBehaviour
    {
        #region Fields
        [SerializeField] InteractionsCollider _interactionsCollider;
        #endregion
        
        private void OnEnable()
        {
            GetComponentInParent<PlayerController>().OnInteraction += OnInteract;
        }

        private void OnDisable()
        {
            GetComponentInParent<PlayerController>().OnInteraction -= OnInteract;
        }

        void OnInteract(InteractionType interaction)
        {
            switch (interaction)
            {
                case InteractionType.None:
                    Debug.Log("none");
                    break;
                
                case InteractionType.Interact:
                    Interact();
                    break;
                
                case InteractionType.Defend:
                    Defend();
                    break;
                
                case InteractionType.Farm:
                    Farm();
                    break;
                
                case InteractionType.Talk:
                    Talk();
                    break;
            }
        }

        private void Talk()
        {
            Debug.Log("Talk");
        }

        private void Farm()
        {
            Debug.Log("Farm");
        }

        private void Defend()
        {
            Debug.Log("Defend");
        }

        private void Interact()
        {
            Debug.Log("Interact");
        }
    }
}
