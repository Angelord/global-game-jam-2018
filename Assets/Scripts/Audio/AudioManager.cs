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
        
        public AK.Wwise.Event CrawlerDeathEvent;

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

        public void OnCreatureDeath(Vector2 position, Enemy.CreatureType type) {

            if (!CreatureIsVisible(position)) {
                return;
            }
            
            if (type == Enemy.CreatureType.Scorpion) {
                ScorpionDeathEvent.Post(gameObject);
            }
            else if (type == Enemy.CreatureType.Spider) {
                SpiderDeathEvent.Post(gameObject);
            }
            else if (type == Enemy.CreatureType.Crawler) {
                Debug.Log("Crawler death");
                CrawlerDeathEvent.Post(gameObject);
            }
        }

        private bool CreatureIsVisible(Vector2 position) {
            Camera mainCamera = Camera.main;
            Vector2 viewportPoint = mainCamera.WorldToViewportPoint(position);
            return viewportPoint.x > -0.05f && viewportPoint.x < 1.05f && 
                   viewportPoint.y > -0.5f && viewportPoint.y < 1.05f;
        }
    }
}