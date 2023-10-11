using System;
using Codice.Client.Commands.WkTree;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private RawImage _crosshairImage;
    [SerializeField] private Texture _gunSprite;
    [SerializeField] private Texture _invalidSprite;
    
    private Inventory _inventory;

    private void OnEnable()
    {
        _inventory = FindAnyObjectByType<Inventory>();
        _inventory.ActiveItemChanged += HandleActiveItemChanged;

        if (_inventory.ActiveItem != null)
        {
            HandleActiveItemChanged(_inventory.ActiveItem);
        }
        else
        {
            _crosshairImage.texture = _invalidSprite;
        }
    }

    private void OnValidate()
    {
        _crosshairImage = GetComponent<RawImage>();
    }

    private void HandleActiveItemChanged(Item item)
    {
        switch (item.CrosshairMode)
        {
            case CrosshairMode.Gun: 
                _crosshairImage.texture = _gunSprite;
                break;
            case CrosshairMode.Invalid: 
                _crosshairImage.texture = _invalidSprite;
                break;
        }
        
        Debug.Log($"Crosshair detected {item.CrosshairMode}.");
    }
}

public enum CrosshairMode
{
    Invalid,
    Gun
}
