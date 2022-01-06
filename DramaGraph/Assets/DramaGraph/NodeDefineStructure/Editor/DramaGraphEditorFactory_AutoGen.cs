using DramaScript;
using Google.Protobuf;

namespace GraphEditor.Drama
{
	public static partial class DramaGraphEditorFactory
	{
		public static ByteString CreateNodeDataBuffer(GraphNodeData nodeData, GraphNodeDefine nodeDefine)
		{
			switch (nodeData.defineName)
			{
				case "Entry":
				{
					DramaScriptNodeDataEntry scriptNodeData = new DramaScriptNodeDataEntry();
					return GetBuffer(scriptNodeData);
				}
				case "Exit":
				{
					DramaScriptNodeDataExit scriptNodeData = new DramaScriptNodeDataExit();
					return GetBuffer(scriptNodeData);
				}
				case "TimeEntry":
				{
					DramaScriptNodeDataTimeEntry scriptNodeData = new DramaScriptNodeDataTimeEntry();
					scriptNodeData.Time = GetPortDataFloat(nodeData, nodeDefine, "Time");
					return GetBuffer(scriptNodeData);
				}
				case "PlayAnimation":
				{
					DramaScriptNodeDataPlayAnimation scriptNodeData = new DramaScriptNodeDataPlayAnimation();
					scriptNodeData.AnimationName = GetPortDataString(nodeData, nodeDefine, "AnimationName");
					scriptNodeData.Speed = GetPortDataFloat(nodeData, nodeDefine, "Speed");
					return GetBuffer(scriptNodeData);
				}
			}
			return null;
		}
	}
}
