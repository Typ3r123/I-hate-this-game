using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickableBlock : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI blockText;
    private int currentNumber = 1;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
            button.onClick.AddListener(ChangeNumber);

        UpdateText();
    }

    private void ChangeNumber()
    {
        // Проверяем что это не пробел
        if (Input.GetKeyDown(KeyCode.Space))
            return;
        
        currentNumber++;
        if (currentNumber > 9)
            currentNumber = 1;
        UpdateText();
    }

    private void UpdateText()
    {
        if (blockText != null)
            blockText.text = currentNumber.ToString();
    }

    // Метод для внешнего чтения
    public int GetCurrentNumber()
    {
        return currentNumber;
    }

    private void OnDestroy()
    {
        if (button != null)
            button.onClick.RemoveListener(ChangeNumber);
    }
}