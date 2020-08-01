using UnityEngine;

namespace Audio {
    public class AudioManager : MonoBehaviour {

        public AK.Wwise.Event BtnHoverEvent;
    
        public AK.Wwise.Event BtnClickEvent;

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
    }
}