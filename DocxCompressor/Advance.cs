using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DocxCompressor
{
    public partial class Advance : Form
    {
        List<DataItem> datasource;
        public Advance()
        {
            InitializeComponent();
            datasource = new List<DataItem>();
            for (int j = 0; j < 10; j++)
            {
                DataItem i = new DataItem("1");
                datasource.Add(i);

                i.EnableQuality = true;
            }
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = datasource;

        }


    }
    public class DataItem : Compressor.Strategy
    {
        public Image Image { get; set; }
        public DataItem(string id)
            : base(id)
        {
             
        }

        
        public new int Quality { get; private set; }

        [DisplayName("压缩度")]
        public int Level
        {
            get { return 100 - Quality; }
            set
            {
                if (value > 90 || value < 0) {
                    throw new ArgumentException("Value not in range");
                }
                Quality = 100 - value;
            }
        }

    }
}
