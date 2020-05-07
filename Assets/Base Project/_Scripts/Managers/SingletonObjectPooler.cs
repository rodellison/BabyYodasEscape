﻿using System.Collections.Generic;
using UnityEngine;

namespace Base_Project._Scripts.Managers
{
    public abstract class SingletonObjectPooler : MonoBehaviour
    {
        public static SingletonObjectPooler SharedInstance;
        
        private List<GameObject> _pooledObjects;
        public GameObject objectToPool;
        public int maxItemsToPool = 15;
        private Transform _poolAnchor;
        public bool canExpand;

        void Awake()
        {
            //This is the main difference between this version (SingletonObjectPooler), and the other (MultiUseObjectPooler).
            //In this Singleton version, the code here ensures there is only one Pool created. 
            if (SharedInstance != null && SharedInstance != this)
                Destroy(gameObject);
            else
                SharedInstance = this;
        }

        // Use this for initialization of the Object Pool
        void Start()
        {
           //Check if we've already been provided some other Game Object to serve as this pools parent..
           //if not, then just use this object
            if (_poolAnchor == null)
                _poolAnchor = transform;
            
            _pooledObjects = new List<GameObject>();
            for (int i = 0; i < maxItemsToPool; i++)
            {
                GameObject obj = Instantiate(objectToPool, Vector3.zero, Quaternion.identity, transform);
                obj.SetActive(false);
                _pooledObjects.Add(obj);
            }
        }

        /// <summary>
        /// Method to return a pooled object to the requestor if one is available and is currently inactive. 
        /// </summary>
        /// <returns>Pooled GameObject</returns>
        public GameObject GetPooledObject()
        {
            foreach (var t in _pooledObjects)
            {
                if (!t.activeInHierarchy)
                {
                    return t;
                }
            }

            if (canExpand)
            {
                GameObject obj = Instantiate(objectToPool, Vector3.zero, Quaternion.identity, transform);
                obj.SetActive(false);
                _pooledObjects.Add(obj);
                return obj;
            }


            return null;
        }
    }
}