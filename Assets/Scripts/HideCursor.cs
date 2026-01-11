using UnityEngine;

public class HideCursor : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
    }
    
    private void OnDestroy()
    {
        Cursor.visible = true;
    }
}
