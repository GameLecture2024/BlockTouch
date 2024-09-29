using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{
    private BlockRoot blockRoot = null;

    // Start is called before the first frame update
    void Start()
    {
        blockRoot = GetComponent<BlockRoot>();
        blockRoot.InitialSetUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
