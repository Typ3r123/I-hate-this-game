using UnityEngine;
using UnityEngine.UI;

public class RotatableImage : MonoBehaviour
{
    private Image image;
    private Button button;
    private int currentValue = 1;  // 1 = вверх, 2 = вниз
    private bool isPointingUp = true;

    private void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        
        if (button != null)
            button.onClick.AddListener(RotateImage);
        
        // Рандомно поворачиваем изображение при старте
        RandomizeRotation();
    }

    private void RandomizeRotation()
    {
        bool shouldRotate = Random.value > 0.5f;  // 50% шанс
        
        if (shouldRotate)
        {
            // Поворачиваем на 180 градусов (вниз)
            image.transform.rotation = Quaternion.Euler(0, 0, 180);
            currentValue = 2;
            isPointingUp = false;
            Debug.Log("Стрелка смотрит вниз. Значение: " + currentValue);
        }
        else
        {
            // Оставляем в исходном положении (вверх)
            image.transform.rotation = Quaternion.Euler(0, 0, 0);
            currentValue = 1;
            isPointingUp = true;
            Debug.Log("Стрелка смотрит вверх. Значение: " + currentValue);
        }
    }

    private void RotateImage()
    {
        if (isPointingUp)
        {
            // Поворачиваем на 180 градусов (вниз)
            image.transform.rotation = Quaternion.Euler(0, 0, 180);
            currentValue = 2;
            isPointingUp = false;
            Debug.Log("Стрелка смотрит вниз. Значение: " + currentValue);
        }
        else
        {
            // Возвращаем в исходное положение (вверх)
            image.transform.rotation = Quaternion.Euler(0, 0, 0);
            currentValue = 1;
            isPointingUp = true;
            Debug.Log("Стрелка смотрит вверх. Значение: " + currentValue);
        }
    }

    public int GetCurrentValue()
    {
        return currentValue;
    }

    private void OnDestroy()
    {
        if (button != null)
            button.onClick.RemoveListener(RotateImage);
    }
}
