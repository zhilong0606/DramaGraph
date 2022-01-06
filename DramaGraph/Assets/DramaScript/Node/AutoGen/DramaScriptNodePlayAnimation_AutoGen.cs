using UnityEngine;

namespace DramaScript
{
	public partial class DramaScriptNodePlayAnimation
	{
		protected DramaScriptNodeDataPlayAnimation specificData
		{
			get { return m_data as DramaScriptNodeDataPlayAnimation; }
		}


		private void TriggerEnd()
		{
			InvokeOutputTrigger(1);
		}

		private string GetAnimationName()
		{
			return GetInputValue(2, specificData.AnimationName);
		}

		private float GetSpeed()
		{
			return GetInputValue(3, specificData.Speed);
		}

		protected override void OnInitInputTrigger()
		{
			RegisterInputTrigger(0, OnStart);
		}

		partial void OnStart();
	}
}
