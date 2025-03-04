using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
   public class Slot : MonoBehaviour, IInteractable
   {
       [SerializeField] private Module _module;
       [SerializeField] private GameObject _modulePrefab;
       [SerializeField] private Turtle _turtle;
       [SerializeField] private SO_ModuleDatabase _moduleDatabase;
       [SerializeField] private SpriteRenderer _spriteRenderer;
       [SerializeField] private Sprite _defaultSprite;

       private void Start()
       {
           Init();
       }

       void Init()
       {
           foreach (SO_ModuleData module in _moduleDatabase.moduleDatas)
           {
               module.Craftable = false;
           }
       }

       private void OnTriggerEnter2D(Collider2D other)
       {
           if (other.tag == "Player")
           {
               if (_module == null)
               {
                   ShowModuleCraftable();
               }
           }
       }

       private void OnTriggerExit2D(Collider2D other)
       {
           if (other.tag == "Player")
           {
               if (_module == null)
               {
                   _spriteRenderer.sprite = _defaultSprite;
               }
           }
       }

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

       void ShowModuleCraftable()
       {
           foreach (SO_ModuleData moduleData in _moduleDatabase.moduleDatas)
           {
               if (_turtle.Inventory.IsInInventory(moduleData.Needings)) moduleData.Craftable = true;
               else moduleData.Craftable = false;
           }

           foreach (SO_ModuleData moduleData in _moduleDatabase.moduleDatas)
           {
               if (moduleData.Craftable)
               {
                   _spriteRenderer.sprite = moduleData.SpriteBuildable;
               }
           }
           
       }
   }
 
}
