using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SaitoGames.Utilities
{
    [Serializable]
    public class ObjectPooler<T> where T : IPooledObject
    {
        public Transform PoolContainer => _objPoolingContainer;

        private T _objectPrefab;
        private int _initialPoolSize;
        private Transform _objPoolingContainer;

        private Queue<T> _poolObject;
        private bool InsufficientObject => _poolObject.Count <= 0;

        public ObjectPooler()
        {
            _poolObject = new Queue<T>(_initialPoolSize);
        }

        ~ObjectPooler()
        {
            // Unregister event
            foreach (var item in _poolObject)
            {
                item.ReturnEvent -= ReturnObject;
            }
        }

        // Use this to initialize the pooler with a list of objects already created
        public void InitPooler(List<T> objects, Transform container)
        {
            _objPoolingContainer = container;
            foreach (var obj in objects)
            {
                ReturnObject(obj);
            }

            PrePopulatePool();
        }

        // Use this to initialize the pooler with a prefab
        public List<T> InitPooler(T prefab, Transform container, int initialCount = 5)
        {
            _objectPrefab = prefab;
            _initialPoolSize = initialCount;
            _objPoolingContainer = container;

            _poolObject = new Queue<T>(initialCount);

            PrePopulatePool();
            return _poolObject.ToList();
        }

        public List<T> GetListObjects()
        {
            return _poolObject.ToList();
        }

        // Take a new object out of the pool
        public T GetNextObject(bool activeImmediately = true)
        {
            if (InsufficientObject)
            {
                if (_objectPrefab == null)
                    return default;

                SpawnNewObject();
            }
            var nextObject = _poolObject.Dequeue();
            if (activeImmediately)
                nextObject?.GameObject.SetActive(true);

            return nextObject;
        }

        // Put an object back to the pool
        public void ReturnObject(T objToReturn)
        {
            _poolObject.Enqueue(objToReturn);

            if (objToReturn.GameObject.activeSelf)
                objToReturn.GameObject.SetActive(false);

            objToReturn.GameObject.transform.position = Vector3.zero;
            objToReturn.GameObject.transform.parent = _objPoolingContainer;
        }

        public void ReturnObject(GameObject objToReturn)
        {
            if (objToReturn.TryGetComponent<T>(out var obj))
            {
                ReturnObject(obj);
            }
        }

        private void SpawnNewObject()
        {
            var newGO = GameObject.Instantiate(_objectPrefab.GameObject, Vector3.zero, Quaternion.identity, _objPoolingContainer);
            var obj = newGO.GetComponent<T>();
            _poolObject.Enqueue(obj);
            obj.GameObject.SetActive(false);
            obj.ReturnEvent += ReturnObject;
            //newGO.AddComponent<PooledObjectDespawner>().MyPool = this;
        }

        private void PrePopulatePool()
        {
            for (var i = 0; i < _initialPoolSize; i++)
            {
                SpawnNewObject();
            }
        }

        
    }
}