using UnityEngine;

public class CameraWave : MonoBehaviour
{
    [SerializeField] private Shader warpShader; // Перетащи шейдер сюда
    [SerializeField] private float warpAmount = 0.1f; // Сила warping
    [SerializeField] private float warpSpeed = 2f; // Скорость warping
    [SerializeField] private float warpFrequency = 5f; // Частота волн
    [SerializeField] private bool enableEffect = true; // Включить эффект
    
    private Material warpMaterial;
    
    private void Start()
    {
        // Если шейдер не указан в инспекторе, ищем его автоматически
        if (warpShader == null)
        {
            warpShader = Shader.Find("Custom/ScreenWarp");
        }
        
        if (warpShader != null)
        {
            warpMaterial = new Material(warpShader);
        }
        else
        {
            Debug.LogError("Шейдер Custom/ScreenWarp не найден!");
        }
    }
    
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (!enableEffect || warpMaterial == null)
        {
            Graphics.Blit(source, destination);
            return;
        }
        
        // Передаем параметры в шейдер
        warpMaterial.SetFloat("_WarpAmount", warpAmount);
        warpMaterial.SetFloat("_WarpSpeed", warpSpeed);
        warpMaterial.SetFloat("_WarpFrequency", warpFrequency);
        
        // Применяем эффект
        Graphics.Blit(source, destination, warpMaterial);
    }
}

