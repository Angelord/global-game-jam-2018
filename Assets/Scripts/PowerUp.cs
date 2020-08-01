using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    public GameObject buff;

    public void Initialize() {
        Debug.Log("Called");
        GetComponent<SpriteRenderer>().sprite = buff.gameObject.GetComponent<SpriteRenderer>().sprite;
    } 

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            Character character = collision.GetComponent<Character>();
            Apply(character);
			character.TriggerHealFX ();
            
            Debug.Log("Collected");
            this.gameObject.SetActive(false);
        }
    }

    protected virtual void Apply(Character character) {
        Vector3 spawnPosition = transform.position;
        Instantiate(buff, spawnPosition, Quaternion.identity);
        buff.GetComponent<Buff>().Initialize(character);
    }
}
