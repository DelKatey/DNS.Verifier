using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DNS_Verifier
{
    public partial class IPList : Form
    {
        public IPList()
        {
            InitializeComponent();
        }

        public IPList(string results)
        {
            InitializeComponent();
            displayTextBox.Text = results;
        }

        public IPList(string results, string title)
        {
            InitializeComponent();
            this.Text = title;
            displayTextBox.Text = results;
        }

        internal static DialogResult Show(string text)
        {//http://stackoverflow.com/questions/2354164/c-sharp-use-form-instead-of-messagebox

            IPList form = new IPList(text);
            return form.ShowDialog();
        }

        internal static DialogResult Show(string text, string title)
        {//http://stackoverflow.com/questions/2354164/c-sharp-use-form-instead-of-messagebox

            IPList form = new IPList(text, title);
            return form.ShowDialog();
        }
    }
}
