using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private UISoundPlayer _soundPlayer;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private PlayerController _player;

    private CanvasGroup _gameOverGroup;
    private float _alpha = 0;
    private bool _isVisible = false;

    private void OnEnable()
    {
        _player.Died += OnDied;
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _player.Died -= OnDied;
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    private void Start()
    {
        _gameOverGroup = GetComponent<CanvasGroup>();
        VisabilityChange(_isVisible, _alpha);
    }

    private void OnDied()
    {
        _isVisible = true;
        _alpha = 1;
        VisabilityChange(_isVisible, _alpha);
        Time.timeScale = 0;
    }

    private void OnRestartButtonClick()
    {
        _soundPlayer.OnButtonClickSoundEffect();
        _isVisible = false;
        _alpha = 0;
        VisabilityChange(_isVisible, _alpha);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    private void OnExitButtonClick()
    {
        _soundPlayer.OnButtonClickSoundEffect();
        SceneManager.LoadScene(0);
        BackgroundController.States.BackgroundIndex = 0;
    }

    private void VisabilityChange(bool isVisible, float alpha)
    {
        _gameOverGroup.alpha = alpha;
        _restartButton.gameObject.SetActive(isVisible);
        _exitButton.gameObject.SetActive(isVisible);
    }
}
