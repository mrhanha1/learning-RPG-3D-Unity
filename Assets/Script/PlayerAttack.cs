using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [Header("Player Attack Settings")]
    [SerializeField] private ParticleSystem hitVFX;
    [SerializeField] private Collider weapCollider;
    [SerializeField] private float attackCooldown = .9f;
    [SerializeField] private float attackDuration = 0.5f;

    [Header("Debug")]
    [SerializeField] private bool showGizmos = true;
    private Animator animator;
    private bool canAttack = true;
    private Queue<ParticleSystem> vfxPool = new Queue<ParticleSystem>();
    private int poolSize = 5;
    private VFXPooler vfxPooler;
    void Start()
    {
        animator = GetComponent<Animator>();
        if (!animator || !hitVFX || !weapCollider) { Debug.LogError("Missing component for attack"); }
        weapCollider.enabled = false;
        for (int i=0; i < poolSize; i++)
        {
            ParticleSystem vfx = Instantiate(hitVFX);
            vfx.gameObject.SetActive(false);
            vfxPool.Enqueue(vfx);
        }
        vfxPooler = VFXPooler.Instance;
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        Debug.Log("Attack performed.");
        weapCollider.enabled = true;
        canAttack = false;
        animator.SetBool("isAttacking", true);

        yield return new WaitForSeconds(attackDuration);
        weapCollider.enabled = false;
        animator.SetBool("isAttacking", false);

        yield return new WaitForSeconds(attackCooldown - attackDuration);
        canAttack = true;
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{gameObject.name}Collision with {collision.gameObject.name} detected, Weapon collider enabled: {weapCollider.enabled}, layer: {collision.gameObject.layer}");
        if ( collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy") && weapCollider.enabled)
        {
            Debug.Log($"Hit {collision.gameObject.name} with weapon name: {weapCollider.name}");
            ContactPoint contact = collision.contacts[0]; // get the first contact point
            //vfxPooler.OnSpawn(vfxPooler.GetObject(contact.point, Quaternion.LookRotation(contact.normal))); // spawn vfx at contact point
            vfxPooler.GetObject(contact.point, Quaternion.LookRotation(contact.normal)).Play(); // spawn vfx at contact point
            Debug.Log($"Spawned VFX at {contact.point} with rotation {Quaternion.LookRotation(contact.normal)}");
            */
            /*
        if (vfxPool.Count > 0)
        {
            ContactPoint contact = collision.contacts[0]; // get the first contact point
            ParticleSystem vfx = vfxPool.Dequeue(); // get vfx from pool
            vfx.gameObject.SetActive(true); // set active vfx
            vfx.transform.position = contact.point; // put vfx at the contact point
            vfx.transform.rotation = Quaternion.LookRotation(contact.normal); // orient vfx to the surface normal
            vfx.Play();
            StartCoroutine(RetunToPool(vfx, vfx.main.duration)); // return vfx to pool after duration
        }

        }
    }
            */
    private IEnumerator RetunToPool(ParticleSystem vfx, float delay)
    {
        yield return new WaitForSeconds(delay);
        vfx.gameObject.SetActive(false);
        vfxPool.Enqueue(vfx);
    }
}
