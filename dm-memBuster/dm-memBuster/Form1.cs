using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace dm_memBuster {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e) {

        }

        private void start_Click(object sender, EventArgs e) {
            timer1.Start();
        }

        private void stop_Click(object sender, EventArgs e) {
            timer1.Stop();
        }

        private void offset_ValueChanged(object sender, EventArgs e) {

        }

        private void go_Click(object sender, EventArgs e) {

        }

        public string hexDump(byte[] buffer, string name, int offset, bool dumpToConsole) {
            int lineWidth = 16;
            int lw = 0;
            int a;
            if (buffer.Length == 0) return "";							//if nothing return nothing...
            string border = " | ", end = "\r\n";
            StringBuilder ascii = new StringBuilder();
            StringBuilder output = new StringBuilder();

            output.Append(String.Format("Buffer:{0}       Size: {1,10:G} \r\n", name, buffer.Length));
            output.Append("HEX DUMP ");
            for (a = 0; a < lineWidth; a++) {
                output.Append(String.Format("{0:X2} ", a));
                ascii.Append(String.Format("{0:X1}", a % 0x10));
            }


            output.Append(border + ascii + end);

            ascii = new StringBuilder();
            int index = 0;
            foreach (byte b in buffer) {
                if (lw == 0) {
                    output.Append(String.Format("{0:X8} ", offset + index));
                }
                index++;
                output.Append(String.Format("{0:X2} ", b));

                if (b < 0x10 || b == 0x7f) {
                    ascii.Append(" ");
                } else {
                    ascii.Append((char)b);
                }

                lw++;
                if (lw == lineWidth) {		            //display as ascii now...
                    lw = 0;
                    output.Append(border + ascii + end);
                    ascii = new StringBuilder();
                }

            }

            if (lw != 0) {
                for (a = lw; a < lineWidth; a++) {
                    output.Append("   ");
                }
                output.Append(border + ascii + end);
            }
            if (dumpToConsole) Console.Write(output.ToString());
            return output.ToString();
        }

        int processHandle = 0;
        public bool StartProcess(int pid) {
            
                this.processHandle = OpenProcess(2035711, false, pid);
                if (this.processHandle == 0) {
                    //   MessageBox.Show(this.ProcessName + " is not running or has not been found. Please check and try again", "Process Not Found", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return false;
                }
                return true;
            //MessageBox.Show("Define process name first!");
            return false;
        }

        public byte[] ReadMem(int pOffset, int pSize) {
            byte[] buffer = new byte[pSize];
            ReadProcessMemory(this.processHandle, pOffset, buffer, pSize, 0);
            return buffer;
        }

        [DllImport("kernel32.dll")]
        public static extern int OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(int hObject);

        public void closeProcess() {
            CloseHandle(this.processHandle);
        }
                
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, int lpNumberOfBytesRead);
        

        private void timer1_Tick(object sender, EventArgs e) {
            //Process processes = Process.GetProcessById(2424);
            //ReadWriteMemory.ProcessMemory m = new ReadWriteMemory.ProcessMemory();
            int pidI = 0;
            Int32.TryParse(pid.Text, out pidI);
            bool status = StartProcess(pidI);
            if (!status && pidI!=0) {
                closeProcess();
                return;
            }
               byte[] kbBuffer = ReadMem((int)offset.Value,(int)depth.Value);
               hex.Text = hexDump(kbBuffer, "Memory", 0, false);
            closeProcess();
        }
    }
}
