using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingObject : Singleton<PoolingObject>
{
    Dictionary<GameObject, List<GameObject>> pools;

    public List<GameObject> pooledObjects = new List<GameObject>();
    public int size;

    protected override void Awake()
    {
        base.Awake();
        // Initialize each pool
        pools = new Dictionary<GameObject, List<GameObject>>();
        foreach (var x in pooledObjects)
        {
            if(!pools.ContainsKey(x))
                pools.Add(x, new List<GameObject>());
            for(int i = 0; i < size; i++)
            {
                GameObject o = Instantiate(x, this.transform);
                o.SetActive(false);
                pools[x].Add(o);
            }
        }
    }

    public GameObject SpawnFromPool(GameObject key, Vector3 position, Quaternion rotation)
    {
        // Find an inactive object with the specified tag
        if (pools.ContainsKey(key))
        {
            foreach(var x in pools[key])
            {
                if (!x.activeInHierarchy)
                {
                    x.transform.position = position;
                    x.transform.rotation = rotation;
                    x.SetActive(true);
                    return x;
                }
            }
            GameObject o = Instantiate(key, this.transform);
            pools[key].Add(o);
            o.transform.position = position;
            o.transform.rotation = rotation;
            o.SetActive(true);
            return o;
        }
        Debug.LogWarning("No available object in the pool with key: " + key.name);
        return null;
    }
}
