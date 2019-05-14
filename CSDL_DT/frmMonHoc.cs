using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CSDL_DT
{
    public partial class frmMonHoc : Form
    {
        public frmMonHoc()
        {
            InitializeComponent();
        }

        DataTable tbl;
        private void frmMonHoc_Load(object sender, EventArgs e)
        {
            Function_Connect.connect();
            Load_DataGrid();
            Load_CBB();


            txtb_Mamon.Enabled = false;
            txtb_Tenmon.Enabled = false;
            cbb_Makhoa.Enabled = false;
            txtb_Sohocphan.Enabled = false;
            txtb_Giaovien.Enabled = false;
            btn_Them.Visible = false; 
        }

        private void frmMonHoc_Click(object sender, EventArgs e)
        {
            btn_Sua.Enabled = false;
            btn_Xoa.Enabled = false;
        }

        public void Load_DataGrid()
        {
            string sql = "SELECT * FROM tblMonHoc";
            tbl = Function_Connect.getdata(sql);
            dataGridView1.DataSource = tbl;
            dataGridView1.Columns[0].HeaderText = "Mã môn";
            dataGridView1.Columns[1].HeaderText = "Tên môn";
            dataGridView1.Columns[2].HeaderText = "Mã khoa";
            dataGridView1.Columns[3].HeaderText = "Số phần học";
            dataGridView1.Columns[4].HeaderText = "Giáo viên";

            dataGridView1.Columns[0].Width = 65;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 65;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 166;
        }

        public void Load_CBB()
        {
            string sql_cbKhoa = "SELECT * FROM tblKhoa";
            cbb_Makhoa.DataSource = Function_Connect.getdata(sql_cbKhoa);
            cbb_Makhoa.DisplayMember = "MaKhoa";
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            int id;
            id = dataGridView1.CurrentCell.RowIndex;
            txtb_Mamon.Text = dataGridView1.Rows[id].Cells[0].Value.ToString();
            txtb_Tenmon.Text = dataGridView1.Rows[id].Cells[1].Value.ToString();
            cbb_Makhoa.Text = dataGridView1.Rows[id].Cells[2].Value.ToString();
            txtb_Sohocphan.Text = dataGridView1.Rows[id].Cells[3].Value.ToString();
            txtb_Giaovien.Text = dataGridView1.Rows[id].Cells[4].Value.ToString();
            btn_Sua.Enabled = true;
            btn_Xoa.Enabled = true;
        }

        private void btn_Capnhat_Click(object sender, EventArgs e)
        {
            txtb_Mamon.Text = "";
            txtb_Tenmon.Text = "";
            txtb_Sohocphan.Text = "";
            txtb_Giaovien.Text = "";

            txtb_Mamon.Enabled = true;
            txtb_Tenmon.Enabled = true;
            cbb_Makhoa.Enabled = true;
            txtb_Sohocphan.Enabled = true;
            txtb_Giaovien.Enabled = true;

            btn_Capnhat.Visible = false;
            btn_Them.Visible = true;
        }
        private void btn_Them_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtb_Mamon.Text.Trim().Length == 0 || 
                txtb_Tenmon.Text.Trim().Length == 0 || 
                txtb_Sohocphan.Text.Trim().Length == 0 || 
                txtb_Giaovien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập đầy đủ thông tin !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Kiểm tra key Mã Chất Liệu
                sql = "Select Mamon From tblMonhoc where Mamon='" + txtb_Mamon.Text.Trim() + "'";
                if (Function_Connect.checkkey(sql))
                {
                    MessageBox.Show("Thông tin bạn cập nhập đã tồn tại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtb_Mamon.Focus();
                    return;
                }
                // Thêm dữ liệu
                if (MessageBox.Show("Bạn có muốn thêm không", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    sql = "INSERT INTO tblMonHoc (Mamon, Tenmon, Makhoa, Sohocphan, Giaovien) VALUES ('" 
                        + txtb_Mamon.Text + "','" + txtb_Tenmon.Text + "','" + cbb_Makhoa.Text + "','" + txtb_Sohocphan.Text + "','" + txtb_Giaovien.Text + "')";
                    Function_Connect.runsql(sql);
                    Load_DataGrid();

                    MessageBox.Show("Thêm thành công !");

                }
            }
            txtb_Mamon.Enabled = false;
            txtb_Tenmon.Enabled = false;
            cbb_Makhoa.Enabled = false;
            txtb_Sohocphan.Enabled = false;
            txtb_Giaovien.Enabled = false;

            btn_Them.Visible = false;
            btn_Capnhat.Visible = true;
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show("Bạn có muốn xoá không", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string sql = "DELETE tblMonhoc WHERE Mamon = '" + txtb_Mamon.Text + "'";
                Function_Connect.runsql(sql);
                Load_DataGrid();

                MessageBox.Show("Thêm thành công !");

            }
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn sửa không", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "UPDATE tblMonHoc SET Tenmon = '" 
                    +txtb_Tenmon.Text+ "', Makhoa = '" +cbb_Makhoa.Text+ "', Sohocphan = '" 
                    +txtb_Sohocphan.Text+ "', Giaovien = '" +txtb_Giaovien.Text+ "'WHERE Mamon = '" +txtb_Mamon.Text+ "'";
                Function_Connect.runsql(sql);
                Load_DataGrid();

                MessageBox.Show("Sửa thành công !");
            }
        }
    }
}
