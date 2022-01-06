namespace DramaScript
{
    public partial class DramaScriptNodePlayAnimation : DramaScriptNode
    {
        private bool m_isStart;
        private float m_curTime;

        partial void OnStart()
        {
            m_isStart = true;
            m_curTime = 0f;
        }

        protected override void OnTick(DramaScriptTime deltaTime)
        {
            if (m_isStart)
            {
                m_curTime += deltaTime.floatSec;
                if (m_curTime > 3f)
                {
                    m_isStart = false;
                    TriggerEnd();
                }
            }
        }
    }
}
