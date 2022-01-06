using UnityEngine;

namespace DramaScript
{
	public partial class DramaScriptNodeTimeEntry
	{
		protected DramaScriptNodeDataTimeEntry specificData
		{
			get { return m_data as DramaScriptNodeDataTimeEntry; }
		}

		private void TriggerEnd()
		{
			InvokeOutputTrigger(0);
		}

		private float GetTime()
		{
			return GetInputValue(1, specificData.Time);
		}
	}
}
