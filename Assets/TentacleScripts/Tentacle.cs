using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackInterval = 2f; // Sald�r�lar aras�ndaki s�re
    public float attackDuration = 0.5f; // Sald�r� aktif kalma s�resi
    public float attackRadius = 1.5f; // Tentacle sald�r� yar��ap�
    public LayerMask characterLayer;
    public float Damage = 5f;

    private bool isAttacking = false;
    private Collider attackCollider;
    private Animator attackAnimator;

    [Header("Effects")]
    public ParticleSystem attackEffect; // Sald�r� efekti (iste�e ba�l�)
    public AudioSource attackSound; // Sald�r� sesi (iste�e ba�l�)

    private void Start()
    {
        attackAnimator = GetComponent<Animator>();
        // Collider bile�enini al ve ba�ta devre d��� b�rak
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

        // Sald�r� efektini tetikle
        if (attackEffect != null)
        {
            attackEffect.Play();
        }

        // Sald�r� sesini �al
        if (attackSound != null)
        {
            attackSound.Play();
        }

        // Collider'� etkinle�tir
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
        // Oyuncuyla �arp��ma durumunu kontrol et
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
        // Tentacle sald�r� alan�n� g�rselle�tir
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
