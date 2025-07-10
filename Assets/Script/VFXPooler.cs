using UnityEngine;

public class VFXPooler : BasePooler<ParticleSystem>
{
    public static VFXPooler Instance { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    protected override void OnSpawn(ParticleSystem obj)
    {
        obj.Play();
        StartCoroutine(DisableAfterDuration(obj));
    }
    private System.Collections.IEnumerator DisableAfterDuration(ParticleSystem obj)
    {
        yield return new WaitForSeconds(obj.main.duration);
        ReturnObject(obj);
    }
}
