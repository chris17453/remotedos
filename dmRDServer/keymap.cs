using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Diagnostics;

namespace dm {
    public static class ToStringExtensions {

        public static string ToHex(this uint value) {
            return String.Format("{0:X}", value);
        }
    }

    public class keymapper {
        public BindingList<key> keys = new BindingList<key>();

        [System.Serializable]
        public class key {
            private bool active = false;
            private string name = "";
            private uint code = 0;
            private uint index = 0;
            private uint altCode = 0;
            private uint shiftCode = 0;
            private uint ctrlCode = 0;
            private uint codePrefix = 0;
            private uint altCodePrefix = 0;
            private uint shiftCodePrefix = 0;
            private uint ctrlCodePrefix = 0;

            [DisplayName("Active")]
            public bool Active {
                get { return active; }
                set { active = value; }
            }
            [DisplayName("Name")]
            public string Name {
                get { return name; }
                set { name = value; }
            }
            [DisplayName("Index")]
            public uint Index {
                get { return index; }
                set { index = value; }
            }
            [DisplayName("Code")]
            public uint Code {
                get { return code; }
                set { code = value; }
            }

            [DisplayName("ShiftCode")]
            public uint ShiftCode {
                get { return shiftCode; }
                set { shiftCode = value; }
            }

            [DisplayName("AltCode")]
            public uint AltCode {
                get { return altCode; }
                set { altCode = value; }
            }

            [DisplayName("CtrlCode")]
            public uint CtrlCode {
                get { return ctrlCode; }
                set { ctrlCode = value; }
            }
            [DisplayName("CodePefix")]
            public uint CodePrefix {
                get { return codePrefix; }
                set { codePrefix = value; }
            }

            [DisplayName("ShiftCode")]
            public uint ShiftCodePrefix {
                get { return shiftCodePrefix; }
                set { shiftCodePrefix = value; }
            }

            [DisplayName("AltCode")]
            public uint AltCodePrefix {
                get { return altCodePrefix; }
                set { altCodePrefix = value; }
            }

            [DisplayName("CtrlCodePrefix")]
            public uint CtrlCodePrefix {
                get { return ctrlCodePrefix; }
                set { ctrlCodePrefix = value; }
            }

            public key(string name) {
                this.name = name;
            }
        }
        private string filename="";
        public keymapper() {
        }
        public keymapper(string filename) {
            this.filename=filename;
            this.load(filename);
        }

        public void reset() {
            foreach (int value in Enum.GetValues(typeof(Keys))) {
                string name = ((Keys)value).ToString();
                key k = new key(name);
                int[] z = code(value, false, false, false);
                int[] c = code(value, false, true, false);
                int[] a = code(value, false, false, true);
                int[] s = code(value, true, false, false);

                k.Index = (uint)value; ;
                k.CodePrefix = (uint)z[0];
                k.Code = (uint)z[1];
                k.CtrlCodePrefix = (uint)c[0];
                k.CtrlCode = (uint)c[1];
                k.AltCodePrefix = (uint)a[0];
                k.AltCode = (uint)a[1];
                k.ShiftCodePrefix = (uint)s[0];
                k.ShiftCode = (uint)s[1];
                keys.Add(k);

            }
        }

