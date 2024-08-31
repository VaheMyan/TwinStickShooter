using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (poolRoot == null) poolRoot = GameObject.Find("PoolRoot");

        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                tmp = Instantiate(objectToPool, poolRoot.transform);
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                tmp = PhotonNetwork.Instantiate(objectToPool.name, poolRoot.transform.position, Quaternion.identity);
                tmp.transform.SetParent(poolRoot.transform);
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
        }
    }
    private GameObject GetPooledObject()
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
        var bullet = GetPooledObject();
        bullet.transform.position = position;
        bullet.transform.rotation = rotation;
        bullet.transform.localScale = scale;

        bullet.SetActive(true);
    }
}