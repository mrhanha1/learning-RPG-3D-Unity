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
    //[SerializeField] private bool showGizmos = true;
    private Animator animator;
    private bool canAttack = true;
    void Start()
    {
        animator = GetComponent<Animator>();
        if (!animator  || !weapCollider) { Debug.LogError("Missing component for attack"); }
        weapCollider.enabled = false;
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
        //Debug.Log("Attack performed.");
        weapCollider.enabled = true;
        canAttack = false;
        animator.SetBool("isAttacking", true);

        yield return new WaitForSeconds(attackDuration);
        weapCollider.enabled = false;
        animator.SetBool("isAttacking", false);

        yield return new WaitForSeconds(attackCooldown - attackDuration);
        canAttack = true;
    }
}
