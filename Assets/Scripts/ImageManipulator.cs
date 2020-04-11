using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageManipulator : MonoBehaviour
{
    public Container container;
    public List<Material> materials;
    Dictionary<string, SceneChar> sceneChars = new Dictionary<string, SceneChar>();

    public void Initialize()
    {
        foreach (var c in container.characters)
        {
            GameObject go = new GameObject();

            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            SceneChar sc = go.AddComponent<SceneChar>();

            go.name = c.name;
            sc.container = container;
            sc.materials = materials;

            sceneChars.Add(go.name, sc);
        }
    }

    public void Manipulate(string s)
    {
        string[] parameters = s.Replace("char", "").Replace("(", "").Replace(")", "").Replace(" ", "").Split(',');
        SceneChar character = sceneChars[parameters[0]];
        Vector3 pos = Camera.main.WorldToViewportPoint(character.transform.position) - Vector3.one / 2;
        float velocity = 1, speed = 1;
        SceneChar.curveType mCurve = SceneChar.curveType.Constant, sCurve = SceneChar.curveType.Constant;
        string sprite = character.inverseEmotions[character.sr.sprite];
        int mat = 0;

        if (parameters.Length > 1)
            for (int i = 1; i < parameters.Length; ++i)
            {
                switch (parameters[i][0])
                {
                    //Replace
                    case 'x':
                        pos.x = float.Parse(parameters[i].Remove(0, 1).Replace('.', ','));
                        break;
                    case 'y':
                        pos.y = float.Parse(parameters[i].Remove(0, 1).Replace('.', ','));
                        break;
                    case 'z':
                        pos.z = float.Parse(parameters[i].Remove(0, 1).Replace('.', ','));
                        break;
                    case 'v':
                        velocity = float.Parse(parameters[i].Remove(0, 1).Replace('.', ','));
                        break;
                    case 't':
                        mCurve = SceneChar.stringToEnum(parameters[i].Remove(0, 1));
                        break;
                    case 'f':
                        character.sr.flipX = !character.sr.flipX;
                        break;
                    //SpriteChanges
                    case 's':
                        speed = float.Parse(parameters[i].Remove(0, 1).Replace('.', ','));
                        break;
                    case 'i':
                        sprite = parameters[i].Remove(0, 1);
                        break;
                    case 'm':
                        mat = int.Parse(parameters[i].Remove(0, 1));
                        break;
                    case 'a':
                        sCurve = SceneChar.stringToEnum(parameters[i].Remove(0, 1));
                        break;
                    default:
                        break;
                }
            }

        StartCoroutine(character.Replace(pos, velocity, mCurve));
        StartCoroutine(character.ChangeSprite(sprite, mat, sCurve, speed));
    }
}