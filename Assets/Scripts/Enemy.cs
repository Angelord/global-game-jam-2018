﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour , IDamageTaker {

    public int health = 1;
    public int speed = 5;
    private Vector3 targetDirection;

    public void Initialize() { }
	
	// Update is called once per frame
	void Update () {
        SetTartgetDirection();
        MoveTowardsTarget();
	}

    private void SetTartgetDirection() {
        GameObject player = GameObject.Find("Character");
        if (player == null) return;
        targetDirection = player.transform.position;
    }

    private void MoveTowardsTarget() {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetDirection, step);
    }

    public int Health() {
        return health;
    }

    public void TakeDamage(int value) {
        health -= value;
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        gameObject.SetActive(false);
    }
}
