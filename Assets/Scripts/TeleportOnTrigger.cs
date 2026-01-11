using UnityEngine;

public class TeleportOnTrigger : MonoBehaviour
{
    [SerializeField] private Transform linkedTeleportPoint; // Связанный триггер куда телепортировать
    [SerializeField] private GameObject[] objectsToDeactivate; // Объекты которые пропадают
    [SerializeField] private GameObject[] objectsToActivate; // Объекты которые появляются
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем что это игрок
        if (collision.CompareTag("Player"))
        {
            // Проверяем что это не тот же триггер что и последний
            if (PlayerTeleportCooldown.lastTeleportTrigger == this)
            {
                return; // Не телепортируем если это тот же триггер
            }
            
            // Проверяем прошла ли задержка
            if (Time.time - PlayerTeleportCooldown.lastTeleportTime >= PlayerTeleportCooldown.teleportCooldown)
            {
                if (linkedTeleportPoint != null)
                {
                    // Обновляем глобальное время последней телепортации
                    PlayerTeleportCooldown.lastTeleportTime = Time.time;
                    PlayerTeleportCooldown.lastTeleportTrigger = this;
                    
                    // Деактивируем объекты
                    foreach (GameObject obj in objectsToDeactivate)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(false);
                        }
                    }
                    
                    // Активируем объекты
                    foreach (GameObject obj in objectsToActivate)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(true);
                        }
                    }
                    
                    // Телепортируем игрока на позицию связанного триггера
                    collision.transform.position = linkedTeleportPoint.position;
                }
            }
        }
    }
}
