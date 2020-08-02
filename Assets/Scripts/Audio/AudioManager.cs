using UnityEditor.VersionControl;
using UnityEngine;

namespace Audio {
    public class AudioManager : MonoBehaviour {

        [Header("Music")] 
        
        public AK.Wwise.Event PlayMusicEvent;
        
        public AK.Wwise.State GameplayState;

        public AK.Wwise.State MenuState;

        public AK.Wwise.State PlayerAliveState;

        public AK.Wwise.State PlayerDeadState;

        [Header("GUI")] 
        
        public AK.Wwise.Event MessageEvent;
        
        public AK.Wwise.Event BtnHoverEvent;

        public AK.Wwise.Event BtnClickEvent;

        [Header("Player")]
        
        public AK.Wwise.Event SwordAppearEvent;

        public AK.Wwise.Event SwordAttackEvent;

        public AK.Wwise.Event SwordHitEvent;

        public AK.Wwise.Event HealEvent;

        public AK.Wwise.Event PulseEvent;

        [Header("Enemies")] 
        
        public AK.Wwise.Event ScorpionDeathEvent;

        public AK.Wwise.Event SpiderDeathEvent;

        public AK.Wwise.Event CrawlerDeathEvent;

        public AK.Wwise.Event CrawlerLaserEvent;

        public AK.Wwise.Event BossEmergeEvent;

        private static AudioManager instance;

        public static AudioManager Instance => instance;

        private void Awake() {
            if (instance != null) {
                Destroy(gameObject);
                return;
            }

            instance = this;

            PlayMusicEvent.Post(gameObject);

            DontDestroyOnLoad(this);
        }

        public void SetMainMenuState() {
            PlayerAliveState.SetValue();
            MenuState.SetValue();
            MessageEvent.Post(gameObject);
        }

        public void SetGameState() {
            PlayerAliveState.SetValue();
            GameplayState.SetValue();
        }

        public void SetGameOverState() {
            PlayerDeadState.SetValue();
        }

        public void OnButtonHover() { BtnHoverEvent.Post(gameObject); }

        public void OnButtonPress() { BtnClickEvent.Post(gameObject); }

        public void OnSwordAppear() { SwordAppearEvent.Post(gameObject); }

        public void OnSwordAttack() { SwordAttackEvent.Post(gameObject); }

        public void OnSwordHit() { SwordHitEvent.Post(gameObject); }

        public void OnPlayerPulse() { PulseEvent.Post(gameObject); }

        public void OnHeal() { HealEvent.Post(gameObject); }

        public void OnBossEmerge() { BossEmergeEvent.Post(gameObject); }

        public void OnCrawlerLaser() { CrawlerLaserEvent.Post(gameObject); }

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