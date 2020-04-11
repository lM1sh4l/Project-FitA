using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class ImageManipulator : MonoBehaviour
{
    public Container container;
    public List<Material> materials;
    Dictionary<string, SceneChar> sceneChars = new Dictionary<string, SceneChar>();

    public void Initialize()
    {
        foreach (var c in container.characters)
        {
            if (c.states.Count > 0)
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
    }

    public void Manipulate(string s)
    {
        string[] parameters = s.Replace("char", "").Replace("(", "").Replace(")", "").Replace(" ", "").Split(',');
        SceneChar character = sceneChars[parameters[0]];
        Vector3 pos = Camera.main.WorldToViewportPoint(character.transform.position) - Vector3.one / 2;
        float velocity = 1, speed = 1;
        string sprite = character.inverseEmotions[character.sr.sprite], mCurve = "Constant", sCurve = "Constant";
        int mat = 0;

        if (parameters.Length > 1)
            for (int i = 1; i < parameters.Length; ++i)
            {
                string[] p = parameters[i].Split('=');
                switch (p[0])
                {
                    //Replace
                    case "x":
                        pos.x = float.Parse(p[1], CultureInfo.InvariantCulture.NumberFormat);
                        break;
                    case "y":
                        pos.y = float.Parse(p[1], CultureInfo.InvariantCulture.NumberFormat);
                        break;
                    case "z":
                        pos.z = float.Parse(p[1], CultureInfo.InvariantCulture.NumberFormat);
                        break;
                    case "moveSpeed":
                        velocity = float.Parse(p[1], CultureInfo.InvariantCulture.NumberFormat);
                        break;
                    case "moveCurve":
                        mCurve = p[1];
                        break;
                    case "flip":
                        character.sr.flipX = !character.sr.flipX;
                        break;
                    //SpriteChanges
                    case "changeSpeed":
                        speed = float.Parse(p[1], CultureInfo.InvariantCulture.NumberFormat);
                        break;
                    case "image":
                        sprite = p[1];
                        break;
                    case "material":
                        mat = int.Parse(p[1]);
                        break;
                    case "changeCurve":
                        sCurve = p[1];
                        break;
                    default:
                        break;
                }
            }

        StartCoroutine(character.Replace(pos, velocity, mCurve));
        StartCoroutine(character.ChangeSprite(sprite, mat, sCurve, speed));
    }
}