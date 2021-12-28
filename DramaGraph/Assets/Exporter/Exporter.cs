using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.Export
{
    public abstract class Exporter
    {
        protected ExportContext m_context;
        public Action<double, string> actionOnProcessMsgSend;

        public void Export(ExportContext context)
        {
            m_context = context;
            OnExport();
        }

        protected abstract void OnExport();

        protected void SendMsg(double processValue, string content)
        {
            if (actionOnProcessMsgSend != null)
            {
                actionOnProcessMsgSend(processValue, content);
            }
        }
    }
}