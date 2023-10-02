using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private TMP_Text _volumeValueText;

    private float _volumeValue = 0.5f;
    private const string _volumeValueString = "VolumeValue";

    public void VolumeSlide(float value)
    {
        _volumeValueText.text = value.ToString("0.0");
        
    }

    public void OnSaveButtonClick()
    {
        _volumeValue = _volumeSlider.value;
        PlayerPrefs.SetFloat(_volumeValueString, _volumeValue);
        LoadValues();
    }

    private void Start()
    {
        LoadValues();
    }

    private void LoadValues()
    {
        _volumeValue = PlayerPrefs.GetFloat(_volumeValueString);
        _volumeSlider.value = _volumeValue;
        AudioListener.volume = _volumeValue;
    }
}
