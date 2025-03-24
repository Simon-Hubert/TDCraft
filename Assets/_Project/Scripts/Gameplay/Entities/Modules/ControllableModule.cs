using UnityEngine;

namespace Entities
{
    public class ControllableModule : Module
    {
        public override bool Interact(Character character) {
            BindInputs();
            return true;
        }

        private void BindInputs() {
            throw new System.NotImplementedException();
        }
    }
}
