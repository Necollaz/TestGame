using System.Collections.Generic;
using UnityEngine;

namespace GameComponents.Scripts.PoolComponents
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Queue<T> _poolQueue;
        
        public int InitialPoolSize { get; private set; }

        public ObjectPool(T prefab, int initialPoolSize)
        {
            _prefab = prefab;
            InitialPoolSize = initialPoolSize;
            _poolQueue = new Queue<T>(initialPoolSize);

            for (int i = 0; i < initialPoolSize; i++)
            {
                T instance = GameObject.Instantiate(_prefab);
                instance.gameObject.SetActive(false);
                _poolQueue.Enqueue(instance);
            }
        }

        public T Get()
        {
            T instance;
            
            if (_poolQueue.Count > 0)
            {
                instance = _poolQueue.Dequeue();
                
                instance.gameObject.SetActive(true);
            }
            else
            {
                instance = GameObject.Instantiate(_prefab);
            }

            return instance;
        }
        
        public void Return(T item)
        {
            item.gameObject.SetActive(false);
            _poolQueue.Enqueue(item);
        }
    }
}