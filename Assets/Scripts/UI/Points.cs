using TMPro;
using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField] private TMP_Text _pointsCountText;
    [SerializeField] private Spawner _spawner;

    private int _pointsCount;

    private void OnEnable()
    {
        _spawner.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        _spawner.Died -= OnEnemyDied;
    }

    private void OnEnemyDied(int value)
    {
        _pointsCount += value;
        _pointsCountText.text = _pointsCount.ToString();
    }
}
