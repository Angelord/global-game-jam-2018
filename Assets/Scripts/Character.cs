﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour, IDamageTaker {
    public float shochWaveRange = 5f;
    public int health = 5;
    public float dashCoolDown = 1.0f;
    public float dashTime = 0.05f;
    public float finalDashCheck = -1.0f;
    public int invulnrableCounter = 0;
    public float swordSummonSpeed = 2.0f;
	public float swordMinRange = 2.0f;
	public float swordMaxHeight = 0f;
    public float acceleration = 5.0f;
    public float maxSpeed = 10.0f;
	public float fieldLeft, fieldRight;
	public float fieldUp, fieldDown;
	public Transform playerCenter;
    private Rigidbody2D rigidbody;
    private bool dashing = false;
    private bool isDead = false;
    

    public SwordGroup swordGroup;

    private Animator animator;
    private SpriteRenderer sprite;

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine (playerCenter.position - Vector3.right * swordMinRange, playerCenter.position + Vector3.right * swordMinRange);
		Gizmos.DrawLine (playerCenter.position - Vector3.up * swordMinRange, playerCenter.position + Vector3.up * swordMinRange);
	}

    void Start() {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (Input.GetButtonDown("Jump")) {
            if(finalDashCheck<0||Time.time>finalDashCheck+dashCoolDown){
                dashing = true;
            }
        }
        HandleMovement();
		ClampToPlayingField ();
        HandleAttack();
    }

	private void ClampToPlayingField() {
		transform.position = new Vector3 (
			Mathf.Clamp(transform.position.x, fieldLeft, fieldRight),
			Mathf.Clamp(transform.position.y, fieldDown, fieldUp),
			transform.position.z
		);
	}

    private void HandleMovement() {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        Vector2 dirVector = new Vector2(xInput, yInput);
        if (dashing) {
            rigidbody.AddForce(dirVector * acceleration * acceleration, ForceMode2D.Force);
            dashing = false;
            finalDashCheck = Time.time;
        }
        else { rigidbody.AddForce(dirVector * acceleration, ForceMode2D.Force); }

        float velLength = rigidbody.velocity.magnitude;
        if (velLength > maxSpeed && !dashing) {
            rigidbody.AddForce(rigidbody.velocity.normalized * -1 * (velLength - maxSpeed));
        }

        if (dirVector.x > 0.1f) {
            sprite.flipX = true;
        }
        else if(dirVector.x < -0.1f) {
            sprite.flipX = false;
        }

        if (dirVector.magnitude > 0.02f) {
            SetAnimatorVariables(dirVector.normalized);
        }

    }

    private void SetAnimatorVariables(Vector2 dirVector) {
        float y = dirVector.y;
        float x = Mathf.Abs(dirVector.x);

        if (y > 0.8f && x < 0.225f) {
            animator.SetInteger("Direction", 0); // up
        }
        else if (y < 0.8f && y > 0.225f && x > 0.225f && x < 0.8f) {
            animator.SetInteger("Direction", 1);    //Up left
        }
        else if (y > -0.225f && y < 0.225f && x > 0.8f) {
            animator.SetInteger("Direction", 2);    //Left
        }
        else if (y > -0.8f && y < -0.225f && x > 0.225f && x < 0.8f) {
            animator.SetInteger("Direction", 3);    //Down left
        }
        else if (y < -0.8f && x < 0.225f) {
            animator.SetInteger("Direction", 4);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            int damage = collision.gameObject.GetComponent<IDamageTaker>().Health;
            TakeDamage(damage);
            collision.gameObject.GetComponent<IDamageTaker>().TakeDamage(damage);
            Debug.Log(health);
        }
    }

    private void HandleAttack() {

        
		if (Input.GetMouseButtonDown(0) && swordGroup.HasSwords) {
			Vector3 shootPosition = GetWorldPositionOnPlane(Input.mousePosition, 0);
			if( !swordsRangeCheck() )return;
			if (shootPosition.y > swordMaxHeight) {
				return;
			}
            shootPosition.z = 0;
            swordGroup.ShootSword(shootPosition);
            animator.ResetTrigger("Attack");
            animator.SetTrigger("Attack");
           // animator.SetBool("Attacking", true);
          //  Invoke("DesetAttacking", 0.14f);
        }
    }

	private Boolean swordsRangeCheck() {
		var distance = Vector3.Distance(GetWorldPositionOnPlane (Input.mousePosition, 0), playerCenter.position);
		return distance >= swordMinRange;
	}

    private void HandleShockWave() {
        if(Input.GetButtonDown("Fire1"))
    }

    public void TakeDamage(int value) {
        if(invulnrableCounter<=0){
            health -= value;
            if (health <= 0) {
                Die();
            }
        }
    }

    public int Health {
        get { return health; }
    }

    public bool IsDead {
        get { return isDead; }
    }

    private void Die() {
        gameObject.SetActive(false);
        isDead = true;
        CombatManager.GameOver = true;
    }

	Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z) {
		Ray ray = Camera.main.ScreenPointToRay(screenPosition);
		Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
		float distance;
		xy.Raycast(ray, out distance);
		return ray.GetPoint(distance);
	}
		
}
