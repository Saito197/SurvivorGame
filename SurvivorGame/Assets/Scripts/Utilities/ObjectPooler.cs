using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SaitoGames.Utilities
{
    public class ObjectPooler : MonoBehaviour
    {
        public Transform PoolContainer => _objPoolingContainer;

        [SerializeField] private GameObject _objectPrefab;
        [SerializeField] private int _initialPoolSize;
        [SerializeField] private Transform _objPoolingContainer;

        private Queue<GameObject> _poolObject;
        private bool InsufficientObject => _poolObject.Count <= 0;

        private void Awake()
        {
            _poolObject = new Queue<GameObject>(_initialPoolSize);
            for (var i = 0; i < _initialPoolSize; i++)
            {
                SpawnNewObject();
            }
        }

        private void SpawnNewObject()
        {
            var newGO = Instantiate(_objectPrefab, Vector3.zero, Quaternion.identity, _objPoolingContainer);
            _poolObject.Enqueue(newGO);
            newGO.SetActive(false);
            //newGO.AddComponent<PooledObjectDespawner>().MyPool = this;
        }

        public List<GameObject> GetListObjects()
        {
            return _poolObject.ToList();
        }

        public void InitPooler(List<GameObject> objects, Transform container = null)
        {
            _objPoolingContainer = container ?? _objPoolingContainer ?? transform;
            foreach (var obj in objects)
            {
                ReturnObject(obj);
            }
        }

        public List<GameObject> InitPooler(GameObject prefab, int initialCount, Transform container)
        {
            _objectPrefab = prefab;
            _initialPoolSize = initialCount;
            _objPoolingContainer = container;

            _poolObject = new Queue<GameObject>(initialCount);

            for (int i = 0; i < _initialPoolSize; i++)
            {
                SpawnNewObject();
            }
            return _poolObject.ToList();
        }

        public GameObject GetNextObject(bool activeImmediately = true)
        {
            if (InsufficientObject)
            {
                if (_objectPrefab == null)
                    return null;

                SpawnNewObject();
            }
            var nextObject = _poolObject.Dequeue();
            if (activeImmediately)
                nextObject?.SetActive(true);

            return nextObject;
        }

        public void ReturnObject(GameObject objToReturn)
        {
            _poolObject.Enqueue(objToReturn);

            if (objToReturn.activeSelf)
                objToReturn.SetActive(false);

            objToReturn.transform.position = Vector3.zero;
            objToReturn.transform.parent = _objPoolingContainer;
        }
    }

}