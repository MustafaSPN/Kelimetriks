﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MADD;

[Docs("This is an example class descriptor")]
public class ExampleClass : ExampleBaseClass
{
    [Docs("Something important about this")]
    private int number = 10;
    private string words;

    private int invisible;

    [Docs("This is the documentation of a method")] 
    public void PubMethod()
    {
        print("AYEE");
    }

    private void CantSeeMe()
    {
        print("SUS");
    }
}
