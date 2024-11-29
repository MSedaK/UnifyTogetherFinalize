using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackInterval = 2f; // Saldýrýlar arasýndaki süre
    public float attackDuration = 0.5f; // Saldýrý aktif kalma süresi
    public float attackRadius = 1.5f; // Tentacle saldýrý yarýçapý
    public LayerMask characterLayer;
    public float Damage = 5f;

    private bool isAttacking = false;
    private Collider attackCollider;
    private Animator attackAnimator;

    [Header("Effects")]
    public ParticleSystem attackEffect; // Saldýrý efekti (isteðe baðlý)
    public AudioSource attackSound; // Saldýrý sesi (isteðe baðlý)

    private void Start()
    {
        attackAnimator = GetComponent<Animator>();
        // Collider bileþenini al ve baþta devre dýþý býrak
        attackCollider = GetComponent<Collider>();
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }



    }
    public void AttackCommand()
    {
        attackAnimator.SetTrigger("Attack");
        Debug.Log("Attack");
    }

    public void Attack()
    {
        if (isAttacking) return;

        isAttacking = true;

        // Saldýrý efektini tetikle
        if (attackEffect != null)
        {
            attackEffect.Play();
        }

        // Saldýrý sesini çal
        if (attackSound != null)
        {
            attackSound.Play();
        }

        // Collider'ý etkinleþtir
        if (attackCollider != null)
        {
            attackCollider.enabled = true;
        }
    }
    public void EndAttack()
    {
        isAttacking=false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isAttacking) return;
        // Oyuncuyla çarpýþma durumunu kontrol et
        if (other.CompareTag("Player"))
        {
            GiveDamage(other.gameObject);
        }
    }

    private void GiveDamage(GameObject player)
    {
        var data= player.GetComponent<PlayerData>();
        data.TakeDamage(Damage);


    }

    private void OnDrawGizmosSelected()
    {
        // Tentacle saldýrý alanýný görselleþtir
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
