using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerData : MonoBehaviour
{
    [SerializeField] float health = 20f;
    bool isDead = false;
    private void Dead()
    {
        isDead = true;
        Destroy(gameObject);
    }
    public void TakeDamage(float damage)
    {
        if (isDead) return;
        health -= damage;
        if (health < 0 ) Dead();
    }

   
   
}
