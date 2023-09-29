using UnityEngine;

namespace SaitoGames.Utilities
{
    public class DefaultPooledOjbect : MonoBehaviour, IPooledObject
    {
        public ReturnPoolEventHandler ReturnEvent { get; set; }

        public GameObject GameObject => gameObject;

        protected virtual void OnDisable()
        {
            ReturnEvent?.Invoke(gameObject);
        }
    }
}