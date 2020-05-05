using Cinemachine;
using UnityEngine;

namespace Systems
{
    public class XWingImpulseGenerator : MonoBehaviour
    {
        // Start is called before the first frame update
        private CinemachineImpulseSource myImpulseSource;
        void Start()
        {
            myImpulseSource = GetComponent<CinemachineImpulseSource>();
        }

        // Update is called once per frame
        void Update()
        {
            myImpulseSource.GenerateImpulse();
        }
    }
}