        int[] code(int code, bool shift, bool ctrl, bool alt) {
            int[] codes = { 0, 0 };
            byte code1 = 0, code2 = 0;

            //if (code == 144) return;        //skip NUMLOCK

            bool skip = false;
            if (!shift && !ctrl && !alt) {
                if (code >= 65 && code <= 90) {
                    code += 97 - 65;        //turn uppers into lowers with no shift.
                    code1 = 0;
                    code2 = (byte)code;
                }
                skip = true;
            }

            if (shift && !ctrl && !alt) {
                //if(code >= 65 && code <= 90){
                code1 = 0;
                code2 = (byte)code;
                //}
                skip = true;
            }

            if (shift) {
                switch (code) {
                    case 27: code1 = 0x2E; code2 = 0x29; break; //` ~
                    case 48: code1 = 0x29; code2 = 0xE0; break; //0 )
                    case 49: code1 = 0x21; code2 = 0x02; break; //1 !
                    case 50: code1 = 0x40; code2 = 0x03; break; //2 @
                    case 51: code1 = 0x23; code2 = 0x04; break; //3 #
                    case 52: code1 = 0x24; code2 = 0x05; break; //4 $
                    case 53: code1 = 0x25; code2 = 0x06; break; //5 %
                    case 54: code1 = 0x5E; code2 = 0x07; break; //6 ^
                    case 55: code1 = 0x26; code2 = 0x08; break; //7 &
                    case 56: code1 = 0x2A; code2 = 0x09; break; //8 *
                    case 57: code1 = 0x28; code2 = 0x0A; break; //9 (
                    case 173: code1 = 0x5F; code2 = 0x0C; break; //- _
                    case 61: code1 = 0x2B; code2 = 0x0D; break; //= +
                    case 219: code1 = 0x7B; code2 = 0x1A; break; //[ {
                    case 221: code1 = 0x7D; code2 = 0x1B; break; //] }
                    case 220: code1 = 0x7C; code2 = 0x2B; break; //\ |
                    case 59: code1 = 0x3A; code2 = 0x27; break; //; :
                    case 186: code1 = 0x3A; code2 = 0x27; break; //; : //?
                    case 222: code1 = 0x22; code2 = 0x28; break; //' "
                    case 188: code1 = 0x3C; code2 = 0x33; break; //, <

                    case 189: code1 = 0x5F; code2 = 0x0C; break; //- +
                    case 187: code1 = 0x2b; code2 = 0x0D; break; //+ =

                    case 190: code1 = 0x3E; code2 = 0x34; break; //. >
                    case 191: code1 = 0x3F; code2 = 0x35; break; /// ?
                }//end switch
                skip = true;
            }//end if

            if (alt) {
                switch (code) {
                    case 65: code1 = 0x00; code2 = 0x30; break; //ALT A
                    case 77: code1 = 0x00; code2 = 0x32; break; //ALT M
                    case 76: code1 = 0x00; code2 = 0x26; break; //ALT L
                    case 112: code1 = 0x00; code2 = 0x68; break; //f1
                    case 113: code1 = 0x00; code2 = 0x69; break; //f2
                    case 114: code1 = 0x00; code2 = 0x6A; break; //f3
                    case 115: code1 = 0x00; code2 = 0x6B; break; //f4
                    case 116: code1 = 0x00; code2 = 0x6C; break; //f5
                    case 117: code1 = 0x00; code2 = 0x6D; break; //f6
                    case 118: code1 = 0x00; code2 = 0x6E; break; //f7
                    case 119: code1 = 0x00; code2 = 0x6F; break; //f8
                    case 120: code1 = 0x00; code2 = 0x70; break; //f9
                    case 121: code1 = 0x00; code2 = 0X71; break; //f10
                    case 122: code1 = 0x00; code2 = 0x8B; break; //f11
                    case 123: code1 = 0x00; code2 = 0x8C; break; //f12
                    case 33: code1 = 0x00; code2 = 0x99; break; //Page UP
                    case 34: code1 = 0x00; code2 = 0xA1; break; //Page Down
                    case 35: code1 = 0x00; code2 = 0x9F; break; //End
                    case 36: code1 = 0x00; code2 = 0x97; break; //HOME
                    case 37: code1 = 0x00; code2 = 0x9B; break; //left
                    case 38: code1 = 0x00; code2 = 0xA0; break; //down
                    case 39: code1 = 0x00; code2 = 0x9D; break; //right
                    case 40: code1 = 0x00; code2 = 0x98; break; //up
                    case 45: code1 = 0x00; code2 = 0xA2; break; //Insert
                    case 46: code1 = 0x00; code2 = 0xA3; break; //Delete


                }
                skip = true;
            }
            if (ctrl) {
                switch (code) {
                    case 65: code1 = 0x01; code2 = 0x1E; break; //CTRL A
                    case 74: code1 = 0x0A; code2 = 0x24; break; //CTRL J
                    case 75: code1 = 0x0B; code2 = 0x25; break; //CTRL K
                    case 78: code1 = 0x0E; code2 = 0x31; break; //CTRL N
                    case 79: code1 = 0x0F; code2 = 0x18; break; //CTRL O
                    case 80: code1 = 0x10; code2 = 0x19; break; //CTRL P
                    case 83: code1 = 0x13; code2 = 0x1F; break; //CTRL S
                    case 87: code1 = 0x17; code2 = 0x11; break; //CTRL W
                    case 34: code1 = 0xE0; code2 = 0x76; break; //CTRL P
                }
                skip = true;
            }

            if (!skip)
                switch (code) {
                    case 33: code2 = 0x49; code1 = 0xE0; break; //Page UP
                    case 34: code2 = 0x51; code1 = 0xE0; break; //Page Down
                    case 35: code2 = 0x4F; code1 = 0xE0; break; //End
                    case 36: code2 = 0x47; code1 = 0xE0; break; //HOME
                    case 37: code2 = 0x4B; code1 = 0xE0; break; //left
                    case 38: code2 = 0x48; code1 = 0xE0; break; //down
                    case 39: code2 = 0x4D; code1 = 0xE0; break; //right
                    case 40: code2 = 0x50; code1 = 0xE0; break; //up
                    case 45: code2 = 0x52; code1 = 0xE0; break; //Insert
                    case 46: code2 = 0x53; code1 = 0x00; break; //Delete

                    case 96: code2 = 0x32; code1 = 0x30; break; //0
                    case 97: code2 = 0x32; code1 = 0x31; break; //1
                    case 98: code2 = 0x32; code1 = 0x32; break; //2
                    case 99: code2 = 0x32; code1 = 0x33; break; //3
                    case 100: code2 = 0x32; code1 = 0x34; break; //4
                    case 101: code2 = 0x32; code1 = 0x35; break; //5
                    case 102: code2 = 0x32; code1 = 0x36; break; //6
                    case 103: code2 = 0x32; code1 = 0x37; break; //7
                    case 104: code2 = 0x32; code1 = 0x38; break; //8
                    case 105: code2 = 0x32; code1 = 0x39; break; //9

                    case 111: code2 = 0xE0; code1 = 0x2F; break; /// 
                    case 106: code2 = 0x37; code1 = 0x2A; break; //* 
                    case 109: code2 = 0x4A; code1 = 0x2D; break; //-
                    case 107: code2 = 0x4E; code1 = 0x2B; break; //+ 
                    case 110: code2 = 0x34; code1 = 0x2E; break; //. 
                    case 112: code2 = 0x3B; code1 = 0x00; break; //f1
                    case 113: code2 = 0x3C; code1 = 0x00; break; //f2
                    case 114: code2 = 0x3D; code1 = 0x00; break; //f3
                    case 115: code2 = 0x3E; code1 = 0x00; break; //f4
                    case 116: code2 = 0x3F; code1 = 0x00; break; //f5
                    case 117: code2 = 0x40; code1 = 0x00; break; //f6
                    case 118: code2 = 0x41; code1 = 0x00; break; //f7
                    case 119: code2 = 0x42; code1 = 0x00; break; //f8
                    case 120: code2 = 0x43; code1 = 0x00; break; //f9
                    case 121: code2 = 0x44; code1 = 0x00; break; //f10
                    case 122: code2 = 0x85; code1 = 0x00; break; //f11
                    case 123: code2 = 0x86; code1 = 0x00; break; //f12
                    case 189: code1 = 0x2D; code2 = 0x0C; break; //- _
                    case 187: code1 = 0x3D; code2 = 0x0D; break; //+ =
                    case 173: code1 = 0x2D; code2 = 0x0C; break; //- _
                    case 222: code1 = 0x27; code2 = 0x28; break; //' "
                    case 186: code1 = 0x3B; code2 = 0x27; break; //; :
                    case 188: code1 = 0x2C; code2 = 0x33; break; //, <
                    case 190: code1 = 0x2E; code2 = 0x34; break; //. >
                    case 191: code1 = 0x2F; code2 = 0x35; break; /// ?

                    case 219: code1 = 0x5B; code2 = 0x1A; break; /// [
                    case 220: code1 = 0x5C; code2 = 0x2B; break; /// \
                    case 221: code1 = 0x5D; code2 = 0x1B; break; /// ]

                }

            codes[0] = code1;
            //codes[0] = code1.ToString();

            codes[1] = code2;
            return codes;

        }

