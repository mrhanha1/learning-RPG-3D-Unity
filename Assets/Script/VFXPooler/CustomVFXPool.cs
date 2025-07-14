using System.Collections.Generic;
using UnityEngine;

public class VFXPool
{
    private Queue<ParticleSystem> pool = new Queue<ParticleSystem>();
    public VFXData data;
    Transform parent;

    public VFXPool(VFXData vfxData, Transform container)
    {
        data = vfxData;
        parent = container;
        var poolParent = new GameObject(data.name + " Pool").transform;
        poolParent.SetParent(container);
        Prewarm(data.initPoolSize);
    }
    public ParticleSystem Get()
    {
        if (pool.Count == 0)
        {
            return CreateNew();
        }
        return pool.Dequeue();
    }
    public void Return(ParticleSystem particleS)
    {
        particleS.gameObject.SetActive(false);
        pool.Enqueue(particleS);
    }
    public void Prewarm(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var particleS = CreateNew();
            pool.Enqueue(particleS);
        }
    }
    private ParticleSystem CreateNew()
    {
        var particleS = Object.Instantiate(data.prefab, parent);
        particleS.gameObject.SetActive(false);
        return particleS;
    }
}
