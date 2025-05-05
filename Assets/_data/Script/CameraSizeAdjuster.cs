using UnityEngine;

public class CameraSizeAdjuster : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; // Camera cần tùy chỉnh
    [SerializeField] private float targetSize = 5f; // Kích thước mục tiêu của camera

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Gán camera chính nếu chưa được gán trong Inspector
            if (mainCamera == null)
            {
                Debug.LogError("Main Camera not found!");
            }
        }

        AdjustCameraSize();
    }
    public void AdjustCameraSize()
    {
        if (mainCamera != null)
        {
            mainCamera.orthographicSize = targetSize;
        }
    }
}
