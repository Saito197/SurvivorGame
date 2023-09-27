using UnityEngine;

namespace SaitoGames.Utilities
{
    public delegate void ValueChangedHandler();

    [CreateAssetMenu]
    public class FloatVariableAsset : ScriptableObject
    {
        public event ValueChangedHandler OnValueChanged;
        public float Value
        {
            get => _value;
            set
            {
#if UNITY_EDITOR
                if (_logChanges)
                    Debug.Log($"Value of {name} changed from:{_value} to:{value}");
#endif
                _value = value;
                OnValueChanged?.Invoke();
            }
        }

        [SerializeField] private float _value;
        [SerializeField] private bool _logChanges;
    }
}