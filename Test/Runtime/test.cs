using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : projectAPI2D
{
    public override void localAwake()
    {
        throw new System.NotImplementedException();
    }


    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < myChild().Length; i++) print(myChild()[i]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
