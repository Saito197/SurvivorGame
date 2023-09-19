using SaitoGames.Utilities;
using UnityEngine;

namespace SaitoGames.SmasherGame.Character
{
    public interface IControlable
    {
        void DirectionCommand(Vector2 direction);
        void ActionCommand(CharacterAction action);
    }
}