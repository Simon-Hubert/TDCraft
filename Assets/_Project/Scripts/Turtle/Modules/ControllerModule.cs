using UnityEngine;

namespace Controls
{
    public class ControllerModule : Module
    {
        [SerializeField] private float _speed;

        #region Module Methods
        public override void BindInput()
        {
            Debug.Log("ControllerModule BindInput");
            CharacterPilotingState.OnMove += ManageMove;
            CharacterPilotingState.OnInteract += ManageInteract;
        }

        public override void UnBindInput()
        {
            CharacterPilotingState.OnMove -= ManageMove;
            CharacterPilotingState.OnInteract -= ManageInteract;
        }

        public override void ManageMove(Vector2 direction)
        {
            Turtle.Movements = new Vector3(direction.x * (_speed * Time.deltaTime), direction.y * (_speed * Time.deltaTime));
        }

        public override void ManageInteract()
        {
            Turtle.Movements = Vector3.zero;
            UnBindInput();
            CharacterOwner = null;
            CharacterPilotingState = null;
        }
        #endregion

    } 
}

