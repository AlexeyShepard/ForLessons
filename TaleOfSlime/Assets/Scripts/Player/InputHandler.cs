using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    public GameObject MovementJoystick;

    public Button JumpButton;

    public Button AttackButton;

    private FloatingJoystick _MovementJoystick;

    private JumpButton _JumpButton;

    private AttackButton _AttackButton;


    private void Start()
    {
        _MovementJoystick = MovementJoystick.GetComponent<FloatingJoystick>();
        _JumpButton = JumpButton.GetComponent<JumpButton>();
        _AttackButton = AttackButton.GetComponent<AttackButton>();
    }

    public Vector2 GetDirectionMovement()
    {
        return _MovementJoystick.Direction;
    }

    public bool JumpButtonWasClicked()
    {
        return _JumpButton.IsPressed;
    }

    public bool AttackButtonWasClicked()
    {
        return _AttackButton.IsPressed;
    }

}
