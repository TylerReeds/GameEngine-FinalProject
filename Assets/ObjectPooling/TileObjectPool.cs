using System.Collections.Generic;
using UnityEngine;

public class TileObjectPool : MonoBehaviour
{
    private Queue<GameObject> pool = new Queue<GameObject>();

    public GameObject Get(GameObject prefab)
    {
        if (pool.Count > 0)
        {
            // Get an object from the pool
            var pooledObj = pool.Dequeue();
            // Get an object from the pool
            pooledObj.SetActive(true); 
            Debug.Log($"Reusing {pooledObj.name} from pool");
            return pooledObj;
        }
        else
        {
            Debug.Log($"Instantiating new {prefab.name}");
            // Instantiate a new object if the pool is empty
            return Instantiate(prefab); 
        }
    }

    public void Return(GameObject obj)
    {
        Debug.Log($"Returning {obj.name} to pool");
        obj.SetActive(false); 
        obj.transform.SetParent(this.transform); 
        pool.Enqueue(obj); 
    }
}
