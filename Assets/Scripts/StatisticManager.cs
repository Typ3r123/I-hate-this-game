using UnityEngine;

public class StatisticManager : MonoBehaviour
{
    private const string RESTARTS_KEY = "Рестарты: ";
    private const string Death_KEY = "Смерти: ";
    
    public static void AddRestart()
    {
        int restarts = PlayerPrefs.GetInt(RESTARTS_KEY, 0) + 1;
        PlayerPrefs.SetInt(RESTARTS_KEY, restarts);
        PlayerPrefs.Save();
    }

    public static void AddDeath()
    {
        int restarts = PlayerPrefs.GetInt(Death_KEY, 0) + 1;
        PlayerPrefs.SetInt(Death_KEY, restarts);
        PlayerPrefs.Save();
    }

    public static int GetRestarts()
    {
        return PlayerPrefs.GetInt(RESTARTS_KEY, 0);
    }

    public static int GetDeath()
    {
        return PlayerPrefs.GetInt(Death_KEY, 0);
    }
}
