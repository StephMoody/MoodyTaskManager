using System;
using System.Collections.Generic;
using System.Monitoring.Contract;
using System.Text;

namespace System.Monitoring.Process
{
    public class ProcessData : IProcessData
    {
        private readonly System.Diagnostics.Process _process;

        public ProcessData(System.Diagnostics.Process process)
        {
            _process = process;
        }

        public string Name => _process.ProcessName;

        public double MemoryUsage => _process.PeakWorkingSet64 / 1024;

        public double ID => _process.Id;

        public string Description => _process.ProcessName;

        public void Kill()
        {
            _process.Kill();
        }
    }
}
