using UnityEngine;

namespace Assets._Game
{
    public class CameraMover : MonoBehaviour
    {
        private GameObject cameraMain;

        [SerializeField] float screenWidth;
        [SerializeField] float screenHeight;
        [SerializeField] float cameraSpeedX = 0.1f;
        [SerializeField] float cameraSpeedY = 0.1f;

        public float desiredX = 0;
        public float desiredY = 0;


        private void Awake()
        {
            cameraMain = Camera.main.gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            CameraMovement();
        }

        private void CameraMovement()
        {
            desiredX = Input.mousePosition.x / screenWidth;
            desiredY = Input.mousePosition.y / screenHeight;

            FixPos();
        }

        private void FixPos()
        {
            Vector3 pos = cameraMain.transform.position;

            float x = cameraSpeedX * Time.deltaTime;
            float y = cameraSpeedY * Time.deltaTime;

            //X
            if (pos.x < desiredX)
            {
                pos.x += x;

                if (pos.x > desiredX)
                {
                    pos.x = desiredX;
                }
            }
            else if (pos.x > desiredX)
            {
                pos.x -= x;

                if (pos.x < desiredX)
                {
                    pos.x = desiredX;
                }
            }

            //Y
            if (pos.y < desiredY)
            {
                pos.y += y;

                if (pos.y > desiredY)
                {
                    pos.y = desiredY;
                }
            }
            else if (pos.y > desiredY)
            {
                pos.y -= y;

                if (pos.y < desiredY)
                {
                    pos.y = desiredY;
                }
            }

            cameraMain.transform.position = pos;
        }
    }
}