using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjMovement : GameMonoBehaviour
{
    [Header("Ship Movement")]
    [SerializeField] public float speed = 5f; // Tốc độ di chuyển mặc định
    [SerializeField] protected Vector3 direction; // Hướng di chuyển
    [SerializeField] protected Transform objectMovement; // Transform của đối tượng di chuyển
    [SerializeField] protected float boostSpeed = 0f;  // Tốc độ tăng cường
    protected float currentBoost = 1f; // Giá trị boost ban đầu
    protected float boost = 1f; // Giá trị boost ban đầu

    // Phương pháp khởi tạo
    protected override void Start()
    {
        base.Start();
        this.SetObjectMovement();
    }

    // Phương pháp di chuyển chung
    public virtual void MoveShip()
    {
        this.BoostSpeed(this.boost); // Khởi tạo giá trị boost
        this.objectMovement.position += this.direction * this.boostSpeed * Time.deltaTime;
    }

    // Phương pháp thiết lập hướng di chuyển
    public virtual void SetDirection(Vector3 newDirection)
    {
        this.direction = newDirection;
    }

    // Phương pháp thiết lập Transform di chuyển
    protected virtual Transform SetObjectMovement()
    {
        return this.objectMovement = transform.parent;
    }

    // Tăng tốc độ di chuyển dần theo thời gian
    protected virtual float BoostSpeed(float boost)
    {
        boost = Mathf.Max(1, boost); // Đảm bảo boost không nhỏ hơn 1
        float targetBoostSpeed = this.speed * boost; // Xác định tốc độ mục tiêu

        if (this.boostSpeed > targetBoostSpeed + 1f) // Nếu vượt quá mục tiêu hơn 1
        {
            this.boostSpeed = Mathf.Lerp(this.boostSpeed, targetBoostSpeed, Time.deltaTime); // Giảm dần
        }
        else if (this.boostSpeed < targetBoostSpeed) // Nếu chưa đạt mục tiêu
        {
            this.currentBoost += boost * Time.deltaTime; // Tăng dần giá trị boost
            this.boostSpeed = Mathf.Min(this.speed + this.currentBoost, targetBoostSpeed); // Đảm bảo không vượt quá mục tiêu
        }
        else
        {
            this.currentBoost = 0; // Đặt lại giá trị boost khi đạt mục tiêu
            this.boostSpeed = targetBoostSpeed;
        }

        return this.boostSpeed;
    }
    public virtual void SetValueBoost(float boost)
    {
        this.boost = boost;
    }

}
