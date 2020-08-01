using Audio;
using UnityEngine;

public class Enemy : MonoBehaviour , IDamageTaker {

    public int health = 1;
    public int speed = 5;
	public float closeInRange = 4f;
	public float stopDistance = 0f;
    private Vector3 targetDirection;
	bool isFacingRight = true;
	public enemyType type = enemyType.DEFAULT;
	public CreatureType creatureType;
	private static int sineCount = 0;
    public GameObject powerUp;
    public GameObject[] buffs;
    public int dropChance = 10;

    public static void resetLevel() {
		Enemy.sineCount = 0;
	}

	private static int getMaxSineCount() {
		if (CombatManager.ScoreCounter < 6)
			return 0;
		if (CombatManager.ScoreCounter < 40)
			return 1;
		if (CombatManager.ScoreCounter < 60)
			return 2;
		return 3;
	}
	
	public enum CreatureType {
		Scorpion,
		Spider,
		Crawler
	}

	public enum enemyType
	{
		DEFAULT,
		SINE
	}

	void Start() {
        if (type == enemyType.SINE) {
			Enemy.sineCount += 1;
			if (sineCount > Enemy.getMaxSineCount()) {
				Destroy(transform.GetComponentInChildren<CoinSpawner> ());
				Die ();
			}
		}

		if (type == enemyType.DEFAULT) {
			speed = (int) Random.Range(25f, 45f);
		}
	}

    public void Initialize() { }

	// Update is called once per frame
	private void FixedUpdate () {
        if (!CombatManager.GameOver) {
            SetTartgetDirection();
            MoveTowardsTarget();
        }

		if (transform.position.y > 0)
			transform.position = new Vector3 (transform.position.x, 0, 0);
	}

	protected void Flip()
	{
		isFacingRight = !isFacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    private void SetTartgetDirection() {
        GameObject player = GameObject.Find("Character");
        if (player == null) return;

		if (player.transform.position.x > transform.position.x && isFacingRight)
			Flip ();
		else if (player.transform.position.x < transform.position.x && !isFacingRight)
			Flip ();

		// Dont move if respecting stopDistance
		if (Vector3.Distance (player.transform.position, transform.position) < stopDistance) {
			targetDirection = transform.position;
			return;
		}

        targetDirection = player.transform.position;
		if (Vector3.Distance (player.transform.position, transform.position) > closeInRange && Mathf.Abs(transform.position.x - player.transform.position.x) > 1f)
			targetDirection.y = transform.position.y;
    }

    private void MoveTowardsTarget() {
        Vector3 direction = targetDirection - transform.position;
        direction.Normalize();
        GetComponent<Rigidbody2D>().AddForce(direction * speed,ForceMode2D.Force);
    }

    public int Health {
       get { return health; }
    }

    public void TakeDamage(int value) {
        health -= value;
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
	    if (!gameObject.activeSelf) return;
			
	    if (Random.Range(1, 101) < dropChance) {
            int buffToUse = Random.Range(0, buffs.Length);
            PowerUp nextPowerUp = GameObject.Instantiate(powerUp, transform.position, Quaternion.identity).GetComponent<PowerUp>();
            nextPowerUp.buff = buffs[buffToUse];
            nextPowerUp.Initialize();
        }
        if (type == enemyType.SINE) {
			Enemy.sineCount -= 1;
        }

	    AudioManager.Instance.OnCreatureDeath(transform.position, creatureType);

        gameObject.SetActive(false);
    }
}
