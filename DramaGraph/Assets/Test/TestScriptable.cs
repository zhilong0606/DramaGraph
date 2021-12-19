using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "TestScriptable")]
public class TestScriptable : ScriptableObject
{
    public List<TBase> bases = new List<TBase>();
    public TestScriptable()
    {
        bases.Add(new TBase());
        bases.Add(new TA());
        bases.Add(new TB());
    }
}

[Serializable]
public class TBase
{
    public int tbase = 1;
}

[Serializable]
public class TA : TBase
{
    public int ta = 2;
}

[Serializable]
public class TB : TBase
{
    public int tb = 3;
}