using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace dm
{
    public partial class dosScreen : UserControl
    {
        private byte[] _screen = null;
        private byte[] _screen2 = null;
        private int _cursorX = 0;
        private int _cursorY = 0;
        private float _fontSize = 10;
        private string _fontName = "Ludica Consle";
        private float _lineHeight = 12;
        private float _columnWidth = 8;
        private bool blink = false;
        public bool update = false;
        public bool _foreceShow=false;
        public bool _showData = false;
        private Timer redrawTimer;
        private IContainer components;
        private Timer blinkTimer;
        public Bitmap screen;
        public int width=80;
        public int height=25;


        [Flags]

        public enum Colors{ 
            BLACK=0x00,
            DARKBLUE=0x01,
            DARKGREEN=0x02,
            DARKCYAN=0x03,
            DARKRED=0x04,
            DARKMAGENTA=0x05,
            BROWN=0x06,
            LIGHTGRAY=0x07,
            GRAY=0x08,
            BLUE=0x09,
            GREEN=0x0A,
            CYAN=0x0B,
            RED=0x0C,
            MAGENTA=0x0D,
            YELLOW=0xE,
            WHITE=0x0F };


        [Description("Screen Data"),
            Category("DM"),
            DefaultValue(null),
            Browsable(true)]
        public byte[] screenData
        {
            get { return _screen; }
            set { _screen2 = _screen; _screen = value; dataPos = 1;  this.Refresh(); }
        }
        [Description("Screen Cursor X"),
            Category("DM"),
            DefaultValue(0),
            Browsable(true)]
        public int cursorX{
            get { return _cursorX; }
            set { _cursorX = value; }
        }
        [Description("Screen Cursor Y"),
            Category("DM"),
            DefaultValue(0),
            Browsable(true)]
        public int cursorY{
            get { return _cursorY; }
            set { _cursorY = value; }
        }
        [Description("Font Size"),
            Category("DM"),
            DefaultValue(0),
            Browsable(true)]
        public float fontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; }
        }
        [Description("Font Name"),
            Category("DM"),
            DefaultValue(0),
            Browsable(true)]
        public string fontName
        {
            get { return _fontName;  }
            set { _fontName = value; }
        }
        [Description("Line Height"),
            Category("DM"),
            DefaultValue(0),
            Browsable(true)]
        public float lineHeight
        {
            get { return _lineHeight;  }
            set { _lineHeight = value; }
        }
        [Description("Line Height"),
            Category("DM"),
            DefaultValue(0),
            Browsable(true)]
        public float columnWidth
        {
            get { return _columnWidth;  }
            set { _columnWidth = value;  }
        }

        [Description("Force Update"),
            Category("DM"),
            DefaultValue(false),
            Browsable(true)]
        public bool forceShow
        {
            get { return _foreceShow; }
            set { _foreceShow = value; }
        }

        [Description("Show Data"),
            Category("DM"),
            DefaultValue(false),
            Browsable(true)]
        public bool showData
        {
            get { return _showData; }
            set { _showData = value; }
        }

        
        bool _selection=false;
        [Description("Enable Selection"),
            Category("DM"),
            DefaultValue(false),
            Browsable(true)]
        public bool selection
        {
            get { return _selection;  }
            set { _selection = value; }
        }

        private Hashtable _colorMapFG = new Hashtable();
        private Hashtable _colorMapBG = new Hashtable();
        Hashtable brushMapFG = new Hashtable();
        Hashtable brushMapBG = new Hashtable();
        private Hashtable _keyMap = new Hashtable();
        
        public Hashtable colorMapFG
        {
            get
            {
                return _colorMapFG;
            }
            set
            {
                _colorMapFG = value;
                brushMapFG.Clear();
                foreach (DictionaryEntry entry in _colorMapFG)
                {
                    brushMapFG[entry.Key]=new SolidBrush(Color.FromArgb(Convert.ToInt32((string)entry.Value, 16)));
                }
            }
        }
        
        public Hashtable colorMapBG
        {
            get
            {
                return _colorMapBG;
            }
            set
            {
                _colorMapBG = value;
                brushMapBG.Clear();
                foreach (DictionaryEntry entry in _colorMapBG)
                {
                    brushMapBG[entry.Key] = new SolidBrush(Color.FromArgb(Convert.ToInt32((string)entry.Value, 16)));
                }
            }
        }

        public Hashtable keyMap
        {
            get
            {
                return _keyMap;
            }
            set
            {
                _keyMap.Clear();
                foreach (DictionaryEntry entry in value) {
                    _keyMap[entry.Key]=Convert.ToInt32((string)entry.Value, 16);
                }
            }
        }
        
        public dosScreen()
        {
            InitializeComponent();
            /*if (_screen == null)
            {
                byte[] s=new byte[4000];
                int i;
                for (int y = 0; y < 25; y++)
                {
                    for (int x = 0; x < 80; x++)
                    {
                        i = (y * 80 + x) * 2;
                        s[i ] = (byte)(0x20);
                        s[i+1] = 0x00; ;
                    }

                }
                screenData= s;
            }*/
        }

        public int bgToggle=0;
        
        //float columnWidth       = this.Width / 80f;
        //float lineHeight        = this.Height / 25f;
            
        public void dosScreenToString() {
            if (_screen == null) return;
            if (brushMapBG.Count == 0) return;
            if (brushMapFG.Count == 0) return;
            if (colorMapBG.Count == 0) return;
            if (colorMapFG.Count == 0) return;

            if (screen == null || screen.Width != this.Width || screen.Height != this.Height)
            {
                try {
                    screen = new Bitmap(this.Width, this.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                } catch {
                    return;
                }
            }

        
            columnWidth       = screen.Width / 80f;
            lineHeight        = screen.Height / 25f;
            //this.lineHeight=lineHeight;
            //this.columnWidth=columnWidth;
            float halfColumnWidth   = columnWidth / 2f;
            float halfLineHeight    =  lineHeight / 2f;
            float fontSize          = _fontSize;
             
            Graphics g          = Graphics.FromImage(screen);
            //g.SmoothingMode     = System.Drawing.Drawing2D.SmoothingMode.None;
            //g.PixelOffsetMode   = System.Drawing.Drawing2D.PixelOffsetMode.None;
            //g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            //g.CompositingMode   = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;


            float maxFontSize = 400;
            float minFontSize = 8;
            Font f = new Font(fontName, fontSize, FontStyle.Regular);
            SizeF extent = g.MeasureString("W", f);
             
             

              float hRatio = lineHeight/ extent.Height;
              float wRatio = columnWidth / extent.Width;
              float ratio = (hRatio <  wRatio) ? hRatio : wRatio;

              float newSize = f.Size * ratio;

              if (newSize < minFontSize)
                newSize = minFontSize;
              else if (newSize > maxFontSize)
                newSize = maxFontSize;

              f = new Font(fontName, columnWidth, FontStyle.Regular);
  
      //      
     //       float sf = (columnWidth / extent.Width < lineHeight / extent.Height) ? columnWidth / extent.Width : lineHeight / extent.Height;
    //        g.ScaleTransform(sf, sf);  
                
            float offWidth =halfColumnWidth-extent.Width/2+1;
            float offHeight = halfLineHeight - extent.Height / 2 + 1;
            float x1=0, x3=offWidth,y1=0, y3=offHeight;
            Brush bgBrush;
            int line = 0, column = 0;


                float ox1 = -1, oy1 = -1;
                int d = 0, od = 0;
                Brush ob = Brushes.Black;
                for (int i = 0; i < _screen.Length; i += 2) {
                        d = _screen[i + 1] >> 4;
                        bgBrush =(SolidBrush) brushMapBG[d.ToString()];
                        /*switch (d) {
                            case 0x0: bgBrush = Brushes.Black; break;
                            case 0x1: bgBrush = Brushes.DarkBlue; break;
                            case 0x2: bgBrush = Brushes.DarkGreen; break;
                            case 0x3: bgBrush = Brushes.DarkCyan; break;
                            case 0x4: bgBrush = Brushes.DarkRed; break;
                            case 0x5: bgBrush = Brushes.DarkMagenta; break;
                            case 0x6: bgBrush = Brushes.Brown; break;
                            case 0x7: bgBrush = Brushes.LightGray; break;
                            case 0x8: bgBrush = Brushes.Gray; break;
                            case 0x9: bgBrush = Brushes.Blue; break;
                            case 0xa: bgBrush = Brushes.Green; break;
                            case 0xb: bgBrush = Brushes.Cyan; break;
                            case 0xc: bgBrush = Brushes.Red; break;
                            case 0xd: bgBrush = Brushes.Magenta; break;
                            case 0xe: bgBrush = Brushes.Yellow; break;
                            case 0xf: bgBrush = Brushes.White; break;
                            default: bgBrush = Brushes.Black; break;
                        }*/
                        if (d != od ) {
                            g.FillRectangle(ob, ox1, oy1, x1+columnWidth, lineHeight);
                            ox1 = x1;
                            oy1 = y1;
                        }
                        if( column==79){
                            g.FillRectangle(bgBrush, ox1, oy1, x1+columnWidth, lineHeight);
                            ox1 = x1;
                            oy1 = y1;
                        }

                        od = d;
                        ob=bgBrush;
                    
                        x1 += columnWidth;
                        column++;
                        if (column > 79) {
                            line++; column = 0;
                            y1 += lineHeight;
                            x1 = 0;
                            ox1 = 0;
                            oy1 = y1;
                            od = 0;
 //                           g.FillRectangle(ob, ox1, oy1, x1 + columnWidth, lineHeight);

                        }
                    }//end BG LOOP
                    
                    int hx1=-1,hy1=-1,hx2=-1,hy2=-1;
                    if(selection && highlight){
                            if(selectionX1 >selectionX2) { hx1=selectionX2; hx2=selectionX1; } else { hx1=selectionX1; hx2=selectionX2; } 
                            if(selectionY1 >selectionY2) { hy1=selectionY2; hy2=selectionY1; } else { hy1=selectionY1; hy2=selectionY2; } 
                                
                            g.FillRectangle(Brushes.LightGray, hx1*columnWidth, hy1*lineHeight, (hx2-hx1+1)*columnWidth ,(hy2-hy1+1)*lineHeight);
                    }
 
                /*
                bgToggle++;
                bgToggle %= 2;
                if(bgToggle==0) g.FillRectangle(Brushes.Gray, 0, 0, columnWidth, lineHeight);
                else g.FillRectangle(Brushes.DarkGray, 0, 0, columnWidth, lineHeight);
                  */          

                x1 = 0; x3 = 0; y1 = 0; y3 = 0; od = 0;
                column=0;
                line=0;
                string text = ""; ob = Brushes.DarkOrange;
                for (int i = 0; i < _screen.Length; i += 2) {
                        int hex = _screen[i];
                        int c;
                        /*switch(hex) {
                            case 176: //c = 0x2591; //break;               //
                            case 177: //c = 0x2592; //break;               //
                            case 178: //c = 0x2593; //break;               //
                                     c = 0; break;               //
                            case 179: c = 0x2502; break;               //│   drawings light vertical
                            case 180: c = 0x2524; break;               //┤   drawings light vertical and left
                            case 181: c = 0x2561; break;               //╡   drawings vertical single and left double
                            case 182: c = 0x2562; break;               //╢   drawings vertical double and left single
                            case 183: c = 0x2556; break;               //╖   drawings down double and left single
                            case 184: c = 0x2555; break;               //╕   drawings down single and left double
                            case 185: c = 0x2563; break;               //╣   drawings double vertical and left
                            case 186: c = 0x2551; break;               //║   drawings double vertical
                            case 187: c = 0x2557; break;               //╗   drawings double down and left
                            case 188: c = 0x255D; break;               //╝   drawings double up and left
                            case 189: c = 0x255C; break;               //╜   drawings up double and left single
                            case 190: c = 0x255B; break;               //╛   drawings up single and left double
                            case 191: c = 0x2510; break;               //┐   drawings light down and left
                            case 192: c = 0x2514; break;               //└   drawings light up and right
                            case 193: c = 0x2534; break;               //┴   drawings light up and horizontal
                            case 194: c = 0x252C; break;               //┬   drawings light down and horizontal
                            case 195: c = 0x251C; break;               //├   drawings light vertical and right
                            case 196: c = 0x2500; break;               //─   drawings light horizontal
                            case 197: c = 0x253C; break;               //┼   drawings light vertical and horizontal
                            case 198: c = 0x255E; break;               //╞   drawings vertical single and right double
                            case 199: c = 0x255F; break;               //╟   drawings vertical double and right single
                            case 200: c = 0x255A; break;               //╚   drawings double up and right
                            case 201: c = 0x2554; break;               //╔   drawings double down and right
                            case 202: c = 0x2569; break;               //╩   drawings double up and horizontal
                            case 203: c = 0x2566; break;               //╦   drawings double down and horizontal
                            case 204: c = 0x2560; break;               //╠   drawings double vertical and right
                            case 205: c = 0x2550; break;               //═   drawings double horizontal
                            case 206: c = 0x256C; break;               //╬   drawings double vertical and horizontal
                            case 207: c = 0x2567; break;               //╧   drawings up single and horizontal double
                            case 208: c = 0x2568; break;               //╨   drawings up double and horizontal single
                            case 209: c = 0x2564; break;               //╤   drawings down single and horizontal double
                            case 210: c = 0x2565; break;               //╥   drawings down double and horizontal single
                            case 211: c = 0x2559; break;               //╙   drawings up double and right single
                            case 212: c = 0x2558; break;               //╘   drawings up single and right double
                            case 213: c = 0x2552; break;               //╒   drawings down single and right double
                            case 214: c = 0x2553; break;               //╓   drawings down double and right single
                            case 215: c = 0x256B; break;               //╫   drawings vertical double and horizontal single
                            case 216: c = 0x256A; break;               //╪   drawings vertical single and horizontal double
                            case 217: c = 0x2518; break;               //┘   drawings light up and left
                            case 218: c = 0x250C; break;               //┌   drawings light down and right
                            case 219: c = 0x2588; break;               //█   full block
                            case 220: c = 0x2584; break;               //▄   lower half block
                            case 221: c = 0x258C; break;               //▌   left half block
                            case 222: c = 0x2590; break;               //▐   right half block
                            case 223: c = 0x2580; break;               //▀*/
                          //  default: c = hex; break;
                        //}
                        if (keyMap[hex.ToString()] == null) c = hex;
                        else c = (int)keyMap[hex.ToString()];
                        Brush brush;
                        d = _screen[i + 1] & 0xF;
                        brush = (SolidBrush)brushMapFG[d.ToString()];
                        /*switch(d) {
                            case 0x7: brush = Brushes.LightGray; break;
                            case 0x0: brush = Brushes.Black; break;
                            case 0x1: brush = Brushes.DarkBlue; break;
                            case 0x2: brush = Brushes.DarkGreen; break;
                            case 0x3: brush = Brushes.DarkCyan; break;
                            case 0x4: brush = Brushes.DarkRed; break;
                            case 0x5: brush = Brushes.DarkMagenta; break;
                            case 0x6: brush = Brushes.Brown; break;
                            case 0x8: brush = Brushes.Gray; break;
                            case 0x9: brush = Brushes.Blue; break;
                            case 0xa: brush = Brushes.Green; break;
                            case 0xb: brush = Brushes.Cyan; break;
                            case 0xc: brush = Brushes.Red; break;
                            case 0xd: brush = Brushes.Magenta; break;
                            case 0xe: brush = Brushes.Yellow; break;
                            case 0xf: brush = Brushes.White; break;
                            default: brush = Brushes.AntiqueWhite; break;
                        }*/

                        if(c!=0)
                        switch (hex) {
                            case 0: break;
                            case 32 : break;
                            /*case 176: break;
                            case 177: break;
                            case 178:  break;*/
                            default: g.DrawString(((char)c).ToString(), f, brush, x1, y1); break;
                        }
                        if(highlight && hx1>=0 && hx2>=0 && hy1>=0 && hy2>=0 ){
                            if(column>=hx1 && column<=hx2 && line>=hy1 && line<=hy2) { 
                                text=text+(char)c; 
                                if(column==hx2) { text=text+"\r\n";  } 
                            }
                        }
                        x1 += columnWidth;
                        column++;
                        if (column > 79) {
                            line++; column = 0;
                            y1 += lineHeight;
                            x1 = 0;
                            //ox1 = 0;
                            //oy1 = y1;
                            //od = 0;
                          //  g.DrawString(text, f, brush, ox1,oy1);
                          //  text = "";
                        
                        }
      
                    
                }//end i loop

                x1 = _cursorX * columnWidth;
                y1 = _cursorY * lineHeight;
                if (blink){
                    bgBrush = Brushes.White; 
                } else {
                    bgBrush = Brushes.Black; 
                }
                g.FillRectangle(bgBrush,x1 + 2, y1 + lineHeight - 2 - 4, columnWidth - 2, 4);

                if (showData){
                    redrawPos++;
                    redrawPos %= 2;
                    if (redrawPos == 0) {
                        g.FillRectangle(Brushes.LightGreen, 0, 0, columnWidth,lineHeight);
                    }
                    if (dataPos == 1)
                    {
                        dataPos = 0;
                        g.FillRectangle(Brushes.Green, columnWidth, 0, columnWidth, lineHeight);
                    }

                }
             //screen=scn;
                update = false;
                try
                {
                    if (text != "")
                        Clipboard.SetText(text);
                }
                catch (Exception ex)
                {

                }
        }

         int redrawPos = 0, dataPos = 0;

         
        public void Print(){
            if(null==screen) return;    
            PrintDocument pd = new PrintDocument();
            //pd.OriginAtMargins = true;
            pd.DefaultPageSettings.Landscape = true;
            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            pd.Print();



        }

        private void pd_PrintPage(object sender, PrintPageEventArgs ev){ 
            if(null==screen) return;    
            Image i = screen;

            float newWidth = i.Width * 100 / i.HorizontalResolution;
            float newHeight = i.Height * 100 / i.VerticalResolution;

            float widthFactor = newWidth / ev.MarginBounds.Width;
            float heightFactor = newHeight / ev.MarginBounds.Height;

            if(widthFactor>1 | heightFactor > 1)
            {
                if(widthFactor > heightFactor)
                {
                    newWidth = newWidth / widthFactor;
                    newHeight = newHeight / widthFactor;
                }
                else
                {
                    newWidth = newWidth / heightFactor;
                    newHeight = newHeight / heightFactor;
                }
            }
            ev.Graphics.DrawImage(i, ev.MarginBounds);
        }


         private void dosScreen_Resize(object sender, EventArgs e) {
             dosScreenToString();
          
         }

         private void dosScreen_MouseClick(object sender, MouseEventArgs e) {
         }

         protected override void OnPaint(PaintEventArgs e)
         {
             base.OnPaint(e);
             dosScreenToString();
             if (null!=screen) e.Graphics.DrawImage(screen, 0, 0);
         }
 

         bool highlight=false;
         int selectionX1=0,selectionY1=0,selectionX2=0,selectionY2=0;
         
         private void dosScreen_MouseDown(object sender, MouseEventArgs e) {
            if(!highlight) {
                highlight=true;
                selectionX1=(int)(e.X/columnWidth);
                selectionY1=(int)(e.Y/lineHeight);
                selectionX2=selectionX1;
                selectionY2=selectionY1;
                //screenRedraw.Interval=10;
            }
         }

         private void dosScreen_MouseUp(object sender, MouseEventArgs e) {
             if(highlight) {
                 highlight=false;
                 selectionX2=selectionX1;
                 selectionY2=selectionY1;
                 //screenRedraw.Interval=1000;
             }
         }

        int mouseX=0;
        int mouseY=0;

         private void dosScreen_MouseMove(object sender, MouseEventArgs e) {
             mouseX=(int)(e.X/columnWidth);
             mouseY=(int)(e.Y/lineHeight);
             if(highlight) {
                selectionX2=(int)(e.X/columnWidth);
                selectionY2=(int)(e.Y/lineHeight);
            }
         }

        public byte[] getMouseCooridinates(){
            return  new byte[] { (byte)mouseX,(byte)mouseY};
        }

         private void InitializeComponent()
         {
            this.components = new System.ComponentModel.Container();
            this.redrawTimer = new System.Windows.Forms.Timer(this.components);
            this.blinkTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // redrawTimer
            // 
            this.redrawTimer.Enabled = true;
            this.redrawTimer.Tick += new System.EventHandler(this.redrawTimer_Tick);
            // 
            // blinkTimer
            // 
            this.blinkTimer.Enabled = true;
            this.blinkTimer.Interval = 1000;
            this.blinkTimer.Tick += new System.EventHandler(this.blinkTimer_Tick);
            // 
            // dosScreen
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.DoubleBuffered = true;
            this.Name = "dosScreen";
            this.Size = new System.Drawing.Size(599, 344);
            this.Load += new System.EventHandler(this.dosScreen_Load_1);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dosScreen_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dosScreen_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dosScreen_MouseUp);
            this.ResumeLayout(false);

         }

         private void redrawTimer_Tick(object sender, EventArgs e)
         {
             this.Refresh();
             //dosScreenToString();this.

         }

        public void cls(){
            int w=80;
            int h=25;
            //Loop
            for (byte x = 0; x < w; x++){
                for (byte y = 0; y < h; y++){       //Loop
                    
                    screenData[x*2 + y * w*2] = (byte)' ';
                    screenData[x*2 + y * w*2+1] = 0xF0;
                }
            }
        }

        public void setCharacter(int x, int y, char c)
        {
            int w=80;
            int h=25;
            if(x<0 || y<0 || x>79 || y>24) return;
            int pos=x*2 + y * w*2;
            if(pos>=80*25*2 || pos<0) return;         //safety
            screenData[pos] = (byte)c;
        }

        
        public void setColor(int x, int y, Colors foreground,Colors background)
        {
            int w=80;
            int h=25;
            if(x<0 || y<0 || x>79 || y>24) return;
            int hexValue=(int)foreground+((int)background<<4);
            int pos=x*2 + y * w*2+1;
            if(pos>=80*25*2 || pos<0) return;         //safety
            screenData[pos] = (byte)(hexValue);
        }

         private void dosScreen_Load_1(object sender, EventArgs e)
         {

         }

         private void blinkTimer_Tick(object sender, EventArgs e)
         {
            if (blink) blink = false; else blink = true;
             //update = true;
             this.Refresh();
         }

         public void box(int x1,int y1,int x2,int y2,Colors background,Colors border){
           char c;            
           Colors bg;
           for (int x = x1; x <=x2; x++) { 
           for (int y = y1; y <=y2; y++) { 
               c=' ';
               bg=background;
               if(x==x1) { c=(char)178; bg=border; }
               if(x==x2) { c=(char)178; bg=border; }
               if(y==y1) { c=(char)178; bg=border; }
               if(y==y2) { c=(char)178; bg=border; }
               this.setCharacter (x,y,c);      
               this.setColor     (x,y,Colors.LIGHTGRAY,bg);
            }
            }
        }//end function

        public void box(sObj data){
           char c;            
           Colors bg;
           for (int x = data.x1; x <=data.x2; x++) { 
           for (int y = data.y1; y <=data.y2; y++) { 
               c=' ';
               bg=data.background;
               if(x==data.x1) { c=(char)178; bg=data.foreground; }
               if(x==data.x2) { c=(char)178; bg=data.foreground; }
               if(y==data.y1) { c=(char)178; bg=data.foreground; }
               if(y==data.y2) { c=(char)178; bg=data.foreground; }
               this.setCharacter (x,y,c);      
               this.setColor     (x,y,Colors.LIGHTGRAY,bg);
            }
            }
        }//end function



         public void drawString(int x, int y, string text){
            int px=x,py=y;
             foreach(char c in text){
                 if(c==0x13) continue;
                 if(c==10) {
                     py++;
                     px=x;
                     continue;

                 }
                 this.setCharacter(px,py,c);
                 px++;
             }
         }

         public void drawString(int x, int y, string text,Colors background,Colors foreground){
            int px=x,py=y;
             foreach(char c in text){
                 if(c==0x13) continue;
                 if(c==10) {
                     py++;
                     px=x;
                     continue;

                 }
                 this.setCharacter(px,py,c);
                 this.setColor(px,py,foreground,background);
                 
                 px++;
             }
         }
         
         public class sObj
         {
             public int x1;
             public int y1;
             public int x2;
             public int y2;
             public Colors foreground;
             public Colors background;
             int index;
         
             public void up(){
                 if(y1<=0) return;
                 y1--;
                 y2--;
             }
             public void down(){
                 if(y2>=24) return;
                 y1++;
                 y2++;
             }
            public void left(){
                 if(x1<=0) return;
                 x1--;
                 x2--;
             }
             public void right(){
                 if(x2>=79) return;
                 x1++;
                 x2++;
             }

         }

     
         }//end launch check function on timer
}
