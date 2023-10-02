using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    protected List<Enemy> _pool = new List<Enemy>();

    protected void Initialize(Enemy prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            Enemy spawned = Instantiate(prefab, _container.transform);
            spawned.gameObject.SetActive(false);
            _pool.Add(spawned);
        }
    }

    protected void Initialize(Enemy[] prefabs)
    {
        for (int i = 0; i < _capacity; i++)
        {
            int randomIndex = Random.Range(0, prefabs.Length);
            Enemy spawned;

            if (i < prefabs.Length)
                spawned = Instantiate(prefabs[i], _container.transform);
            else
                spawned = Instantiate(prefabs[randomIndex], _container.transform);

            spawned.gameObject.SetActive(false);
            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out Enemy result)
    {
        result = _pool.FirstOrDefault(x => x.gameObject.activeSelf == false);
        return result != null;
    }
}
