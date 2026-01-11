using UnityEngine;

public class PlayerTeleportCooldown : MonoBehaviour
{
    public static float lastTeleportTime = 0f;
    public static float teleportCooldown = 0.1f;
    public static TeleportOnTrigger lastTeleportTrigger = null; // Последний триггер с которого телепортировались
}
