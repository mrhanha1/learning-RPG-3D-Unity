using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class VFXPoolManager : MonoBehaviour
{
    public static VFXPoolManager Instance { get; private set; }
    [SerializeField] private List<VFXItem> vfxList = new List<VFXItem>();
    private Dictionary<string, VFXPooler> vfxPoolers = new Dictionary<string, VFXPooler>();
    [System.Serializable]
    public class VFXItem
    {
        public string name;
        public ParticleSystem prefab;
        public VFXPooler pooltype;
    }
    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        for (int i = 0; i < vfxList.Count; i++)
        {
            vfxList[i].pooltype = new VFXPooler(vfxList[i].prefab, 5);
            vfxPoolers[vfxList[i].name] = vfxList[i].pooltype;
            /*
            GameObject vfxObject = new GameObject(vfxList[i].name + " Pooler");
            vfxList[i].pooltype = vfxObject.AddComponent<VFXPooler>();
            vfxList[i].pooltype.Init(vfxList[i].prefab.GetComponent<ParticleSystem>(), 5);
            vfxPoolers[vfxList[i].name] = vfxList[i].pooltype;
            */
            Debug.Log($"VFX Pooler created for {vfxList[i].name} with prefab {vfxList[i].prefab.name}");
        }
    }
    public void SpawnVFX(string name, Vector3 position, Quaternion rotation)
    {

        if (vfxPoolers.TryGetValue(name, out VFXPooler pooler))
            {
                pooler.Spawn(position, rotation);
            }
        else
        {
            Debug.LogWarning($"VFX Pooler for {name} not found.");
        }
    }
}
