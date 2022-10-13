using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                this.textBox1.Text = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                this.textBox1.SelectionStart = this.textBox1.Text.Length;
            }
            catch
            {
            }
        }
        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }
        private void textBox2_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                this.textBox2.Text = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                this.textBox2.SelectionStart = this.textBox2.Text.Length;
            }
            catch
            {
            }
        }
        private void textBox2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }
        private void button1_selectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = dialog.FileName;
                this.textBox1.SelectionStart = this.textBox1.Text.Length;
            }
        }
        private void button2_selectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox2.Text = dialog.FileName;
                this.textBox2.SelectionStart = this.textBox2.Text.Length;
            }
        }

        //格式 例：
        //函数名称          : ****************
        private void button_goMatch_Click(object sender, EventArgs e)
        {
            try
            {
                string file1 = this.textBox1.Text;
                string file2 = this.textBox2.Text;

                string[] lines1 = System.IO.File.ReadAllLines(file1);
                string[] lines2 = System.IO.File.ReadAllLines(file2);

                List<string> methods1 = new List<string>();
                List<string> methods2 = new List<string>();
                foreach (string line in lines1)
                {
                    if (line.Contains("函数名称"))
                        methods1.Add(line.Split(':')[1].Trim());
                }
                foreach (string line in lines2)
                {
                    if (line.Contains("函数名称"))
                        methods2.Add(line.Split(':')[1].Trim());
                }

                List<string> alone1 = new List<string>();
                List<string> alone2 = new List<string>();
                foreach (string method in methods1)
                {
                    int index = methods2.FindIndex(m => m == method);
                    if (index == -1)
                        alone1.Add(method);
                }
                foreach (string method in methods2)
                {
                    int index = methods1.FindIndex(m => m == method);
                    if (index == -1)
                        alone2.Add(method);
                }

                string name1 = System.IO.Path.GetFileName(file1);
                string name2 = System.IO.Path.GetFileName(file2);
                string res = $"文件 {name1}独有函数列表：\r\n{string.Join("\r\n",alone1)}\r\n文件 {name2}独有函数列表：\r\n{string.Join("\r\n", alone2)}";


                ResultForm resultForm = new ResultForm();
                resultForm.textBox1.Text = res;
                resultForm.textBox1.Select(0,0);
                resultForm.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),"Error");
            }
            
        }


    }


}
