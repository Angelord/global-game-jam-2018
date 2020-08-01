using UnityEngine;
using UnityEngine.UI;

namespace Audio {
    [RequireComponent(typeof(Button))]
    public class ButtonSound : MonoBehaviour {

        public void OnMouseOver() {
            AudioManager.Instance.OnButtonHover();
        }

        public void OnMouseDown() {
            AudioManager.Instance.OnButtonPress();
        }
    }
}
