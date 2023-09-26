using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaitoGames.Utilities
{
    [CreateAssetMenu]
    public class GameEvent : ScriptableObject
    {
        public delegate void EventHandler(object[] args);
        public event EventHandler Response;

        [SerializeField] private bool _logThisEvent;

        public void Raise(object[] args = null)
        {
#if UNITY_EDITOR
            if (_logThisEvent)
                Debug.Log($"Event {name} raised");
#endif

            Response?.Invoke(args);
        }

    }
}