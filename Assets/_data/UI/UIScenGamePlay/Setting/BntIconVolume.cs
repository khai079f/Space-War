using UnityEngine;
using UnityEngine.UI;

public class BntIconVolume : BaseButton
{
    [SerializeField] protected Sprite imageOn;
    [SerializeField] protected Sprite imageOff;
    [SerializeField] protected Image imageIcon;
    [SerializeField] protected VolumeGame volumeGame;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadImageIcon();  
        this.LoadSpritesFromResources();
        this.LoadVolumeGame();
    }
    protected virtual void LoadVolumeGame()
    {
        if (this.volumeGame != null) return;
        this.volumeGame = GetComponentInParent<VolumeGame>();
        Debug.Log(transform.name + " - LoadVolumeGame", gameObject);
    }
    // Load the Image component if imageIcon is null
    protected virtual void LoadImageIcon()
    {
        if (this.imageIcon != null) return;
        this.imageIcon = GetComponent<Image>();
        Debug.Log(transform.name + " - LoadImageIcon", gameObject);
    }
    // Load các sprite imageOn và imageOff từ thư mục Resources/Icon nếu chưa được gán
    protected virtual void LoadSpritesFromResources()
    {
        // Thay "Icon" bằng tên thư mục bạn chứa sprite bên trong Resources (không cần "Resources/" vì Resources là thư mục đặc biệt)
        if (this.imageOn != null) return;
        Sprite[] spritesOn = Resources.LoadAll<Sprite>("UI/SettingUI/" + transform.name + "/" + transform.name + "On");
        this.imageOn = spritesOn[0];
        if (this.imageOn == null) Debug.LogWarning(transform.name + "UI/SettingUI/" + transform.name + "/" + transform.name + "On", gameObject);
        if (this.imageOff != null) return;
        Sprite[] spritesOff = Resources.LoadAll<Sprite>("UI/SettingUI/" + transform.name + "/" + transform.name + "Off");
        this.imageOff = spritesOff[0];
        if (imageOff == null) Debug.LogWarning(transform.name + "UI/SettingUI/" + transform.name + "/" + transform.name + "Off", gameObject);
        this.SetImageOn();
    }
    // Set the default sprite for imageIcon (using imageOn as default)

    public virtual void SetImageOn()
    {
        if (this.imageIcon != null && this.imageOn != null)
        {
            this.imageIcon.sprite = this.imageOn;
        }
        else
        {
            Debug.LogWarning(transform.name + " - Unable to set default sprite because either Image component or imageOn sprite is missing.", gameObject);
        }
    }
    public virtual void SetImageByValue(float value)
    {
        if (this.imageIcon != null && this.imageOff != null)
        {
            if(value > 0)
            {
                this.imageIcon.sprite = this.imageOn;
            }
            else
            {
                this.imageIcon.sprite = this.imageOff;
            }
        }
        else
        {
            Debug.LogWarning(transform.name + " - Unable to set default sprite because either Image component or imageOff sprite is missing.", gameObject);
        }
    }

    protected override void OnClick()
    {
        // Example: Toggle between imageOn and imageOff on click
        if (this.imageIcon == null) return;
        this.imageIcon.sprite = (this.imageIcon.sprite == imageOn) ? imageOff : imageOn;
        if (this.imageIcon.sprite == imageOn)
        {
            this.volumeGame.SetValueForSliderVolume(this.volumeGame.GetcurrentVolume());
        }
        else
        {
            this.volumeGame.MuteVolume();
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        this.SetImageByValue(this.volumeGame.GetcurrentVolume());
    }

}
