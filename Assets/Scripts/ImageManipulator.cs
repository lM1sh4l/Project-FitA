using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageManipulator : MonoBehaviour
{
    public Container container;
    Dictionary<string, SceneChar> sceneChars = new Dictionary<string, SceneChar>();

    public void Initialize()
    {
        foreach(var c in container.characters)
        {
            GameObject go = new GameObject();

            go.AddComponent<SpriteRenderer>();
            SceneChar sc = go.AddComponent<SceneChar>();

            go.name = c.name;
            sc.container = container;

            sceneChars.Add(go.name, sc);
        }
    }
}
