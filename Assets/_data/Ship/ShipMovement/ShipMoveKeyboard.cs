using UnityEngine;

public class ShipMoveKeyboard : ObjMovement
{
    [SerializeField] protected ShipCtrl shipCtrl;
    [SerializeField] protected float manaUse = 0.5f;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShipCtrl();
    }
    protected virtual void LoadShipCtrl()
    {
        if (this.shipCtrl != null) return;
        this.shipCtrl = transform.parent.GetComponent<ShipCtrl>();
        Debug.LogWarning(transform.name + ":LoadShipCtrl", gameObject);
    }
    protected virtual void Update()
    {
        this.GetInput(); // Lấy input từ bàn phím trước khi di chuyển
        this.MoveShip();
    }
    protected virtual void GetInput()
    {
        // 1. Xác định hướng di chuyển như cũ
        Vector3 inputDirection = InputManager.Instance.KeyboardWorldPos;
        SetDirection(inputDirection.normalized);

        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // 2. Mức công suất mặc định
        float targetEnginePower = 0.5f;
        // Màu cam sáng (bright orange)
        Color colorEnginePower = new Color(1f, 0.65f, 0f, 1f);

        // 3. Kiểm tra boost (Shift + di chuyển)
        bool isShiftHeld = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool hasDirection = Mathf.Abs(verticalInput) > 0f || Mathf.Abs(horizontalInput) > 0f;

        if (isShiftHeld && hasDirection)
        {
            SpeedBoost();
            targetEnginePower = 1.2f;
            // Màu xanh dương–trắng (bright cyan-white)
            colorEnginePower = new Color(0.4f, 1.5f, 1f, 1f);
        }
        else
        {
            SetValueBoost(1f);
        }

        // 4. Gửi màu và power vào model
        shipCtrl.ShipModelCtrl.SetEngineColor(colorEnginePower);
        shipCtrl.ShipModelCtrl.SetTargetEnginePower(targetEnginePower);
    }


    protected virtual void SpeedBoost()
    {
        // Nếu chưa có shipCtrl hoặc mana không đủ, thôi
        if (shipCtrl == null) return;
        if (shipCtrl.ShipAbilities.GetCurrentMana() * 0.15f > shipCtrl.ShipAbilities.GetCurrentMana()) return;

        // Tiêu mana (nếu muốn tiêu theo giây, nhân Time.deltaTime)
        shipCtrl.ShipAbilities.DeductMana(manaUse,1f);

        // Tăng tốc độ di chuyển 1.5 lần
        SetValueBoost(1.5f);
    }

}
