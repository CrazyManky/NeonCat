using System;
using System.Collections.Generic;
using Project.Screpts.Character;
using UnityEngine;

public class GameLevel : MonoBehaviour
{
    [SerializeField] private Transform _playerInstance;
    [SerializeField] private CharacterMovement _characterController;
    [SerializeField] private List<GameItem> _gameItems;

    private CharacterMovement _characterInstance;
    private CounterInterectiveItems _counterInterective;
    public CharacterMovement CharacterMovement => _characterInstance;
    public CounterInterectiveItems CounterInterectiveItems => _counterInterective;

    public void InitLevel()
    {
        InstanceCharachet();
        _counterInterective = new CounterInterectiveItems(_gameItems.Count);
        foreach (var gameItem in _gameItems)
        {
            gameItem.OnColisiton += _counterInterective.AddCountValue;
        }
    }

    private void InstanceCharachet()
    {
        _characterInstance = Instantiate(_characterController);
        _characterInstance.transform.position = _playerInstance.position + new Vector3(0, 0, 20);
    }

    public void OnDestroy()
    {
        foreach (var gameItem in _gameItems)
        {
            gameItem.OnColisiton -= _counterInterective.AddCountValue;
        }
    }
}

public class CounterInterectiveItems
{
    private int _maxItems;
    private int _counted = 0;

    public event Action OnMaxItems;

    public CounterInterectiveItems(int maxItems)
    {
        _maxItems = maxItems;
    }

    public void AddCountValue()
    {
        _counted++;
        if (_counted == _maxItems)
        {
            OnMaxItems?.Invoke();
        }
    }
}