using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private int attackDamage;
    [SerializeField] private int maxHealth;
    [SerializeField] private int attackSpeed;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask baseLayer;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Animator anim;
    [SerializeField] private NeedMoreBullets NMB;
    [SerializeField] private bool isPlayerSeen;
    int currentHealth;
    private float timer;
    private bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        enemyAttack();

    }


    void enemyAttack()
    {
        if (canAttack)
        {
            anim.SetTrigger("Attack");

            canAttack = false;
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > attackSpeed)
            {
                canAttack = true;
                timer = 0;
            }
        }

    }
    void bulletInsantiator()
    {
        Instantiate(NMB, attackPoint.position, attackPoint.rotation);
    }
}
