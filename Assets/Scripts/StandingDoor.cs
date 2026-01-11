using UnityEngine;
using TMPro;

public class StandingDoor : MonoBehaviour
{
    [Header("Персонаж")]
    public Transform player;

    [Header("Дверь")]
    public Door door;

    [Header("Текст")]
    public TextMeshProUGUI statusText;

    [Header("Основные параметры")]
    public float standingTime = 10f;           // Время стояния для открытия двери
    public float firstMoveDistance = 5f;       // Первое расстояние
    public float secondMoveDistance = 5f;      // Второе расстояние

    [Header("Сообщение при долгом стоянии")]
    public string customMessage = "Еще чуть-чуть"; // ← твой текст
    public float customMessageDelay = 15f;     // через сколько секунд показать

    private Vector3 lastPosition;
    private float standingTimer = 0f;
    private float totalMovedDistance = 0f;
    private bool doorLocked = false;
    private bool customMessageShown = false;

    private void Start()
    {
        if (player != null)
            lastPosition = player.position;
    }

    private void Update()
    {
        if (player == null || door == null || doorLocked) return;

        float movedDistance = Vector3.Distance(player.position, lastPosition);
        totalMovedDistance += movedDistance;

        // Обновляем состояние и текст
        if (totalMovedDistance >= firstMoveDistance + secondMoveDistance)
        {
            doorLocked = true;
            if (statusText != null)
                statusText.text = "Попробуй сначала";
        }
        else if (totalMovedDistance >= firstMoveDistance)
        {
            if (statusText != null)
                statusText.text = "Не нервничай";
        }
        else
        {
            if (statusText != null && !customMessageShown)
                statusText.text = "Давай немного постоим";
        }

        // Таймер стояния (для открытия двери)
        if (movedDistance <= 0.01f && !doorLocked)
        {
            standingTimer += Time.deltaTime;

            // Открываем дверь
            if (standingTimer >= standingTime)
            {
                door.Open();
                if (statusText != null)
                    statusText.text = "Проходи не задерживайся";
                enabled = false;
            }

            // Показываем кастомное сообщение, если прошло достаточно времени
            if (!customMessageShown && standingTimer >= customMessageDelay)
            {
                customMessageShown = true;
                if (statusText != null)
                    statusText.text = customMessage;
            }
        }

        lastPosition = player.position;
    }
}