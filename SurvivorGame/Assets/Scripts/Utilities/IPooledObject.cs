using UnityEngine;

namespace SaitoGames.Utilities
{
    public delegate void ReturnPoolEventHandler(GameObject obj);

    public interface IPooledObject
    {
        public ReturnPoolEventHandler ReturnEvent { get; set; }
        public GameObject GameObject { get; }
    }
}