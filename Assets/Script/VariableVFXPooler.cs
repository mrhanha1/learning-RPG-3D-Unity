using UnityEngine;

public class vVFXPooler : BasePooler<ParticleSystem>
{
    public static vVFXPooler Instance { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
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
