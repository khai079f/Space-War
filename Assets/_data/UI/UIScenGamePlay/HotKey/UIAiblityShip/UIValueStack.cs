using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIValueStack : UIAbilityHotKeyAbstract
{
    [SerializeField] protected Text textStack;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIText();
    }
    protected virtual void LoadUIText()
    {
        if (textStack != null) return;
        this.textStack = transform.GetComponent<Text>();
        Debug.LogWarning(transform.name + " LoadUIText ", gameObject);
    }
    private void FixedUpdate()
    {
        this.UpdateTextStack();
    }
    protected virtual void UpdateTextStack()
    {
        if (this.baseAbility == null) return;
        this.textStack.text = this.baseAbility.GetValueStackForUI().ToString();
    }
}
