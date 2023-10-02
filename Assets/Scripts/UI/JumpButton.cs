using UnityEngine;
using UnityEngine.Events;

public class JumpButton : MonoBehaviour
{
    public event UnityAction JumpButtonClicked;

    public void OnClick()
    {
        JumpButtonClicked?.Invoke();
    }
}
