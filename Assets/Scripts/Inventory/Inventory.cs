﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action<Item> ActiveItemChanged;

    [SerializeField] private Transform _rightHand;
    
    private List<Item> _items = new List<Item>();
    private Transform _itemRoot;

    public  Item ActiveItem { get; private set; }
    
    private void Awake()
    {
        _itemRoot = new GameObject("item").transform;
        _itemRoot.transform.SetParent(transform);
    }

    public void Pickup(Item item)
    {
        _items.Add(item);
        item.transform.SetParent(_itemRoot);

        Equip(item);
    }

    private void Equip(Item item)
    {
        Debug.Log($"Equiped Item {item.gameObject.name}");
        item.transform.SetParent(_rightHand);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
        ActiveItem = item;
        ActiveItemChanged?.Invoke(ActiveItem);
    }
    
}