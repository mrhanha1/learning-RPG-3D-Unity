using UnityEngine;

[CreateAssetMenu(menuName = "VFX/VFX Data")]
public class VFXData : ScriptableObject
{
    public ParticleSystem prefab;
    public int initPoolSize = 5;
    public bool autoReturn = true;
}
