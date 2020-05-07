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
        Quaternion rotationMin = Quaternion.Euler(new Vector3(0f, 0f, -20f));
        Quaternion rotationMax = Quaternion.Euler(new Vector3(0f, 0f, 20f));

        // Start is called before the first frame update
        public InputActionControls playerControls;

        private Vector3 inputValue = Vector3.zero;

        private void OnEnable()
        {
            playerControls = new InputActionControls();
            playerControls.Gameplay.SetCallbacks(this);
            playerControls.Enable();
        }

        // Update is called once per frame
        //TODO: Would be nice to have some Lerping Z Rotate in the direction we're moving
        void Update()
        {
            var deltaX = inputValue.x * PlayerMoveSpeed.Value * Time.deltaTime;
            float currentYPos = transform.position.y;
            Quaternion currentRotation = transform.rotation;

            if (inputValue.x != 0 &&
                (transform.position.x + deltaX < maxPlayerHorizontalDistance.Value &&
                 transform.position.x + deltaX > -maxPlayerHorizontalDistance.Value))
            {
                transform.Translate(deltaX, 0f, 0f);

                transform.rotation = (inputValue.x > 0)
                    ? Quaternion.Lerp(currentRotation, rotationMax, Time.deltaTime * 5f)
                    : Quaternion.Lerp(currentRotation, rotationMin, Time.deltaTime * 5f);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(currentRotation, Quaternion.identity, Time.deltaTime * 5f);
            }

            if (inputValue.z != 0)
            {
                var deltaPosition = inputValue.z * PlayerMoveSpeed.Value * 0.5f * Time.deltaTime;
                var newTransformPosition = transform.position.y + deltaPosition;
                //        Debug.Log(newTransformPosition);
                //This is to make sure we keep the player in Vertical zone where we need..
                if (newTransformPosition > 20f || newTransformPosition < 4f)
                    return;

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
            Vector2 input2D = context.ReadValue<Vector2>();
            inputValue.x = input2D.x;
            inputValue.z = input2D.y;

            //      Debug.Log(" Move called");
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            //Nothing to do for this in this script..Shooting is handled in the PlayerShootSystem script
        }
    }
}