using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialog
{
    [TextArea(3,10)]
    [SerializeField] private string[] sentences;
    [SerializeField] private string name;

    public string[] Sentences { get { return sentences; } }

    public string Name { get { return name; } }
}
