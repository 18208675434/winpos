using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QDAMAPOS
{
    public partial class FrmNumberCenter : Form
    {
        Image backimg;
        public FrmNumberCenter()
        {
            InitializeComponent();
        }

        public FrmNumberCenter(Image img)
        {
            InitializeComponent();
            backimg = img;
            
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }

        private void FrmNumberCenter_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = backimg;
        }

    }
}
