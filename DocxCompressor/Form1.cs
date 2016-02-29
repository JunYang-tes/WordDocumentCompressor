using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Compressor;
using System.Diagnostics;

namespace DocxCompressor
{
    public partial class Form1 : Form
    {
        Compressor.DocCompressor docCompressor = new Compressor.DocCompressor();
        bool isProgressing = false;
        public Form1()
        {
            init();
        }
        public Form1(String file) {
            init();
            Start(file);
        }

        private void init() {
            InitializeComponent();
            docCompressor.PregressReport += new EventHandler<Compressor.Events.ProgressReportor>(docCompressor_PregressReport);
            docCompressor.SubPregressReport += new EventHandler<Compressor.Events.ProgressReportor>(docCompressor_SubPregressReport);
            label5.DataBindings.Add("Text", trackBar1, "Value");
        
        }


        void docCompressor_SubPregressReport(object sender, Compressor.Events.ProgressReportor e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                lbSubInfo.Text = string.Format("{0} {1}/{2}",e.Text,e.Done,e.Totle);
                progressBar2.Maximum = e.Totle;
                progressBar2.Value = e.Done;
            });
        }

        void docCompressor_PregressReport(object sender, Compressor.Events.ProgressReportor e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                lbInfo.Text = string.Format("{0} {1}/{2}", e.Text, e.Done, e.Totle);
                progressBar1.Maximum = e.Totle;
                progressBar1.Value = e.Done;
            });
        }


        private void Start(string file)
        {

            isProgressing = true;
            new Thread((ThreadStart)delegate
            {
                Compressor.CompressOptions op = new Compressor.CompressOptions();
                panel1.Invoke((MethodInvoker)delegate
                {
                    panel1.Visible = true;
                    panel2.Visible = false;
                    op.Quality = 100-trackBar1.Value;
                });
                try
                {

                   Result ret= docCompressor.execute(file,
                        op);
                   if (MessageBox.Show(
                       String.Format("压缩完成，压缩率：{0},新文件位置:{1},是否打开该目录？", ret.Rate, ret.OutFile)
                       , "提示", MessageBoxButtons.YesNo)
                       == DialogResult.Yes) {
                           Process.Start(
                               System.IO.Path.GetDirectoryName(ret.OutFile));
                   }
                }
                catch (Compressor.Exceptions.ComponentNotFound ex)
                {
                    MessageBox.Show("必要组件" + ex.Name + "丢失或损坏，重建安装可能会解决这个问题");
                }
                catch (Compressor.Exceptions.UnKnowException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Compressor.Exceptions.Unsupport)
                {
                    MessageBox.Show("尚不支持该格式");
                }
                finally
                {
                    isProgressing = false;
                    panel1.Invoke((MethodInvoker)delegate
                    {
                        panel1.Visible = false;
                        panel2.Visible = true;
                    });
                }
            }).Start();
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (isProgressing)
            {
                MessageBox.Show("请等待当前文件处理完成");
            }
            else
            {
                Start(((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString());
            }
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else e.Effect = DragDropEffects.None;
        }

      
    }
}
