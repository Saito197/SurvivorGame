using SaitoGames.Utilities;
using System;
using UnityEngine;

namespace SaitoGames.SurvivorGame.Character
{
    public enum CharacterAction
    {
        // Define a list of input action/commands that executes once when the button is pressed
        None,
        Dodge,
        Special1,
        Special2
    }

    public enum ControllerValue
    {
        None,
        Strafe
    }

    public interface IControlable
    {
        void MovementCommand(Vector2 direction);
        void LookDirectionCommand(Vector2 direction);
        void ActionCommand(CharacterAction action);
        void ValueUpdateCommand(ControllerValue value, bool isPressed);
    }
}