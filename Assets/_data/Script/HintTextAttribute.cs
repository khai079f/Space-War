using UnityEngine;

public class HintTextAttribute : PropertyAttribute
{
    public string Hint { get; private set; }

    public HintTextAttribute(string hint)
    {
        Hint = hint;
    }
}
