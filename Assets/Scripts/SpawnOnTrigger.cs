using UnityEngine;

public class SpawnOnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn; // Префаб который спавнится
    
    private bool hasSpawned = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasSpawned) return;
        
        hasSpawned = true;
        
        // Спавним объект на позиции этого триггера
        if (prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        }
    }
}
