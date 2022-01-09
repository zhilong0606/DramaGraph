using UnityEngine;

namespace DramaScript
{
	public partial class DramaScriptNodePlayAnimation
	{
		protected DramaScriptNodeDataPlayAnimation specificData
		{
			get { return m_data as DramaScriptNodeDataPlayAnimation; }
		}


		private string GetAnimationName()
		{
			return GetInputValue(2, specificData.AnimationName);
		}

		private float GetSpeed()
		{
			return GetInputValue(3, specificData.Speed);
		}

		private void TriggerEnd()
		{
			InvokeOutputTrigger(4);
		}

		protected override void OnInitInputTrigger()
		{
			RegisterInputTrigger(1, OnStart);
		}

		partial void OnStart();
	}
}
