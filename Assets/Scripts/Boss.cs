using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

public class Boss : MonoBehaviour {

    // public float idleDelay = 1.0f;
    // public bool despawning = true;
    // public float spawnDelay = 1.0f;
    // private float lastRecordedTime = -1.0f;

    public AudioClip wormSpawnClip;

    private const int DAMAGE = 5;

    public float minYPos = -9.0f;
    public float maxYPos = -0.9f;

    public float emergeDuration = 5.0f;

    public float minEmergeFrequency = 10.0f;
    public float maxEmergeFrequency = 30.0f;

    public float digDelay = 1.0f;

    private float lastEmerge = 0.0f;
    private float nextEmerge = 0.0f;

    private Animator animator;
    private Collider2D collider;
    private Character player;
    private SpriteRenderer sprite;
    private bool emerged = false;


    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        player = GameObject.FindWithTag("Player").GetComponent<Character>();
        sprite = GetComponent<SpriteRenderer>();

        collider.enabled = false;

        nextEmerge = Time.time + Random.Range(minEmergeFrequency, maxEmergeFrequency);
    }

	// Update is called once per frame
	void Update () {
        if (Time.time > nextEmerge && !emerged) {
            emerged = true;
            transform.position = new Vector3( player.transform.position.x, Mathf.Clamp(player.transform.position.y, minYPos, maxYPos), 0.0f);
            Dig();
            Invoke("Emerge", digDelay);
        }
    }

    private void Dig() {
        sprite.sortingOrder = -1;
        AudioManager.Instance.OnBossEmerge();
        animator.SetTrigger("Dig");
    }

    private void Emerge() {
        LookAtPlayer();
		GameObject.FindGameObjectWithTag ("CameraBigShake").GetComponent<PerlinShake>().PlayShake();
        sprite.sortingOrder = 3;
        animator.SetTrigger("Emerge");
        collider.enabled = true;
        Invoke("Submerge", emergeDuration);
    }

    private void LookAtPlayer() {
        if (player.transform.position.x > transform.position.x) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void Submerge() {
        animator.SetTrigger("Submerge");
        collider.enabled = false;
        nextEmerge = Time.time + Random.Range(minEmergeFrequency, maxEmergeFrequency);
        emerged = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponent<IDamageTaker>().TakeDamage(DAMAGE);
        }
    }
}
