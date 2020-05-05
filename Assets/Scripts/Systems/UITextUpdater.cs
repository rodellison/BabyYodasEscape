using Base_Project._Scripts.GameData;
using UnityEngine;
using UnityEngine.UI;

namespace Systems
{
    public class UITextUpdater : MonoBehaviour
    {
        public StringVariable TextToDisplay;
        public string prefaceString;

        private Text textControlToUpdate;
        // Start is called before the first frame update
        void Start()
        {
            textControlToUpdate = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            textControlToUpdate.text = prefaceString + TextToDisplay.Value;
        }
    }
}
