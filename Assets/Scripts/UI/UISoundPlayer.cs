using UnityEngine;

public class UISoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _buttonSoundEffectSource;

    public void OnButtonClickSoundEffect()
    {
        _buttonSoundEffectSource.Play();
    }
}
