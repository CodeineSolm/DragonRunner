using UnityEngine;
using TMPro;

public class OptionsController : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _backgroundDropdown;

    private void OnEnable()
    {
        _backgroundDropdown.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _backgroundDropdown.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(int index)
    {
        switch (index)
        {
            case 0: //Day
                BackgroundController.States.BackgroundIndex = 0;
                break;
            case 1: //Night
                BackgroundController.States.BackgroundIndex = 1;
                break;
            case 2: //Sunset
                BackgroundController.States.BackgroundIndex = 2;
                break;
        }
    }
}
