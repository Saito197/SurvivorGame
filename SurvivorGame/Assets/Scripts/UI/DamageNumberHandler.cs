using SaitoGames.Utilities;
using UnityEngine;

namespace SaitoGames.SurvivorGame.GameState
{
    public class DamageNumberHandler : MonoBehaviour
    {
        [SerializeField] private GameEvent _damageEvent;
        [SerializeField] private DamageNumber _dmgNumberPrefab;
        private ObjectPooler<DamageNumber> _dmgNumberPooler;

        private void Awake()
        {
            _damageEvent.Response += OnDamage;
            _dmgNumberPooler = new ObjectPooler<DamageNumber>();
            _dmgNumberPooler.InitPooler(_dmgNumberPrefab, transform, 50);
        }

        private void OnDamage(object[] args)
        {
            if (args == null || args.Length == 0) return;

            var pos = (Vector3)args[0];
            var dmg = (float)args[1];

            var dmgNumber = _dmgNumberPooler.GetNextObject(false);
            dmgNumber.SetDamageNumber(dmg);
            dmgNumber.transform.position = pos;
            dmgNumber.gameObject.SetActive(true);
        }
    }
}