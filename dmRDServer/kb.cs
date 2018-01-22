using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DM_ADV_Drone {
    public class kb {
        byte[] buffer;

        public class keyState {
            public bool depressed;
            public bool toggle;
            public byte code;
            public byte extended;

            public keyState() {

            }
            public keyState(byte code, byte ext) {
                this.code = code;
                this.extended = ext;
                this.toggle = false;
                this.depressed = true;
            }
        }
        public class extendedKey {
            public keyState insert = new keyState();
            public keyState caps = new keyState();
            public keyState num = new keyState();
            public keyState scroll = new keyState();
            public keyState alt = new keyState();
            public keyState ctrl = new keyState();
            public keyState lshift = new keyState();
            public keyState rshift = new keyState();
        };

        public List<keyState> kbqueue = new List<keyState>();
        public int startAddress;
        public int endAddress;


        public extendedKey extendedKeys = new extendedKey();

        [Flags]
        private enum kb_flags {
            INSERT = 0x80,
            CAPS = 0x40,
            NUM = 0x20,
            SCROLL = 0x10,
            ALT = 0x8,
            CTRL = 0x4,
            LEFT_SHIFT = 0x2,
            RIGHT_SHIFT = 0x1
        } ;

        /*
 0040:0017	 Keyboard flag bits (byte) 7=ins, 6=caps, 5=num, 4=scrll,3=ALT, 2=CTRL, 1=LSHFT, 0=RSHFT (toggle states)
 0040:0018	 Keyboard flag bits (byte) (depressed states)
 0040:0019	 Keyboard ALT-Numeric pad number buffer area
 0040:001A	 Pointer to head of keyboard queue
 0040:001C	 Pointer to tail of keyboard queue
 0040:001E	 15 key queue (head=tail, queue empty)
 0040:003E	 */

        public kb(byte[] kb) {
            this.buffer = kb;
        }

        public void read() {
            this.extendedKeys.insert.toggle = (this.buffer[0] & (int)kb_flags.INSERT) >> 7 == 1;
            this.extendedKeys.caps.toggle = (this.buffer[0] & (int)kb_flags.CAPS) >> 6 == 1;
            this.extendedKeys.num.toggle = (this.buffer[0] & (int)kb_flags.NUM) >> 5 == 1;
            this.extendedKeys.scroll.toggle = (this.buffer[0] & (int)kb_flags.SCROLL) >> 4 == 1;
            this.extendedKeys.alt.toggle = (this.buffer[0] & (int)kb_flags.ALT) >> 3 == 1;
            this.extendedKeys.ctrl.toggle = (this.buffer[0] & (int)kb_flags.CTRL) >> 2 == 1;
            this.extendedKeys.lshift.toggle = (this.buffer[0] & (int)kb_flags.LEFT_SHIFT) >> 1 == 1;
            this.extendedKeys.rshift.toggle = (this.buffer[0] & (int)kb_flags.RIGHT_SHIFT) == 1;

            this.extendedKeys.insert.depressed = (this.buffer[1] & (int)kb_flags.INSERT) >> 7 == 1;
            this.extendedKeys.caps.depressed = (this.buffer[1] & (int)kb_flags.CAPS) >> 6 == 1;
            this.extendedKeys.num.depressed = (this.buffer[1] & (int)kb_flags.NUM) >> 5 == 1;
            this.extendedKeys.scroll.depressed = (this.buffer[1] & (int)kb_flags.SCROLL) >> 4 == 1;
            this.extendedKeys.alt.depressed = (this.buffer[1] & (int)kb_flags.ALT) >> 3 == 1;
            this.extendedKeys.ctrl.depressed = (this.buffer[1] & (int)kb_flags.CTRL) >> 2 == 1;
            this.extendedKeys.lshift.depressed = (this.buffer[1] & (int)kb_flags.LEFT_SHIFT) >> 1 == 1;
            this.extendedKeys.rshift.depressed = (this.buffer[1] & (int)kb_flags.RIGHT_SHIFT) == 1;
            this.startAddress = this.buffer[3] - 0x1E;
            this.endAddress = this.buffer[5] - 0x1E;
            if (this.startAddress != this.endAddress) {
                kbqueue.Clear();
                if (this.startAddress < this.endAddress) {
                    for (int a = this.startAddress; a < this.endAddress; a += 2) {
                        keyState key = new keyState(this.buffer[7 + a], this.buffer[8 + a]);
                        kbqueue.Add(key);
                    }
                } else {
                    for (int a = this.startAddress; a < 30; a += 2) {
                        keyState key = new keyState(this.buffer[7 + a], this.buffer[8 + a]);
                        kbqueue.Add(key);
                    }
                    for (int a = 0; a < this.endAddress; a += 2) {
                        keyState key = new keyState(this.buffer[7 + a], this.buffer[8 + a]);
                        kbqueue.Add(key);
                    }
                }
            }
        }


        public class NativeWIN32 {
            public const ushort KEYEVENTF_KEYUP = 0x0002;
            public enum VK : ushort {
                SHIFT = 0x10,
                CONTROL = 0x11,
                MENU = 0x12,
                ESCAPE = 0x1B,
                BACK = 0x08,
                TAB = 0x09,
                RETURN = 0x0D,
                PRIOR = 0x21,
                NEXT = 0x22,
                END = 0x23,
                HOME = 0x24,
                LEFT = 0x25,
                UP = 0x26,
                RIGHT = 0x27,
                DOWN = 0x28,
                SELECT = 0x29,
                PRINT = 0x2A,
                EXECUTE = 0x2B,
                SNAPSHOT = 0x2C,
                INSERT = 0x2D,
                DELETE = 0x2E,
                HELP = 0x2F,
                NUMPAD0 = 0x60,
                NUMPAD1 = 0x61,
                NUMPAD2 = 0x62,
                NUMPAD3 = 0x63,
                NUMPAD4 = 0x64,
                NUMPAD5 = 0x65,
                NUMPAD6 = 0x66,
                NUMPAD7 = 0x67,
                NUMPAD8 = 0x68,
                NUMPAD9 = 0x69,
                MULTIPLY = 0x6A,
                ADD = 0x6B,
                SEPARATOR = 0x6C,
                SUBTRACT = 0x6D,
                DECIMAL = 0x6E,
                DIVIDE = 0x6F,
                F1 = 0x70,
                F2 = 0x71,
                F3 = 0x72,
                F4 = 0x73,
                F5 = 0x74,
                F6 = 0x75,
                F7 = 0x76,
                F8 = 0x77,
                F9 = 0x78,
                F10 = 0x79,
                F11 = 0x7A,
                F12 = 0x7B,
                OEM_1 = 0xBA,           // ',:' for US  
                OEM_PLUS = 0xBB,           // '+' any country  
                OEM_COMMA = 0xBC,           // ',' any country  
                OEM_MINUS = 0xBD,           // '-' any country  
                OEM_PERIOD = 0xBE,           // '.' any country  
                OEM_2 = 0xBF,           // '/?' for US  
                OEM_3 = 0xC0,           // '`~' for US  
                MEDIA_NEXT_TRACK = 0xB0,
                MEDIA_PREV_TRACK = 0xB1,
                MEDIA_STOP = 0xB2,
                MEDIA_PLAY_PAUSE = 0xB3,
                LWIN = 0x5B,
                RWIN = 0x5C
            }
        }

        public struct KEYBDINPUT {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public long time;
            public uint dwExtraInfo;
        };

        [StructLayout(LayoutKind.Explicit, Size = 28)]
        public struct
         INPUT {
            [FieldOffset(0)]
            public uint type;
            [FieldOffset(4)]
            public KEYBDINPUT ki;
        };

        [DllImport("user32.dll")]
        static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        [DllImport("user32.dll", SetLastError=false)]
        static extern IntPtr GetMessageExtraInfo();

        public void write(int pid,int code, int state) {
            ActivateProcess(pid);
            uint scanCode = MapVirtualKey((uint)code, 0);
            uint intReturn = 0;
            INPUT structInput;
            structInput = new INPUT();
            structInput.type = (uint)1;
            structInput.ki.wScan = (ushort)scanCode;
            structInput.ki.time = 0;
            structInput.ki.dwFlags = 0; //(uint)state;
            structInput.ki.dwExtraInfo =  (uint)GetMessageExtraInfo(); //(uint)state;
            //keydown
            structInput.ki.wVk = (ushort)code;
            intReturn = SendInput((uint)1, ref structInput, Marshal.SizeOf(structInput));

            //Keyup
            structInput.ki.dwFlags = NativeWIN32.KEYEVENTF_KEYUP;
            structInput.ki.wVk = (ushort)code;
            intReturn = SendInput((uint)1, ref structInput, Marshal.SizeOf(structInput));
            ActivateProcess((int)FindMyself.GetCurrentProcessId());
        }

        public static void write2(int pid, int code) {
            ushort cmd = 0x12;
            uint scanCode = MapVirtualKey((uint)code, 0);
            uint intReturn = 0;
            INPUT structInput;
            structInput = new INPUT();
            structInput.type = (uint)1;
            structInput.ki.wScan =cmd;
            structInput.ki.time = 0;
            structInput.ki.dwFlags = 0; //(uint)state;
            structInput.ki.dwExtraInfo = (uint)GetMessageExtraInfo(); //(uint)state;
            //keydown
            structInput.ki.wScan = (ushort)scanCode;
            structInput.ki.wVk = (ushort)code;
            intReturn = SendInput((uint)1, ref structInput, Marshal.SizeOf(structInput));
        }

        public static void writeExt(int pid, int code,string f) {
            
            ushort cmd = 0x12;
            uint scanCode = MapVirtualKey((uint)code, 0);
            uint intReturn = 0;
            INPUT structInput;
            structInput = new INPUT();
            structInput.type = (uint)1;
            structInput.ki.wScan = (ushort)code;
            structInput.ki.time = 0;
            if (f == "a") structInput.ki.dwFlags = (uint) kb_flags.ALT;
            if (f == "c") structInput.ki.dwFlags = (uint) kb_flags.CTRL;

            structInput.ki.dwExtraInfo = (uint)GetMessageExtraInfo(); //(uint)state;
            //keydown
            structInput.ki.wScan = (ushort)scanCode;
            structInput.ki.wVk = (ushort)code;
            intReturn = SendInput((uint)1, ref structInput, Marshal.SizeOf(structInput));


            //ActivateProcess((int)FindMyself.GetCurrentProcessId());
        }


        public static void write3(int pid, int code) {
            INPUT structInput;
            structInput = new INPUT();
            structInput.type = (uint)1;
            structInput.ki.wScan = (ushort)code;
            structInput.ki.time = 0;
            structInput.ki.dwFlags = 1;
            structInput.ki.dwExtraInfo = (uint)GetMessageExtraInfo(); //(uint)state;
            //keydown
            structInput.ki.wVk = (ushort)code;
             SendInput((uint)1, ref structInput, Marshal.SizeOf(structInput));
        }

        class FindMyself {
            [DllImport("kernel32.dll")]
            public static extern uint GetCurrentProcessId();
        }

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_SHOWWINDOW = 0x0040;

        public static void ActivateProcess(int PID) {
            if (PID < 0) return;
            try {
                Process proc = Process.GetProcessById(PID);
                IntPtr mainWindow = proc.MainWindowHandle;

                IntPtr newPos = new IntPtr(0);  // 0 puts it on top of Z order.   You can do new IntPtr(-1) to force it to a topmost window, instead.
                SetWindowPos(mainWindow, new IntPtr(0), 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_SHOWWINDOW);
            } catch (Exception e) {

            }
        }


    }
}
 
