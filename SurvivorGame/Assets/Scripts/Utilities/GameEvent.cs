using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaitoGames.Utilities
{
    [CreateAssetMenu]
    public class GameEvent : ScriptableObject
    {
        public delegate void EventHandler(Type[] schema, object[] args);
        public event EventHandler Response;

        [SerializeField] private bool _logThisEvent;

        public void Raise(Type[] schema = null, object[] args = null)
        {
#if UNITY_EDITOR
            if (_logThisEvent)
                Debug.Log($"Event {name} raised");
#endif

            Response?.Invoke(schema, args);
        }

    }
}