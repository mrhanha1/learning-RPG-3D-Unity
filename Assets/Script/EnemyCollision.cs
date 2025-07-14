using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private Collider enemyCollider;
    private VFXPooling hitVFXPool;
    private void Awake()
    {
        enemyCollider = GetComponent<Collider>();
        hitVFXPool = GetComponent<VFXPooling>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log($"Enemy {gameObject.name} collided with: {collision.gameObject.name}, in layer {collision.gameObject.layer}");
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Weapon") && enemyCollider.enabled)
        {
            ContactPoint[] contactPoints = collision.contacts;
            Debug.Log($"Enemy {gameObject.name} hit by weapon: {collision.gameObject.name}");
            foreach (var point in contactPoints)
            {
                hitVFXPool.SpawnVFX(point.point, Quaternion.LookRotation(point.normal));
            }
        }
    }
}
