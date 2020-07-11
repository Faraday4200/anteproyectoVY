using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;

namespace Practico3puertos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == false)
            {
                serialPort1.BaudRate = 9600;
                serialPort1.DataBits = 8;
                serialPort1.Parity = Parity.None;
                serialPort1.PortName = comboBox1.SelectedItem.ToString();
                serialPort1.Open();
                textBox1.Text = "Conectado";

                MessageBox.Show("Puerto conectado");
                textBox1.BackColor = Color.Lime;

                timer1.Enabled = true;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();

                timer1.Enabled = false;
                textBox1.Text = "Desconectado";

                MessageBox.Show("Puerto Desconectado");
                textBox1.BackColor = Color.Red;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string msj = richTextBox1.Text;

            List<string> lista = new List<string>(msj.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));
            int i;
            for (i=0; i<lista.Count; i++)
            {
                serialPort1.Write(lista[i]);
                serialPort1.Write("\r");
                Thread.Sleep(5000);
            }
              
            
            serialPort1.Write(richTextBox1.Text);
            serialPort1.Write("\r");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            mensaje = serialPort1.ReadExisting();
            mensaje = mensaje.Trim();

            if (mensaje.Length > 0)
            {
               if(mensaje.Contains("Done.") || mensaje.Contains(">"))
                {
                    if (mensaje == ">")
                    {
                        richTextBox2.Text += mensaje + ("**Realizado  \n> **");
                    }
                    else
                    {
                        richTextBox2.Text += ("** No Realizado  \n> **") + mensaje;
                    }
                }
               
            }
        }

        string mensaje;

        private void button4_Click(object sender, EventArgs e)
        {
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(saveFileDialog1.FileName + " .txt", richTextBox2.Lines);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            

                serialPort1.Write("open");
                serialPort1.Write("\r");
                richTextBox1.Text += "abrir pinza" + "\n";
            

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
                serialPort1.Write("close");
                serialPort1.Write("\r");
                richTextBox1.Text += "cerrar pinza " + "\n";
            

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }

}

