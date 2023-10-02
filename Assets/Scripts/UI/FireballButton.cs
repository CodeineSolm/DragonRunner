using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FireballButton : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private Image _image;

    public event UnityAction FireballButtonClicked;

    public void OnClick()
    {
        FireballButtonClicked?.Invoke();
    }

    private void OnEnable()
    {
        _player.Shooted += OnShooted;
        _player.Died += OnDied;
    }

    private void OnDisable()
    {
        _player.Shooted -= OnShooted;
        _player.Died -= OnDied;
    }

    private void OnShooted(float value)
    {
        StartCoroutine(Filling(0, 1, value, Fill));
    }

    private void OnDied()
    {
        Destroy(gameObject);
    }

    private void Fill(float value)
    {
        _image.fillAmount = value;
    }

    private IEnumerator Filling(float startValue, float endValue, float duration, UnityAction<float> lerpingEnd)
    {
        float elapsedTime = 0;
        float nextValue;

        while (elapsedTime < duration)
        {
            nextValue = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            _image.fillAmount = nextValue;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        lerpingEnd?.Invoke(endValue);
    }
}
