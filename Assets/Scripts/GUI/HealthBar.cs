﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    private List<Image> bars = new List<Image>();
    private Character player;

    private void Start() {
        foreach (Transform child in transform) {
            bars.Add(child.GetComponent<Image>());
        }

        player = GameObject.FindWithTag("Player").GetComponent<Character>();
    }

    private void Update() {
        for (int i = 0; i < bars.Count; i++) {
            if (i < player.health) {
                bars[i].enabled = true;;
            }
            else {
                bars[i].enabled = false;
            }
        }
    }
}