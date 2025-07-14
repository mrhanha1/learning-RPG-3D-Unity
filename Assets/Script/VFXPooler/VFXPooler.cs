using UnityEngine;

public class VFXPooler : BasePooler<ParticleSystem>
{
    public VFXPooler(ParticleSystem prefab, int initialPoolSize = 5)
    {
        this.prefab = prefab;
        this.poolSize = initialPoolSize;
    }
    public void Init(ParticleSystem prefab, int initialPoolSize = 5)
    {
        base.Awake(); // Call the base Awake to initialize the pool
        this.prefab = prefab;
        this.poolSize = initialPoolSize;
        for (int i = 0; i < poolSize; i++)
        {
            ParticleSystem obj = Instantiate(prefab);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    protected override void Awake()
    {
        base.Awake();
    }
    public override void Spawn(Vector3 position, Quaternion rotation)
    {
        ParticleSystem obj = GetObject(position, rotation);
        obj.Play();
        StartCoroutine(DisableAfterDuration(obj));
        Debug.Log($"poolsize now is {poolSize}");
    }
    private System.Collections.IEnumerator DisableAfterDuration(ParticleSystem obj)
    {
        yield return new WaitForSeconds(obj.main.duration);
        ReturnObject(obj);
    }
}
