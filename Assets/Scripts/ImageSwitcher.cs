using UnityEngine;

public class ImageSwitcher : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rightSprite;  // SpriteRenderer для нажатия вправо
    [SerializeField] private SpriteRenderer leftSprite;   // SpriteRenderer для нажатия влево
    
    private void Update()
    {
        // Нажатие вправо
        if (Input.GetKeyDown(KeyCode.D))
        {
            ShowRightSprite();
        }
        
        // Нажатие влево
        if (Input.GetKeyDown(KeyCode.A))
        {
            ShowLeftSprite();
        }
    }

    private void ShowRightSprite()
    {
        if (rightSprite != null)
            rightSprite.gameObject.SetActive(true);
        
        if (leftSprite != null)
            leftSprite.gameObject.SetActive(false);
    }

    private void ShowLeftSprite()
    {
        if (leftSprite != null)
            leftSprite.gameObject.SetActive(true);
        
        if (rightSprite != null)
            rightSprite.gameObject.SetActive(false);
    }
}
