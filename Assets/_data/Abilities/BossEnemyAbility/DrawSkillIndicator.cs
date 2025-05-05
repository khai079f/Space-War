using UnityEngine;

public class DrawSkillIndicator : GameMonoBehaviour
{
    [SerializeField] protected SpriteRenderer sprite;
    private float targetValue = -1f;   // Giá trị mục tiêu
    private float currentValue = 0f;    // Giá trị hiện tại
    // Đặt chiều cao mong muốn
    public float targetHeight = 15f;

    protected override void Start()
    {
        base.Start();
        this.StateDrawSkillIndicator(false);
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpriteRenderer();
    }
    protected virtual void LoadSpriteRenderer()
    {
        if (this.sprite != null) return;
        this.sprite = GetComponent<SpriteRenderer>();
        Debug.LogWarning(transform.name + " LoadSpriteRenderer", gameObject);
    }
    public virtual void UpdateRange(float targetHeight,Vector3 Scalexyz)
    {
        transform.localScale = Scalexyz;
        // Kiểm tra nếu SpriteRenderer có tồn tại
        if (this.sprite != null)
        {
            // Tính chiều cao hiện tại của sprite
            float currentHeight = sprite.bounds.size.y;

            // Tính tỷ lệ thay đổi để đạt được targetHeight
            float scaleFactor = targetHeight / currentHeight;
        //    Debug.Log("targetHeight:"+ targetHeight+ " ,currentHeight:"+ currentHeight);
            // Thay đổi scale y của sprite mà không thay đổi x, z
            transform.localScale = new Vector3(transform.localScale.x, scaleFactor, transform.localScale.z);
        }
    }
    public virtual void ChangeHeightByTime(float elapsedTime, float timeChargeEnergy)
    {
        if (this.sprite.material == null) return;
        Material material = this.sprite.material;
        material.SetFloat("Height", -1);
        if (elapsedTime < timeChargeEnergy)
        {
            this.currentValue = Mathf.Lerp(0f, targetValue, elapsedTime / timeChargeEnergy);
            if (material.HasProperty("Height"))
            {
                material.SetFloat("Height", currentValue);
            }
        }
        else
        {
            // Đảm bảo giá trị là chính xác khi hoàn thành
            if (material.HasProperty("Height"))
            {
                material.SetFloat("Height", targetValue);
            }
        }
    }
    public virtual void StateDrawSkillIndicator(bool state)
    {
        if (state == this.transform.gameObject.activeSelf) return;
        this.transform.gameObject.SetActive(state);
    }

}
