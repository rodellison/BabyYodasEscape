using UnityEngine;

namespace Systems
{
    public class ObjectRotate : MonoBehaviour
    {
        // Start is called before the first frame update
        public float RotationSpeed = 3f;
 
        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0f,Time.deltaTime * RotationSpeed, 0f);
        }
    }
}
