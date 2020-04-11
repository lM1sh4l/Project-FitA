using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Character
{
    public string name;
    public Color color;

    public List<AnimationState> states;

    private Character() { }
}

[System.Serializable]
public class AnimationState
{
    [HideInInspector]
    public string name;
    public Sprite sprite;
}
