using UnityEngine;

public abstract class ItemComponent : MonoBehaviour
{
    public bool CanUse => Time.time >= _nextUseTime;
    
    protected float _nextUseTime;
    public abstract void Use(); // abstract methods can not have an implementation
}