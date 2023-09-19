using SaitoGames.Utilities;
using UnityEngine;

namespace SaitoGames.SmasherGame.Character
{
    public enum CharacterAction
    {
        // Define a list of input action/commands 
        Dodge,
        Attack,
        Special1,
        Special2
    }

    public interface IControlable
    {
        void DirectionCommand(Vector2 direction);
        void ActionCommand(CharacterAction action);
    }
}