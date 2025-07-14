using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
public class VFXPooling : MonoBehaviour
{
    [SerializeField] public ParticleSystem vfxPrefab;
    private ObjectPool<ParticleSystem> vfxPool;

    private void Awake()
    {
        if (vfxPrefab != null)
        {
            vfxPool = new ObjectPool<ParticleSystem>(
                () => Instantiate(vfxPrefab),
                vfx => vfx.gameObject.SetActive(true),
                vfx => vfx.gameObject.SetActive(false),
                vfx => Destroy(vfx.gameObject),
                false, 5, 15
            );
        }
    }
    public void SpawnVFX(Vector3 position, Quaternion rotation)
    {
        if (vfxPool != null)
        {
            ParticleSystem vfx = vfxPool.Get();
            vfx.transform.position = position;
            vfx.transform.rotation = rotation;
            vfx.Play();
            StartCoroutine(ReturnVFXAfterTime(vfx, vfx.main.duration));
        }
    }
    public void ReturnVFX(ParticleSystem vfx)
    {
        if (vfxPool != null && vfx != null)
        {
            vfxPool.Release(vfx);
        }
    }
    private IEnumerator ReturnVFXAfterTime(ParticleSystem vfx, float time)
    {
        yield return new WaitForSeconds(time);
        ReturnVFX(vfx);
    }
}
