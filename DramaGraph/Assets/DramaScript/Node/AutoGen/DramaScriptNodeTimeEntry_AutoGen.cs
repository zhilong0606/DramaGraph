using UnityEngine;

namespace DramaScript
{
	public partial class DramaScriptNodeTimeEntry
	{
		protected DramaScriptNodeDataTimeEntry specificData
		{
			get { return m_data as DramaScriptNodeDataTimeEntry; }
		}

		private float GetTime()
		{
			return GetInputValue(1, specificData.Time);
		}

		private void TriggerEnd()
		{
			InvokeOutputTrigger(2);
		}
	}
}
