using System.Collections.Generic;
using UnityEngine;

public static class VFXSystem
{
    private static Dictionary<string, VFXPool> pools = new Dictionary<string, VFXPool>();
    private static Transform vfxContainer;

    public static void Initialize()
    {
        vfxContainer = new GameObject("VFXContainer").transform;
    }
    public static void Play(string vfxName, Vector3 position, Quaternion rotation)
    {
        if (!pools.TryGetValue(vfxName, out VFXPool pool))
        {
            Debug.LogWarning($"VFX Pool for {vfxName} not found.");
            return;
        }
        var vfx = pool.Get();
        vfx.transform.SetPositionAndRotation(position, rotation);
        vfx.gameObject.SetActive(true);
        if (pool.data.autoReturn)
        {
            ReturnAfterCompletion(vfxName, vfx);
        }
    }
    private static async void ReturnAfterCompletion(string vfxName, ParticleSystem vfx)
    {
        //await new WaitForCompletionAsync(vfx);
        pools[vfxName].Return(vfx);
    }
}
