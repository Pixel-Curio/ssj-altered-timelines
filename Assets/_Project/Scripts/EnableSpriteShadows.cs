using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSpriteShadows : MonoBehaviour
{
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null) throw new System.Exception("No renderer found.");

        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        renderer.receiveShadows = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
