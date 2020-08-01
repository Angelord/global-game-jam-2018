using UnityEngine;

namespace Audio {
    public class AudioManager : MonoBehaviour {

        [Header("GUI")]

        public AK.Wwise.Event BtnHoverEvent;
    
        public AK.Wwise.Event BtnClickEvent;

        [Header("Player")]

        public AK.Wwise.Event SwordAppearEvent;

        public AK.Wwise.Event SwordHitEvent;
        
        public AK.Wwise.Event HealEvent;

        [Header("Enemies")]
        
        public AK.Wwise.Event ScorpionDeathEvent;
        
        public AK.Wwise.Event SpiderDeathEvent;

        public AK.Wwise.Event WormDeathEvent;

        public AK.Wwise.Event WormLaserEvent;

        public AK.Wwise.Event WormEmergeEvent;

        private static AudioManager instance;

        public static AudioManager Instance => instance;

        private void Start() {
            if (instance != null) {
                Destroy(gameObject);
                return;
            }

            instance = this;
            
            DontDestroyOnLoad(this);
        }

        public void OnButtonHover() { BtnHoverEvent.Post(gameObject); }

        public void OnButtonPress() { BtnClickEvent.Post(gameObject); }

        public void OnSwordAppear() { SwordAppearEvent.Post(gameObject); }

        public void OnSwordHit() { SwordHitEvent.Post(gameObject); }

        public void OnHeal() { HealEvent.Post(gameObject); }
    }
}