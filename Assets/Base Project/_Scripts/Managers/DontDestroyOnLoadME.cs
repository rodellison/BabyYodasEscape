using UnityEngine;

namespace Base_Project._Scripts.Managers
{
    public class DontDestroyOnLoadME : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

    }
}
