using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterState : MonoBehaviour
{
    protected CharacterStateMachine _stateMachine;
    protected Character _character;


    public virtual void Initialize(Character character, CharacterStateMachine stateMachine)
    {
       _character = character;
       _stateMachine = stateMachine;
    }
    public virtual void StateEnter() {}
    public virtual void StateExit() {}
    public virtual void StateUpdate(float deltaTime) {}
    public virtual void StateFixedUpdate(float fixedDeltaTime) {}
    
}
