using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using ZhuiZhi_Integral_Scale_UncleFruit.Common;

namespace ZhuiZhi_Integral_Scale_UncleFruit.MyControl
{
    public partial class KeyBoard : System.Windows.Forms.UserControl
    {

        //功能键的文字内容
        public string KeyDelete { get; set; }
        public string KeyHide { get; set; }
        public string KeyEnter { get; set; }
        public string KeyClear { get; set; }

        public delegate void KeyboardHandler(object sender, KeyboardArgs e);
        public event KeyboardHandler Press;


        public KeyBoard()
        {
            InitializeComponent();
            BindEnvent();
            KeyDelete = btnDel.Text;
            KeyHide = btnHide.Text;
            KeyEnter = btnEnter.Text;
            KeyClear = btnClear.Text;
        }


        private void BindEnvent()
        {
            foreach (Control ctl in this.Controls)
                if (ctl is Button)
                    ctl.Click += KeyboardPress;
        }


        private void KeyboardPress(Object sender, EventArgs e)
        {
            OnKeyboardPress(new KeyboardArgs(((Button)sender).Text));
        }

        private void OnKeyboardPress(KeyboardArgs e)
        {
            KeyboardHandler temp = Press;
            if (temp != null)
                temp(this, e);
        }

        public class KeyboardArgs : EventArgs
        {
            public string KeyCode { get; private set; }
            public KeyboardArgs(string code)
            {
                KeyCode = code;
            }
        }




        private void KeyBoard_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                wScale = (float)this.Width / 430;
                hScale = (float)this.Height / 700;
                ControlAutoSize(this);
            }
            catch (Exception ex)
            {

            }
        }


        /// <summary>
        /// 声明结构 记录控件位置和大小
        /// </summary>
        public struct ControlRect
        {
            public int Left;
            public int Top;
            public int Width;
            public int Height;
            public float Size;
        }

        public List<ControlRect> _oldCtrl = new List<ControlRect>();
        private int _ctrlNo = 0;

        private void AddControl(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                ControlRect cR;
                cR.Left = c.Left;
                cR.Top = c.Top;
                cR.Width = c.Width;
                cR.Height = c.Height;
                cR.Size = c.Font.Size;
                _oldCtrl.Add(cR);

                // 控件可能嵌套子控件
                if (c.Controls.Count > 0)
                    AddControl(c);
            }
        }


        float wScale = 1;
        float hScale = 1;
        public void ControlAutoSize(Control mForm)
        {
            if (_ctrlNo == 0)
            {
                ControlRect cR;
                cR.Left = mForm.Left;
                cR.Top = mForm.Top;
                cR.Width = mForm.Width;
                cR.Height = mForm.Height;
                cR.Size = mForm.Font.Size;

                _oldCtrl.Add(cR);

                AddControl(mForm);
            }

            _ctrlNo = 1;
          
            AutoScaleControl(mForm, wScale, hScale);
        }

        private void AutoScaleControl(Control mForm, float wScale, float hScale)
        {
            int ctrlLeft, ctrlTop, ctrlWidth, ctrlHeight;
            float ctrlFontSize, hSize, wSize;

            string debug = "";
            string tet = debug;
            string name = mForm.Name;

            foreach (Control c in mForm.Controls)
            {
                ctrlLeft = _oldCtrl[_ctrlNo].Left;
                ctrlTop = _oldCtrl[_ctrlNo].Top;
                ctrlWidth = _oldCtrl[_ctrlNo].Width;
                ctrlHeight = _oldCtrl[_ctrlNo].Height;
                ctrlFontSize = _oldCtrl[_ctrlNo].Size;

                c.Left = (int)Math.Ceiling(ctrlLeft * wScale);
                c.Top = (int)Math.Ceiling(ctrlTop * hScale);
                c.Width = (int)Math.Ceiling(ctrlWidth * wScale);
                c.Height = (int)Math.Ceiling(ctrlHeight * hScale);

                wSize = ctrlFontSize * wScale;
                hSize = ctrlFontSize * hScale;
                c.Font = new Font(c.Font.Name, Math.Min(hSize, wSize), c.Font.Style, c.Font.Unit);

                _ctrlNo++;

            }
        }

       
    }
}
