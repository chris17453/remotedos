using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace dmKB_Mapper
{
    public partial class km : Form
    {
        public km()
        {
            InitializeComponent();
        }
        string keyFileName=@"C:\remoteDOS\keyMap.txt";

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        void resetKBMap() {
            
        }

        dm.keymapper keymap = new dm.keymapper();
        //BindingList<dm.keymapper.key> keys = new BindingList<dm.keymapper.key>(keymap.keys);

        private void kView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
            if (e.Value != null) {
                long value = 0;
                if (long.TryParse(e.Value.ToString(), out value)) {
                    e.Value = value.ToString("X");
                    e.FormattingApplied = true;
                }
            }
        }

        private void km_Load(object sender, EventArgs e){
            keymap.reset();
             kView.AutoGenerateColumns = false;

             DataGridViewCell cell = new DataGridViewTextBoxCell();
             DataGridViewTextBoxColumn colActiveKey = new DataGridViewTextBoxColumn()
             {
                 CellTemplate = cell,
                 Name = "Active",
                 HeaderText = "Active",
                 DataPropertyName = "Active"
             };
             kView.Columns.Add(colActiveKey);          
            DataGridViewTextBoxColumn colKey= new DataGridViewTextBoxColumn(){
                CellTemplate = cell, 
                Name             = "Key",
                HeaderText       = "Key",
                DataPropertyName = "Name" 
               };
            kView.Columns.Add(colKey);
            DataGridViewTextBoxColumn colIndex = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "Index",
                HeaderText = "Index",
                DataPropertyName = "Index"
            };
            kView.Columns.Add(colIndex);
            DataGridViewTextBoxColumn colCodePrefix = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "CodePrefix",
                HeaderText = "CodePrefix",
                DataPropertyName = "CodePrefix"
            };
            kView.Columns.Add(colCodePrefix);
            /*
             
            
                                  F1, 0,      70, 0,54, 0,5E, 0,68
                              F2, 0,      71, 0,55, 0,5F, 0,69
                              F3, 0,      72, 0,56, 0,60, 0,6A
                              F4, 0,      73, 0,57, 0,61, 0,6B
                              F5, 0,      74, 0,58, 0,62, 0,6C
                              F6, 0,      75, 0,59, 0,63, 0,6D
                              F7, 0,      76, 0,5A, 0,64, 0,6E
                              F8, 0,      77, 0,5B, 0,65, 0,6F
                              F9, 0,      78, 0,5C, 0,66, 0,70
                             F10, 0,      79, 0,5D, 0,67, 0,71
                             F11, 0,      7A, 0,87, 0,89, 0,8B
                             F12, 0,      7B, 0,88, 0,8A, 0,8C

             */
            DataGridViewTextBoxColumn colCode = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "Code",
                HeaderText = "Code",
                DataPropertyName = "Code"
            };
            kView.Columns.Add(colCode);
            DataGridViewTextBoxColumn colShiftCodePrefix = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "ShiftCodePrefix",
                HeaderText = "ShiftCodePrefix",
                DataPropertyName = "ShiftCodePrefix"
            };
            kView.Columns.Add(colShiftCodePrefix);
            DataGridViewTextBoxColumn colShiftCode = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "ShiftCode",
                HeaderText = "ShiftCode",
                DataPropertyName = "ShiftCode"
            };
            kView.Columns.Add(colShiftCode);
            DataGridViewTextBoxColumn colAltCodePrefix = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "AltCodePrefix",
                HeaderText = "AltCodePrefix",
                DataPropertyName = "AltCodePrefix"
            };
            kView.Columns.Add(colAltCodePrefix);
            DataGridViewTextBoxColumn colAltCode = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "AltCode",
                HeaderText = "AltCode",
                DataPropertyName = "AltCode"
            };
            kView.Columns.Add(colAltCode);
            DataGridViewTextBoxColumn colCtrlCodePrefix = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "CtrlCodePrefix",
                HeaderText = "CtrlCodePrefix",
                DataPropertyName = "CtrlCodePrefix"
            };
            kView.Columns.Add(colCtrlCodePrefix);
            DataGridViewTextBoxColumn colCtrlCode = new DataGridViewTextBoxColumn()
            {
                CellTemplate = cell,
                Name = "CtrlCode",
                HeaderText = "CtrlCode",
                DataPropertyName = "CtrlCode"
            };
            kView.Columns.Add(colCtrlCode);
            kView.DataSource = keymap.keys;
        }

        private void saveas_Click(object sender, EventArgs e)
        {

        }

      
        private void Reset_Click(object sender, EventArgs e)
        {
            resetKBMap();
        }
        public string hexDump(byte[] buffer, string name, int offset, bool dumpToConsole)
        {
            int lineWidth = 16;
            int lw = 0;
            int a;
            if (buffer.Length == 0) return "";							//if nothing return nothing...
            string border = " | ", end = "\r\n";
            StringBuilder ascii = new StringBuilder();
            StringBuilder output = new StringBuilder();

            output.Append(String.Format("Buffer:{0}       Size: {1,10:G} \r\n", name, buffer.Length));
            output.Append("HEX DUMP ");
            for (a = 0; a < lineWidth; a++)
            {
                output.Append(String.Format("{0:X2} ", a));
                ascii.Append(String.Format("{0:X1}", a % 0x10));
            }


            output.Append(border + ascii + end);

            ascii = new StringBuilder();
            int index = 0;
            foreach (byte b in buffer)
            {
                if (lw == 0)
                {
                    output.Append(String.Format("{0:X8} ", offset + index));
                }
                index++;
                output.Append(String.Format("{0:X2} ", b));

                if (b < 0x10 || b == 0x7f)
                {
                    ascii.Append(" ");
                }
                else
                {
                    ascii.Append((char)b);
                }

                lw++;
                if (lw == lineWidth)
                {		            //display as ascii now...
                    lw = 0;
                    output.Append(border + ascii + end);
                    ascii = new StringBuilder();
                }

            }

            if (lw != 0)
            {
                for (a = lw; a < lineWidth; a++)
                {
                    output.Append("   ");
                }
                output.Append(border + ascii + end);
            }
            if (dumpToConsole) Console.Write(output.ToString());
            return output.ToString();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            return;
            Process processes = Process.GetProcessById(2424);
            IntPtr p = processes.MainWindowHandle;
            ReadWriteMemory.ProcessMemory m = new ReadWriteMemory.ProcessMemory();
            bool status = m.StartProcess(2424);
            if (!status){
                m.closeProcess();
                return;
            }
            m.WriteByte(0x41A, 30);
            m.WriteByte(0x41C, 30);
            
            //System.Threading.Thread.Sleep(10);
            this.code1=m.ReadByte(0x41E);
            this.code2 = m.ReadByte(0x41F);
         //   byte[] kbBuffer = m.ReadMem(0x300, 0x200);
         //   hex.Text = hexDump(kbBuffer, "KeyBoard", 0, false);
            m.closeProcess();
        }

        byte code1=0, code2 = 0;

        private void kView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void hex_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void hex_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
            for (int index = 0; index < keymap.keys.Count;index++ )
            {
                if (keymap.keys[index].Index== e.KeyValue)
                {
                    if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                    {
                        keymap.keys[index].ShiftCode = this.code2;
                        keymap.keys[index].ShiftCodePrefix = this.code1;
                    }
                    else
                        if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
                        {
                            keymap.keys[index].AltCode = this.code2;
                            keymap.keys[index].AltCodePrefix = this.code1;
                        }
                        else
                            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                            {
                                keymap.keys[index].CtrlCode = this.code2;
                                keymap.keys[index].CtrlCodePrefix = this.code1;
                            }
                            else
                            {
                                keymap.keys[index].Code = this.code2;
                                keymap.keys[index].CodePrefix = this.code1;
                            }
                }
            }
            kView.Refresh();   
        }

        private void load_Click(object sender, EventArgs e) {
            keymap.load(keyFileName);
        }

        private void save_Click(object sender, EventArgs e) {
            keymap.save(keyFileName);
        }

        
    }
}
