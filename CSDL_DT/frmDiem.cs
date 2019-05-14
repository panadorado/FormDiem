using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSDL_DT
{
    public partial class frmDiem : Form
    {
        public frmDiem()
        {
            InitializeComponent();
        }

        private void frmDiem_Load(object sender, EventArgs e)
        {
            Function_Connect.connect();
            Load_DataGrid();
        }

        public void Load_DataGrid()
        {
            string sql = "select tblDiem.MaSV,tblDiem.MaMon,tblSinhVien.HoTen,tblSinhVien.NgaySinh,tblDiem.Diem from tblDiem inner join tblSinhVien on tblSinhVien.MaSV = tblDiem.MaSV";
            dataGridView1.DataSource = Function_Connect.getdata(sql);
            dataGridView1.Columns[0].HeaderText = "MaSV";
            dataGridView1.Columns[1].HeaderText = "MaMH";
            dataGridView1.Columns[2].HeaderText = "TenSV";
            dataGridView1.Columns[3].HeaderText = "Ngay Sinh";
            dataGridView1.Columns[4].HeaderText = "Diem";

            dataGridView1.Columns[0].Width = 120;
            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 120;
            dataGridView1.Columns[4].Width = 110;
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {

        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

        }
    }
}
