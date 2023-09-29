using SaitoGames.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SaitoGames.SurvivorGame.GameState
{
    public class DamageNumber : MonoBehaviour, IPooledObject
    {
        public ReturnPoolEventHandler ReturnEvent { get; set; }

        public GameObject GameObject => gameObject;

        [SerializeField] private TextMesh _dmgNumberText;

        private float _timer;


        public void SetDamageNumber(float dmg)
        {
            _dmgNumberText.text = dmg.ToString();
            _timer = 1f;
        }

        private void Update()
        {
            if (_timer > 0f)
            {
                var delta = Time.deltaTime;
                _timer -= delta;
                transform.position += Vector3.up * delta;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}