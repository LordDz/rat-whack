using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
//using System.Runtime.InteropServices;

namespace Assets._Game
{
    public class CursorSprite : MonoBehaviour
    {
        [DllImport("user32.dll")]

        public static extern bool SetCursorPos(int X, int Y);

        public static CursorSprite instance;
        [SerializeField] GameObject parentCanvas;
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] Sprite sprite1;
        [SerializeField] Sprite sprite2;
        private float cooldown = 0;
        private float cooldownStopScratch = 0;

        [SerializeField] float delay = 0.1f;
        [SerializeField] float timeScratch = 0.5f;
        [SerializeField] float xOffset;
        [SerializeField] float zIndex;
        private bool isClosed = false;


        private bool isScratching = false;

        public bool IsMouseDown { get; private set; }


        void Awake()
        {
            instance = this;
            Cursor.visible = false;  // Use custom sprite instead of Cursor
            // Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
            spriteRenderer.sprite = sprite1;
        }

        public void StartScratching(bool state)
        {
            isScratching = state;

            if (state)
            {
                cooldownStopScratch = timeScratch;
            }
            else
            {
                spriteRenderer.sprite = sprite1;
            }
        }


        private void Update()
        {
            SetPos();
            Scratching();
            CheckMouseDown();
        }

        private void CheckMouseDown()
        {
            IsMouseDown = Input.GetMouseButtonDown(0);
        }

        private void SetPos()
        {

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition, Camera.main,
            out Vector2 movePos);

            Vector3 pos = parentCanvas.transform.TransformPoint(movePos);

            //Set fake mouse Cursor
            transform.position = new Vector3(pos.x - xOffset, pos.y, zIndex);
        }

        private void Scratching()
        {
            if (isScratching)
            {
                cooldown -= Time.deltaTime;
                cooldownStopScratch -= Time.deltaTime;
                if (cooldown <= 0)
                {
                    if (isClosed)
                    {
                        spriteRenderer.sprite = sprite1;
                    }
                    else
                    {
                        spriteRenderer.sprite = sprite2;
                    }
                    isClosed = !isClosed;
                    cooldown = delay;
                }

                if (cooldownStopScratch <= 0)
                {
                    StartScratching(false);
                }
            }
        }

        public void MoveCursor(Vector3 ratPos, int dir)
        {
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(
            //    parentCanvas.transform as RectTransform,
            //    Input.mousePosition, Camera.main,
            //    out Vector2 movePos);

            var pos = Camera.main.WorldToScreenPoint(ratPos);


            SetCursorPos((int)pos.x + (1 * dir), (int)pos.y);
        }
    }
}