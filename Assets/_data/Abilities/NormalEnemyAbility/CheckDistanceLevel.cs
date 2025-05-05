using UnityEngine;

public class CheckDistanceLevel : GameMonoBehaviour
{
     protected float farDistance = 22f;  // Ngưỡng để phân biệt khoảng cách "xa"
     protected float safeDistance = 17f;  // Ngưỡng để phân biệt khoảng cách "an toàn"
     protected float attackDistance = 7f;
    [SerializeField] protected float distance ;
    public float Distance => distance;
    // Tính toán khoảng cách giữa đối tượng và người chơi
    protected virtual float GetDistanceToPlayer(Vector3 playerTransform,Vector3 objectTransform)
    {
        if (playerTransform == null || objectTransform == null)
        {
            Debug.LogWarning("Player or object transform is not set!");
            return float.MaxValue;  // Trả về một giá trị lớn nếu không tìm thấy player
        }

        return Vector3.Distance(objectTransform, playerTransform);
    }

    // Phân loại khoảng cách theo các cấp độ: xa, an toàn, gần
    public virtual LevelForDistance GetDistanceLevel(Transform playerTransform, Transform objectTransform)
    {
        this.distance = GetDistanceToPlayer(playerTransform.position, objectTransform.position);

        if (this.distance > farDistance) return LevelForDistance.Far;
        if (this.distance > safeDistance) return LevelForDistance.Safe;
        if (this.distance > attackDistance) return LevelForDistance.canAtk;
        return LevelForDistance.Near;
    }
    public virtual float GetDistance(Vector3 playerTransform, Vector3 objectTransform)
    {
        this.distance = GetDistanceToPlayer(playerTransform, objectTransform);
        return this.distance;
    }
    public enum LevelForDistance
    {
        Far,        // Khoảng cách xa
        Safe,       // Khoảng cách an toàn
        canAtk,     // Khoảng cách tấn công
        Near        // Khoảng cách gần
    }
}

