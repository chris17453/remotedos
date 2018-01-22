using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace DM_ADV_Drone {
    public class ProcessExtensions {
        private static string FindIndexedProcessName(int pid) {
            var processName = Process.GetProcessById(pid).ProcessName;
            var processesByName = Process.GetProcessesByName(processName);
            string processIndexdName = null;

            for (var index = 0; index < processesByName.Length; index++) {
                processIndexdName = index == 0 ? processName : processName + "#" + index;
                var processId = new PerformanceCounter("Process", "ID Process", processIndexdName);
                if ((int) processId.NextValue() == pid) {
                    return processIndexdName;
                }
            }

            return processIndexdName;
        }

        private static int FindPidFromIndexedProcessName(string indexedProcessName) {
            var parentId = new PerformanceCounter("Process", "Creating Process ID", indexedProcessName);
            return Process.GetProcessById((int) parentId.NextValue()).Id;
        }

        public static int Parent(int process) {
            return FindPidFromIndexedProcessName(FindIndexedProcessName(process));
        }
    }
}