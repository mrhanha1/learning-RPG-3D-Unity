using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitVFX;
    private VFXPooler vfxPooler;
    private Collider weaponCollider;
    private void Awake()
    {
        weaponCollider = GetComponent<Collider>();
        vfxPooler = VFXPooler.Instance;
    }
    public void EnableCollision()
    {
        if (weaponCollider != null){weaponCollider.enabled = true;}
    }
    public void DisableCollision()
    {
        if (weaponCollider != null) { weaponCollider.enabled = false; }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Weapon {gameObject.name} collided with: {collision.gameObject.name}, in layer {collision.gameObject.layer}");
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy") && weaponCollider.enabled)
        {
            ContactPoint[] contactPoints = collision.contacts;
            Debug.Log($"Weapon {gameObject.name} hit enemy: {collision.gameObject.name}");
            foreach (var point in contactPoints)
            {
                vfxPooler.GetObject(point.point, Quaternion.LookRotation(point.normal));
            }
        }
    }
    private void Update()
    {
        
    }
}

