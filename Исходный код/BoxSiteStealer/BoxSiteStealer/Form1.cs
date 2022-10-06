using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;





using System.Diagnostics;
using System.Runtime.InteropServices;




namespace BoxSiteStealer
{
    public partial class Form1 : Form
    {
        //перемнная для пути сохранения
        string path = System.IO.Path.Combine(Application.StartupPath, "");

        //ссылка на сайт

        string linksite;


        //импорт библеотеки и создание класса для скругление

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );




        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //скругление кнопки скачать
            button1.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 20, 20));
        }

        //путь сохранения
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    path = dialog.SelectedPath;
                    textBox2.Text = path.ToString();
                }

            }
        }

        //скачивание сайта
        private void button1_Click(object sender, EventArgs e)
        {
            linksite = textBox1.Text.ToString();

            if (checkBox1.Checked == true) //скачать только выбранную страницу
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                //Имя запускаемого приложения
                psi.FileName = "cmd";
                //команда, которую надо выполнить
                psi.Arguments = $" @\"/c wget -k -l 7 -p -E -nc {linksite} --no-check-certificate  -P {path} --user-agent=\"Mozilla/5.0 (Macintosh; Intel Mac OS X 10.8; rv:21.0) Gecko/20100101 Firefox/21.0\" ";
                //  /c - после выполнения команды консоль закроется
                //  /к - не закрывать консоль после выполнения команды
                Process.Start(psi);
            }
            else //скачать сайт целиком
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                //Имя запускаемого приложения
                psi.FileName = "cmd";
                //команда, которую надо выполнить
               // psi.Arguments = $" @\"/k wget  --page-requisites -r -l 10 --no-check-certificate {linksite} -P {path}";
                psi.Arguments = $" @\"/c wget -r -k -l 7 -p -E -nc {linksite} --no-check-certificate -P {path} --user-agent=\"Mozilla/5.0 (Macintosh; Intel Mac OS X 10.8; rv:21.0) Gecko/20100101 Firefox/21.0\" ";
                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;
                //  /c - после выполнения команды консоль закроется
                //  /к - не закрывать консоль после выполнения команды
                Process.Start(psi);
            }

            textBox1.Text = ""; 

         }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/spbkit1337");
        }
    }


}

