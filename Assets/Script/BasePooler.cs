using UnityEngine;
using System.Collections.Generic;
public abstract class BasePooler <T> : MonoBehaviour where T : Component
{
    [SerializeField] protected T prefab;
    [SerializeField] protected int poolSize = 5;
    protected Queue<T> pool = new Queue<T>();
    protected virtual void Awake()
    {
        pool = new Queue<T>();
        for (int i = 0; i < poolSize; i++)
        {
            T obj = Instantiate(prefab);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    public virtual T GetObject(Vector3 position, Quaternion rotation)
    {// trả về object từ pool, đặt vị trí và xoay
        T obj = pool.Count > 0 ? pool.Dequeue() : Instantiate(prefab);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.gameObject.SetActive(true);
        return obj;
    }
    public virtual void ReturnObject(T obj)
    {// trả obj vào pool
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
    protected virtual void OnSpawn(T obj)
    {
        // Optional: Override this method to perform actions when an object is spawned
    }
}
