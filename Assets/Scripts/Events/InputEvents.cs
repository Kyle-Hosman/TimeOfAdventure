using UnityEngine;
using System;

public class InputEvents
{
    public InputEventContext inputEventContext { get; private set; } = InputEventContext.DEFAULT;

    public void ChangeInputEventContext(InputEventContext newContext) 
    {
        Debug.Log("Changing Input Event Context from " + this.inputEventContext + " to " + newContext);
        this.inputEventContext = newContext;
        Debug.Log("New Input Event Context is " + this.inputEventContext);
    }

    public event Action<Vector2> onMovePressed;
    public void MovePressed(Vector2 moveDir) 
    {
        if (onMovePressed != null) 
        {
            onMovePressed(moveDir);
        }
    }

    public event Action<InputEventContext> onSubmitPressed;
    public void SubmitPressed()
    {
        if (onSubmitPressed != null) 
        {
            onSubmitPressed(this.inputEventContext);
        }
    }

    public event Action onQuestLogTogglePressed;
    public void QuestLogTogglePressed()
    {
        if (onQuestLogTogglePressed != null) 
        {
            onQuestLogTogglePressed();
        }
    }
}
