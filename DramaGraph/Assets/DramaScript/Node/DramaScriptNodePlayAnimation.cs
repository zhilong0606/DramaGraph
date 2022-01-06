namespace DramaScript
{
    public partial class DramaScriptNodePlayAnimation : DramaScriptNode
    {
        partial void OnStart()
        {
            throw new System.NotImplementedException();
        }

        private void TriggerExit()
        {
            InvokeOutputTrigger(0);
        }
    }
}
