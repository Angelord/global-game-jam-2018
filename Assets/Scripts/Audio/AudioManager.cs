using UnityEngine;
using UnityEngine.Serialization;

namespace Audio {
    public class AudioManager : MonoBehaviour {

        public AK.Wwise.Event BtnHoverEvent;
    
        public AK.Wwise.Event BtnClickEvent;

        public AK.Wwise.Event SwordLaunchEvent;

        public AK.Wwise.Event SwordHitEvent;

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

        public void OnButtonHover() {
            BtnHoverEvent.Post(gameObject);
        }

        public void OnButtonPress() {
            BtnClickEvent.Post(gameObject);
        }

        public void OnSwordLaunch() {
            SwordLaunchEvent.Post(gameObject);
        }

        public void OnSwordHit() {
            SwordHitEvent.Post(gameObject);
        }
    }
}