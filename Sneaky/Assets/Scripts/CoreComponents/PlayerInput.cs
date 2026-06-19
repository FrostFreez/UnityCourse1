using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : CoreComponent
{
    [SerializeField] private InputActionAsset actions;
    [SerializeField] private Dictionary<string, InputItem> items;

    public override void StartComponent()
    {
        foreach (var item in items)
        {
            item.Value.GetAction(actions, item.Key);
        }
    }

    public override void UpdateComponent()
    {
        foreach (var item in items)
        {
            item.Value.ReadValue();
        }
    }
    public T GetInputItem<T>(string name) where T : InputItem
    {
        if (!items.ContainsKey(name) | items[name] is not T)
        {
            Debug.LogWarning(controller.name + ": is trying to access inexistent input: " + name);
            return null;
        }
        return items[name] as T;
    }
}

[Serializable]
public abstract class InputItem
{
    [field: SerializeField] public InputAction action { get; private set; }
    public void GetAction(InputActionAsset actions, string name)
    {
        action = actions.FindAction(name);
    }
    public abstract void ReadValue();
}

[Serializable]
public class InputButton : InputItem
{
    public bool held = false;
    public bool pressed = false;
    public bool released = false;
    public override void ReadValue()
    {
        released = held && !action.IsPressed();
        pressed = !held && action.IsPressed();
        held = action.IsPressed();
    }
}

[Serializable]
public class InputVector2 : InputItem
{
    public Vector2 vector;
    public override void ReadValue()
    {
        vector = action.ReadValue<Vector2>();
    }
}