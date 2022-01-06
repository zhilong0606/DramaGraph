using UnityEngine;

namespace DramaScript
{
	public partial class DramaScriptNodeEntry
	{
		protected DramaScriptNodeDataEntry specificData
		{
			get { return m_data as DramaScriptNodeDataEntry; }
		}

		private void TriggerStart()
		{
			InvokeOutputTrigger(0);
		}
	}
}
