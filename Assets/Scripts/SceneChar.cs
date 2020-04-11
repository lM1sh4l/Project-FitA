using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SceneChar : MonoBehaviour
{
    public Character character;
    [HideInInspector]
    public Container container;
    [HideInInspector]
    public List<Material> materials;
    [HideInInspector]
    public SpriteRenderer sr;

    public Dictionary<Sprite, string> inverseEmotions = new Dictionary<Sprite, string>();
    Dictionary<string, Sprite> emotions = new Dictionary<string, Sprite>();

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        character = container.characters.ToList().Find(i => name == i.name);
        foreach (var state in character.states)
        {
            emotions.Add(state.name, state.sprite);
            inverseEmotions.Add(state.sprite, state.name);
        }
        sr.sprite = character.states[0].sprite;
        transform.position = Vector3.right * 100;
    }

    public enum curveType
    {
        Constant,
        EaseInOut,
        Linear
    }

    public IEnumerator Replace(Vector3 viewportPos, float speed = 1, string curve = "Constant")
    {
        AnimationCurve animationCurve = enumToCurve(stringToEnum(curve));

        Vector3 startPos = transform.position;
        Vector3 targetPos = Camera.main.ViewportToWorldPoint((viewportPos + Vector3.one) * .5f);

        targetPos.z = viewportPos.z;

        float t = 0;
        while (t <= 1)
        {
            float f = animationCurve.Evaluate(t);
            transform.position = Vector3.Lerp(startPos, targetPos, f);
            t += speed;
            yield return null;
        }
    }

    public IEnumerator ChangeSprite(string sprite, int material = 0, string curve = "Constant", float speed = 1)
    {
        Sprite s = emotions[sprite];

        if (material > 0)
        {
            AnimationCurve animationCurve = enumToCurve(stringToEnum(curve));
            Material mat = materials[material];
            sr.material = mat;

            sr.material.SetTexture("_End", s.texture);

            float t = 0;
            while (t <= 1)
            {
                sr.material.SetFloat("_T", animationCurve.Evaluate(t));
                t += speed;
                yield return null;
            }

            sr.material = materials[0];
        }
        sr.sprite = s;

    } 

    public static AnimationCurve enumToCurve(curveType curve)
    {
        AnimationCurve animationCurve = new AnimationCurve();
        switch (curve)
        {
            case curveType.Constant:
                animationCurve = AnimationCurve.Constant(0, 1, 1);
                break;
            case curveType.Linear:
                animationCurve = AnimationCurve.Linear(0, 0, 1, 1);
                break;
            case curveType.EaseInOut:
                animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
                break;
        }
        return animationCurve;
    }

    public static curveType stringToEnum(string curve)
    {
        curveType type = (curveType)Enum.Parse(typeof(curveType), curve);
        return type;
    }
}
