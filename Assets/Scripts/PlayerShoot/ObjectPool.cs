using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    private GameObject poolRoot;

    private void Awake()
    {
        SharedInstance = this;
    }
    private void Start()
    {
        poolRoot = GameObject.Find("PoolRoot");

        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool, poolRoot.transform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
    public void InstantiateWithPool(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        var bullet = SharedInstance.GetPooledObject();
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bullet.transform.localScale = scale;

        bullet.SetActive(true);
        bullet.GetComponent<ApplyShoot>().trailRenderer.enabled = true;
    }
}