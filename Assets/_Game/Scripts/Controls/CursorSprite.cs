using UnityEngine;

namespace Assets._Game
{
    public class CursorSprite : MonoBehaviour
    {
        [SerializeField] Texture2D cursorTexture;
        void Awake()
        {
            Cursor.visible = true;
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        }
    }
}