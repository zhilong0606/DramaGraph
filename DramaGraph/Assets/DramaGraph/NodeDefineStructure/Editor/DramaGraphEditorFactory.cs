using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DramaScript;
using Google.Protobuf;

namespace GraphEditor.Drama
{
    public static partial class DramaGraphEditorFactory
    {

        private static string GetPortDataString(GraphNodeData nodeData, GraphNodeDefine nodeDefine, string portName)
        {
            GraphPortDefine portDefine = nodeDefine.GetPort(portName);
            if (portDefine != null)
            {
                GraphPortDataString portData = nodeData.GetPortData(portDefine.id) as GraphPortDataString;
                if (portData != null)
                {
                    return portData.value;
                }
            }
            return default;
        }

        private static float GetPortDataFloat(GraphNodeData nodeData, GraphNodeDefine nodeDefine, string portName)
        {
            GraphPortDefine portDefine = nodeDefine.GetPort(portName);
            if (portDefine != null)
            {
                GraphPortDataFloat portData = nodeData.GetPortData(portDefine.id) as GraphPortDataFloat;
                if (portData != null)
                {
                    return portData.value;
                }
            }
            return default;
        }

        private static ByteString GetBuffer(IMessage obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (CodedOutputStream cos = new CodedOutputStream(ms))
                {
                    obj.WriteTo(cos);
                    cos.Flush();
                    ms.Position = 0;
                    return ByteString.FromStream(ms);
                }
            }
        }
    }
}