using UnityEngine;

namespace DramaScript
{
	public partial class DramaScriptNodeExit
	{
		protected DramaScriptNodeDataExit specificData
		{
			get { return m_data as DramaScriptNodeDataExit; }
		}


		protected override void OnInitInputTrigger()
		{
			RegisterInputTrigger(1, OnEnd);
		}

		partial void OnEnd();
	}
}
