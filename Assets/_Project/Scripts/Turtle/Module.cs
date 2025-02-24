using System;
using Character;
using Controls;
using UnityEngine;

namespace Controls
{
    public class Module : MonoBehaviour
    {
        protected Turtle Turtle;
        protected Life Life;
        protected Character.Character CharacterOwner;
        protected CharacterPilotingState CharacterPilotingState;

        public virtual void Init(Turtle turtle)
        {
            Turtle = turtle;
        }
        public virtual void ManageInteract() {}
        public virtual void ManageMove(Vector2 direction) {}
        public virtual void ManageShoot() {}
        public virtual void BindInput() {}
        public virtual void UnBindInput() {}

        public void Interact(Character.Character character = null)
        {
            CharacterOwner = character;
            character.transform.position = transform.position;
            CharacterPilotingState = character.GetComponent<CharacterPilotingState>();
            BindInput();
        }
    }
}


