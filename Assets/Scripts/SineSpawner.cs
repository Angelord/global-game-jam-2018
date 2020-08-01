using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

public class SineSpawner : MonoBehaviour {

	// Use this for initialization
	public GameObject sine;
	public float activationDistance = 8f;
	private bool canShoot = true;
	private Transform playerTransform;

	void Start () {
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void enableShoot() {
		canShoot = true;
	}

	// Update is called once per frame
	void Update () {

        if (!CombatManager.GameOver && Vector3.Distance (transform.position, playerTransform.position) < activationDistance && canShoot) {
			canShoot = false;
			Invoke ("enableShoot", Random.Range (0.5f, 2.5f));
			if(Random.Range(1, 100) > 60) {
					var bonus_obj = GameObject.Instantiate (sine, transform);
					bonus_obj.transform.parent = null;
			}
			var obj = GameObject.Instantiate (sine, transform);
			AudioManager.Instance.OnCrawlerLaser();
			obj.transform.parent = null;
		}

	}
}
