using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private bool attacking = false;

    private float attackTimer = 0f;
    private float attackCooldown = 0.3f;

    private CharacterController controller;

    public Collider attackTriggerGround;
    public Collider attackTriggerAir;
    public Animator animator_ground;
    public Animator animator_air;

    void Start() {

    }

    void Awake()
    {
        attackTriggerGround.enabled = false;
        attackTriggerAir.enabled = false;
        controller = GetComponent<CharacterController>();
    }

    void Update()
        {
        if (Input.GetKeyDown("w") && !attacking && controller.isGrounded)//Change attack key input here
            {
            animator_ground.SetTrigger("Attack_Ground");

            attacking = true;
            attackTimer = attackCooldown;

            attackTriggerGround.enabled = true;
            }
        else if (Input.GetKeyDown("w") && !attacking && !controller.isGrounded)//Change attack key input here
        {
            animator_air.SetTrigger("Attack_Air");

            attacking = true;
            attackTimer = attackCooldown;

            attackTriggerAir.enabled = true;
        }
        if (attacking)
            {
                if(attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                attackTriggerGround.enabled = false;
                attackTriggerAir.enabled = false;
            }
         }
    }
}