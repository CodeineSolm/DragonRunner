using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Sprite[] _bigClouds;
    [SerializeField] private Sprite[] _smallClouds;
    [SerializeField] private Sprite[] _mountains;
    [SerializeField] private Sprite[] _backgrounds;
    [SerializeField] private SpriteRenderer _bigCloud;
    [SerializeField] private SpriteRenderer _smallCloud;
    [SerializeField] private SpriteRenderer _mountain;
    [SerializeField] private SpriteRenderer _background;

    private void Start()
    {
        _bigCloud.sprite = _bigClouds[BackgroundController.States.BackgroundIndex];
        _smallCloud.sprite = _smallClouds[BackgroundController.States.BackgroundIndex];
        _mountain.sprite = _mountains[BackgroundController.States.BackgroundIndex];
        _background.sprite = _backgrounds[BackgroundController.States.BackgroundIndex];
    }
}
