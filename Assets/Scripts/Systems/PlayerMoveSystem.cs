using Base_Project._Scripts.GameData;
using Base_Project._Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems
{
    public class PlayerMoveSystem : MonoBehaviour, InputActionControls.IGameplayActions
    {
        public FloatVariable PlayerMoveSpeed;
        public FloatVariable maxPlayerHorizontalDistance;
        public float MaxZRotation = 20f;
       public Vector3 currentEulerAngles = Vector3.zero;

        // Start is called before the first frame update
        public InputActionControls playerControls;

        private Vector3 inputValue = Vector3.zero;

        private void OnEnable()
        {
            if (playerControls == null)
                playerControls = new InputActionControls();

            playerControls.Gameplay.SetCallbacks(this);
            playerControls.Enable();
        }
 
        // Update is called once per frame
        void Update()
        {
            var deltaX = inputValue.x * PlayerMoveSpeed.Value * Time.deltaTime;
            float currentYPos = transform.position.y;
            Vector3 tempRotation = currentEulerAngles;
         
            if ((inputValue.x > 0.1f || inputValue.x < -0.1f) &&
                (transform.position.x + deltaX < maxPlayerHorizontalDistance.Value &&
                 transform.position.x + deltaX > -maxPlayerHorizontalDistance.Value))
            {
                //Important to translate this item in WORLD space, not Local Space, otherwise rotations get wonky
                transform.Translate(deltaX, 0f, 0f, Space.World);

                tempRotation += new Vector3(0, 0, inputValue.x) * Time.deltaTime * 50f;
                if (tempRotation.z <= MaxZRotation && tempRotation.z >= -MaxZRotation)
                    currentEulerAngles = tempRotation;
            }
            else
            {
                currentEulerAngles = new Vector3(0, 0, Mathf.Lerp(currentEulerAngles.z, 0, Time.deltaTime * 5f));
            }

            transform.eulerAngles = currentEulerAngles;

            if (inputValue.z > 0.1f || inputValue.z < -0.1f)
            {
                var deltaPosition = inputValue.z * PlayerMoveSpeed.Value * 0.5f * Time.deltaTime;
                var newTransformPosition = transform.position.y + deltaPosition;
                //This is to make sure we keep the player in Vertical zone where we need..
                if (newTransformPosition > 20f || newTransformPosition < 4f)
                {
                    return;
                }

                transform.Translate(0f, deltaPosition, 0f);
            }
            else
            {
                //This ensures the XWing doesnt float up when doing things like Euler rotations..
                Vector3 tempPos = transform.position;
                tempPos.y = currentYPos;
                transform.position = tempPos;
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 input2D;
            input2D = context.ReadValue<Vector2>();
            inputValue.x = input2D.x;
            inputValue.z = input2D.y;
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            //Nothing to do for this in this script..Shooting is handled in the PlayerShootSystem script
        }
    }
}