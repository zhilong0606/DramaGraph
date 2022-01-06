using Google.Protobuf;

namespace DramaScript
{
	public static partial class DramaScriptFactory
	{
		public static DramaScriptNode CreateNode(string typeName)
		{
			switch (typeName)
			{
				case "Entry":
				return new DramaScriptNodeEntry();
				case "Exit":
				return new DramaScriptNodeExit();
				case "TimeEntry":
				return new DramaScriptNodeTimeEntry();
				case "PlayAnimation":
				return new DramaScriptNodePlayAnimation();
			}
			return null;
		}

		public static object ParseNodeData(string typeName, ByteString  buffer)
		{
			switch (typeName)
			{
				case "Entry":
				return DramaScriptNodeDataEntry.Parser.ParseFrom(buffer.ToByteArray());
				case "Exit":
				return DramaScriptNodeDataExit.Parser.ParseFrom(buffer.ToByteArray());
				case "TimeEntry":
				return DramaScriptNodeDataTimeEntry.Parser.ParseFrom(buffer.ToByteArray());
				case "PlayAnimation":
				return DramaScriptNodeDataPlayAnimation.Parser.ParseFrom(buffer.ToByteArray());
			}
			return null;
		}
	}
}
