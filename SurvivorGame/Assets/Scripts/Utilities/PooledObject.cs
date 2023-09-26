using UnityEngine;

namespace SaitoGames.Utilities
{
    public delegate void ReturnPoolEventHandler(GameObject obj);

    public class PooledObject : MonoBehaviour
    {
        public ReturnPoolEventHandler ReturnEvent;

        protected virtual void OnDisable()
        {
            ReturnEvent?.Invoke(gameObject);
        }
    }
}