        public void save(string keyFilename) {
            try {
                System.IO.StreamWriter file = new System.IO.StreamWriter(keyFilename);

                foreach (key k in keys) {
                    string A = "N";
                    if (k.Active) A = "Y";
                    string line = String.Format("{0,20},{1,8},{2,2},{3,2},{4,2},{5,2},{6,2},{7,2},{8,2},{9,2},{10,1}",
                                                            k.Name, k.Index.ToHex(),
                                                            k.CodePrefix.ToHex(), k.Code.ToHex(),
                                                            k.ShiftCodePrefix.ToHex(), k.ShiftCode.ToHex(),
                                                            k.CtrlCodePrefix.ToHex(), k.CtrlCode.ToHex(),
                                                            k.AltCodePrefix.ToHex(), k.AltCode.ToHex(), A);

                    file.WriteLine(line);
                }
                file.Close();
            } catch (Exception ex) {
                MessageBox.Show("Failed to create map file.");
            }
        }

        public void load(string keyFilename) {
            try {
                keys.Clear();
                CultureInfo provider = new CultureInfo("en-US");
                string[] lines = System.IO.File.ReadAllLines(keyFilename);
                foreach (string line in lines) {
                    string[] tokens = line.Split(',');
                    key k = new key(tokens[0].Trim());

                    /*k.Index             = tokens[1].Trim();
                    k.CodePrefix        = tokens[2].Trim();
                    k.Code              = tokens[3].Trim();
                    k.ShiftCodePrefix   = tokens[4].Trim();
                    k.ShiftCode         = tokens[5].Trim();
                    k.CtrlCodePrefix    = tokens[6].Trim();
                    k.CtrlCode          = tokens[7].Trim();
                    k.AltCodePrefix     = tokens[8].Trim();
                    k.AltCode           = tokens[9].Trim();
                    k.Active            = tokens[10].Trim();*/

                    uint temp = 0;
                    UInt32.TryParse(tokens[1].Trim(), NumberStyles.HexNumber, null, out temp); k.Index = temp; temp = 0;
                    UInt32.TryParse(tokens[2].Trim(), NumberStyles.HexNumber, null, out temp); k.CodePrefix = temp; temp = 0;
                    UInt32.TryParse(tokens[3].Trim(), NumberStyles.HexNumber, null, out temp); k.Code = temp; temp = 0;
                    UInt32.TryParse(tokens[4].Trim(), NumberStyles.HexNumber, null, out temp); k.ShiftCodePrefix = temp; temp = 0;
                    UInt32.TryParse(tokens[5].Trim(), NumberStyles.HexNumber, null, out temp); k.ShiftCode = temp; temp = 0;
                    UInt32.TryParse(tokens[6].Trim(), NumberStyles.HexNumber, null, out temp); k.CtrlCodePrefix = temp; temp = 0;
                    UInt32.TryParse(tokens[7].Trim(), NumberStyles.HexNumber, null, out temp); k.CtrlCode = temp; temp = 0;
                    UInt32.TryParse(tokens[8].Trim(), NumberStyles.HexNumber, null, out temp); k.AltCodePrefix = temp; temp = 0;
                    UInt32.TryParse(tokens[9].Trim(), NumberStyles.HexNumber, null, out temp); k.AltCode = temp; temp = 0;
                    if (tokens[10] == "Y") k.Active = true;

                    keys.Add(k);
                }
            } catch (Exception ex) {
                MessageBox.Show("Cannot Load Map File");
            }
        }//end load

