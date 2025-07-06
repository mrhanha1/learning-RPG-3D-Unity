using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [Header("Player Attack Settings")]
    [SerializeField] private ParticleSystem hitVFX;
    [SerializeField] private Collider weapCollider;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackDuration = 0.5f;

    private Animator animator;
    private bool canAttack = true;
    private Queue<ParticleSystem> vfxPool = new Queue<ParticleSystem>();
    private int poolSize = 5;
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
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()//?????
    {
        Debug.Log("Attack performed.");
        canAttack = false;
        animator.SetBool("isAttacking", true);
        weapCollider.enabled = true;

        yield return new WaitForSeconds(attackDuration);//?????
        weapCollider.enabled = false;
        animator.SetBool("isAttacking", false);

        yield return new WaitForSeconds(attackCooldown - attackDuration);//?????
        canAttack = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && weapCollider.enabled)
        {
            if (vfxPool.Count > 0)
            {
                ParticleSystem vfx = vfxPool.Dequeue(); // get vfx from pool
                vfx.gameObject.SetActive(true); // set active vfx
                vfx.transform.position = other.ClosestPoint(weapCollider.transform.position); // put vfx closest to the hit point
                vfx.Play();
                StartCoroutine(RetunToPool(vfx, vfx.main.duration)); // return vfx to pool after duration
            }
        }
    }
    private IEnumerator RetunToPool(ParticleSystem vfx, float delay)
    {
        yield return new WaitForSeconds(delay);
        vfx.gameObject.SetActive(false);
        vfxPool.Enqueue(vfx);
    }
}
