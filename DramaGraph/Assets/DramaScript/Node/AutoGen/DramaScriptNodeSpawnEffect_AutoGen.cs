using UnityEngine;

namespace DramaScript
{
	public partial class DramaScriptNodeSpawnEffect
	{
		protected DramaScriptNodeDataSpawnEffect specificData
		{
			get { return m_data as DramaScriptNodeDataSpawnEffect; }
		}

		private string GetPath()
		{
			return GetInputValue(1, specificData.Path);
		}

		private void TriggerExit()
		{
			InvokeOutputTrigger(2);
		}
	}
}
