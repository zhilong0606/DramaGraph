using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GraphEditor
{
    public class GraphUtility
    {

        public static string GetPortValueTypeName(EGraphPortValueType valueType)
        {
            switch (valueType)
            {
                case EGraphPortValueType.Bool:
                    return "bool";
                case EGraphPortValueType.String:
                    return "string";
                case EGraphPortValueType.Float:
                    return "float";
                case EGraphPortValueType.Float2:
                    return "Vector2";
                case EGraphPortValueType.Float3:
                    return "Vector3";
            }
            throw new Exception("");
        }
    }
}