        bool IsBitSet(byte b, int pos) {
            return (b & (1 << pos)) != 0;
        }

        public byte[] getCodes(int code,bool shift,bool alt,bool ctrl,byte kbState1) {
            byte[] codes=new byte[]{0,0};
            uint code1=0,code2=0;

            for (int index = 0; index < keys.Count; index++) {
                if (keys[index].Index == code) {
                    if (shift) {                                                                //Shift Codes
                        if (code >= 0x41 && code < 0x5A && IsBitSet(kbState1, 6)) {
                            code2 = keys[index].Code;
                            code1 = keys[index].CodePrefix;
                        } else {
                            code2 = keys[index].ShiftCode;
                            code1 = keys[index].ShiftCodePrefix;
                        }
                    } else
                        if (alt) {                                                              //ALT Codes
                            code2=keys[index].AltCode;
                            code1=keys[index].AltCodePrefix;
                        } else
                            if (ctrl) {                                                         //CTRL Codes
                                code2=keys[index].CtrlCode;
                                code1=keys[index].CtrlCodePrefix;
                            } else {                                                            //Non modified codes
                                if (code >= 0x41 && code < 0x5A && IsBitSet(kbState1, 6)) {
                                    code2 = keys[index].ShiftCode;
                                    code1 = keys[index].ShiftCodePrefix;
                                } else {
                                    code2 = keys[index].Code;
                                    code1 = keys[index].CodePrefix;
                                }
                            }
                }
            }
            codes[0] = (byte)code1;
            codes[1] = (byte)code2;
            return codes;
        }
    }
}