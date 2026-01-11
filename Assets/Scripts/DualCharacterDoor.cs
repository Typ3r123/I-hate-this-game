using UnityEngine;

public class DualCharacterDoor : MonoBehaviour
{
    [Header("Персонажи")]
    public Transform characterA;  // Основной персонаж
    public Transform characterB;  // Копия персонажа
    
    [Header("Дверь")]
    public Door door;  // Ссылка на скрипт Door
    
    private bool doorOpened = false;

    private void Update()
    {
        // Проверяем, ближе ли персонаж А к двери, чем персонаж Б
        if (characterA != null && characterB != null && door != null)
        {
            float distanceA = Vector2.Distance(characterA.position, door.transform.position);
            float distanceB = Vector2.Distance(characterB.position, door.transform.position);
            
            // Если персонаж А ближе и дверь ещё не открыта
            if (distanceA < distanceB && !doorOpened)
            {
                door.Open();
                doorOpened = true;
            }
            
            // Если персонаж Б ближе и дверь открыта, закрываем
            if (distanceB < distanceA && doorOpened)
            {
                door.Close();
                doorOpened = false;
            }
        }
    }
}
