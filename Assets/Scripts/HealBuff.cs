using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

public class HealBuff : Buff {
    public int healValue = 1;

    protected override void Apply() {
        
        character.health += healValue;
        
        AudioManager.Instance.OnHeal();
    }

}
