using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using ORSAPR.Model;
using Microsoft.VisualBasic.Devices;

namespace ORSAPR.Tests
{
    public class StressTest
    {
        /// <summary>
        /// тест производительности
        /// </summary>
        public void TestAddIn()
        {
            ChiselData _chiselData = new ChiselData();
            _chiselData.Width = 10;
            _chiselData.Length = 100;
            _chiselData.Height = 6;
            _chiselData.BladeLength = 40;
            _chiselData.InnerLength = 10;
            _chiselData.InnerWidth = 5;
            KompasConnector _kompasApp = new KompasConnector();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var streamWriter = new StreamWriter($"log.txt", true);
            Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            var count = 0;
            while (true)
            {
                _kompasApp.CreateDocument3D();
                Manager _manager = new Manager(_kompasApp);
                _manager.BuildModelLocksmith(_chiselData);
                var computerInfo = new ComputerInfo();
                var usedMemory = (computerInfo.TotalPhysicalMemory - computerInfo.AvailablePhysicalMemory) *
                                 0.000000000931322574615478515625;
                streamWriter.WriteLine(
                    $"{++count}\t{stopWatch.Elapsed:hh\\:mm\\:ss}\t{usedMemory}");
                streamWriter.Flush();
            }
        }
    }
}
