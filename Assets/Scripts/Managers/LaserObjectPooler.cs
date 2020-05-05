﻿using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class LaserObjectPooler : MonoBehaviour
    {
        public static LaserObjectPooler SharedInstance;
        private List<GameObject> _pooledObjects;
        public GameObject objectToPool;
        public int maxItemsToPool = 15;
        private Transform _poolParent;
        public bool canExpand;

        void Awake()
        {
            SharedInstance = this;
        }

        // Use this for initialization of the Object Pool
        void Start()
        {
            //Check if we've already been provided some other Game Object to serve as this pools parent..
            //if not, then just use this object
            if (_poolParent == null)
                _poolParent = transform;
            
            _pooledObjects = new List<GameObject>();
            for (int i = 0; i < maxItemsToPool; i++)
            {
                GameObject obj = Instantiate(objectToPool, Vector3.zero, Quaternion.identity, _poolParent);
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
                GameObject obj = Instantiate(objectToPool, Vector3.zero, Quaternion.identity, _poolParent);
                obj.SetActive(false);
                _pooledObjects.Add(obj);
                return obj;
            }


            return null;
        }
    }
}