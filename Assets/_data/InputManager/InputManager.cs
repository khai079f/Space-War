using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    protected static InputManager instance;
    public static InputManager Instance { get => instance; }

    [SerializeField] protected Vector3 mouseWorldPos;
    public Vector3 MouseWorldPos { get => mouseWorldPos; }

    [SerializeField] protected Vector3 keyboardWorldPos;
    public Vector3 KeyboardWorldPos { get => keyboardWorldPos; }

    [SerializeField] protected int onFiring=0;
    public int OnFiring { get => onFiring; }

    protected Vector4 direction ;
    public Vector4 Direction => direction;

    protected float[] lastTapTime = new float[4]; // Mảng lưu thời gian nhấn phím
    protected float doubleTapThreshold = 0.3f; // Thời gian tối đa giữa 2 lần nhấn để tính là double tap

    protected void Awake()
    {
        if (InputManager.instance != null) Debug.LogWarning("Only 1 InputManager allow to exist");
        InputManager.instance = this;    
    }

    private void Update()
    {
        this.GetMousePos();
        this.GetKeyboardPos();
        this.UpdateFiringState();
        this.GetDirectionByKeyDown();
    }
    protected virtual void GetMousePos()
    {
        this.mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    protected virtual void GetKeyboardPos()
    {
        // Lấy đầu vào từ bàn phím
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Tính toán hướng di chuyển dựa trên đầu vào
        this.keyboardWorldPos = new Vector3(horizontalInput, verticalInput, 0f);
    }

    protected virtual void UpdateFiringState()
    {
        // Kiểm tra trạng thái nhấn chuột trái
        if (Input.GetMouseButton(0)) // Chuột trái nhấn xuống
        {
            this.onFiring = 1;
        }
        else // Chuột trái không nhấn
        {
            this.onFiring = 0;
        }
        // Kiểm tra trạng thái nhấn chuột phải
    }

    /* protected virtual void GetDirectionByKeyDown()
     {
         this.direction.x = Input.GetKeyDown(KeyCode.A) ? 1 : 0;
         if(this.direction.x ==0) this.direction.x = Input.GetKeyDown(KeyCode.LeftArrow) ? 1 : 0;
         this.direction.y = Input.GetKeyDown(KeyCode.D) ? 1 : 0;
         if (this.direction.y == 0) this.direction.y = Input.GetKeyDown(KeyCode.RightArrow) ? 1 : 0;
         this.direction.z = Input.GetKeyDown(KeyCode.W) ? 1 : 0;
         if (this.direction.z == 0) this.direction.z = Input.GetKeyDown(KeyCode.UpArrow) ? 1 : 0;
         this.direction.w = Input.GetKeyDown(KeyCode.S) ? 1 : 0;
         if (this.direction.w == 0) this.direction.w = Input.GetKeyDown(KeyCode.DownArrow) ? 1 : 0;

         *//*if (this.direction.x == 1) Debug.Log("Left");
         if (this.direction.y == 1) Debug.Log("right");
         if (this.direction.z == 1) Debug.Log("up");
         if (this.direction.w == 1) Debug.Log("down");*//*
     }*/

    protected virtual void GetDirectionByKeyDown()
    {
        this.direction.x = CheckDoubleTap(KeyCode.A, KeyCode.LeftArrow, 0);
        this.direction.y = CheckDoubleTap(KeyCode.D, KeyCode.RightArrow, 1);
        this.direction.z = CheckDoubleTap(KeyCode.W, KeyCode.UpArrow, 2);
        this.direction.w = CheckDoubleTap(KeyCode.S, KeyCode.DownArrow, 3);
    }

    // Hàm phụ để kiểm tra double tap cho một hướng
    protected int CheckDoubleTap(KeyCode key1, KeyCode key2, int index)
    {
        float currentTime = Time.time;
        if (Input.GetKeyDown(key1) || Input.GetKeyDown(key2))
        {
            if (currentTime - lastTapTime[index] < doubleTapThreshold)
            {
                lastTapTime[index] = currentTime; // Cập nhật thời gian lần nhấn cuối
                return 1; // Double tap
            }
            lastTapTime[index] = currentTime;
        }
        return 0; // Không có double tap
    }
    // Phương thức để tính tọa độ biên của camera
    public virtual Vector2 GetCameraEdgePoint(Vector2 origin, Vector2 direction)
    {
        Camera cam = Camera.main;

        // Xác định tọa độ biên dựa trên hướng của tia
        Vector2 edgePoint = origin + direction * 100f; // Đặt một khoảng cách lớn để chắc chắn nó ra ngoài viền

        // Lấy 4 góc của camera trong không gian thế giới
        Vector2 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector2 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

        // Tính điểm va chạm với các biên của camera
        if (direction.x != 0)
        {
            float tX = (direction.x > 0) ? (topRight.x - origin.x) / direction.x : (bottomLeft.x - origin.x) / direction.x;
            edgePoint = origin + direction * tX;
        }
        if (direction.y != 0)
        {
            float tY = (direction.y > 0) ? (topRight.y - origin.y) / direction.y : (bottomLeft.y - origin.y) / direction.y;
            edgePoint = origin + direction * Mathf.Min(Vector2.Distance(origin, edgePoint), tY);
        }
        return edgePoint;
    }

}
