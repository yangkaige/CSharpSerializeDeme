using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MySerialize
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private List<DataItem> list = new List<DataItem>();
        /// <summary>
        /// 退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            //序列化数据
            using (FileStream fsWrite = new FileStream("_data.bin", FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fsWrite, list);
            }
            Application.Exit();
        }
        /// <summary>
        /// 保存修改的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                MessageBox.Show("标题不能为空");
                return;
            }
            if (string.IsNullOrEmpty(txtContent.Text))
            {
                MessageBox.Show("内容不能为空");
                return;
            }
            int index = listBox.SelectedIndex;
            DataItem dataItem = new DataItem() { Title = txtTitle.Text, Content = txtContent.Text };
            if (index > 0)
            {
                list.RemoveAt(index);
                list.Insert(index, dataItem);
            }
            else
            {
                list.Add(dataItem);
            }

            txtTitle.Text = "";
            txtContent.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (FileStream fsRead = new FileStream("_data.bin", FileMode.OpenOrCreate, FileAccess.Read))
            {
                BinaryFormatter bf = new BinaryFormatter();
                list = (List<DataItem>)bf.Deserialize(fsRead);
                foreach (var item in list)
                {
                    listBox.Items.Add(item.Title);
                }
            }
        }
        /// <summary>
        /// 选中列表控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBox.SelectedIndex;
            DataItem dataItem = list[index];
            txtTitle.Text = dataItem.Title;
            txtContent.Text = dataItem.Content;
        }
    }
}
