using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu()]
public class Container : ScriptableObject
{
    public List<Character> characters;
    public ActionBinds[] actions;

    void OnValidate() { foreach (var c in characters) { foreach (var s in c.states) { if (s.sprite != null) s.name = s.sprite.name; } } }
}

[System.Serializable]
public class ActionBinds
{
    public string name;
    public UnityEvent action;
}
