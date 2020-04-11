using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SceneChar : MonoBehaviour
{
    public Character character;
    [HideInInspector]
    public Container container;
    SpriteRenderer sr;
    Dictionary<string, Sprite> emotions = new Dictionary<string, Sprite>();

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        character = container.characters.ToList().Find(i => name == i.name);
        foreach (var state in character.states) emotions.Add(state.name, state.sprite);

        sr.sprite = emotions["normal"];
    }

    public enum curveType
    {
        constant,
        easeInOut,
        linear
    }

    public IEnumerator Replace(Vector3 viewportPos, float speed = .1f, curveType curve = curveType.linear)
    {
        AnimationCurve animationCurve = new AnimationCurve();
        switch (curve)
        {
            case curveType.constant:
                animationCurve = AnimationCurve.Constant(0, 1, 1);
                break;
            case curveType.linear:
                animationCurve = AnimationCurve.Linear(0, 0, 1, 1);
                break;
            case curveType.easeInOut:
                animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
                break;
        }
        Vector3 targetPos = Camera.main.ViewportToWorldPoint(viewportPos);
        targetPos.z = viewportPos.z;

        Vector3 startPos = transform.position;

        float t = 0;
        while (t <= 1)
        {
            float f = animationCurve.Evaluate(t);
            transform.position = (1 - f) * startPos + f * targetPos;
            t += speed;
            yield return null;
        }
    }
}
