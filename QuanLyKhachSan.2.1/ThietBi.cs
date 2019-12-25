﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Sql;
using System.Data.SqlClient;
namespace QuanLyKhachSan._2._1
{
    public partial class ThietBi : DevExpress.XtraEditors.XtraForm
    {
        public ThietBi()
        {
            InitializeComponent();
            // This line of code is generated by Data Source Configuration Wizard
            thieT_BITableAdapter1.Fill(qlksDataSet1.THIET_BI);
        }

        private void ThietBi_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLKSDataSet.LOAI_PHONG' table. You can move, or remove it, as needed.
            this.lOAI_PHONGTableAdapter.Fill(this.qLKSDataSet.LOAI_PHONG);
            LoadData();
        }
        public void LoadData()
        {
            DataService db = new DataService();
            String sql = "select * from THIET_BI";
            String sql1 = "select MaLoaiPhong from LOAI_PHONG";
            DataTable dt = db.getDataTable(sql);
            cbLoaiPhong.DataSource = db.getDataTable(sql1);
            cbLoaiPhong.ValueMember = "MaLoaiPhong";
            cbLoaiPhong.DisplayMember = "MaLoaiPhong";
            gridControl1.DataSource = dt;


        }
        public void click_them()
        {

            #region ham tang ma tu dong    
            AutoIncreCode a = new AutoIncreCode();
            a.GetLastID("THIET_BI", "MaThietBi");
            string lastid = a.GetLastID("THIET_BI", "MaThietBi");
            string ID = a.NextID(lastid, "TB");
            #endregion
            txtMaTB.Text = ID;
            String sql;
            String madv = txtMaTB.Text;
            String tentb = txtTenTb.Text;
            String soluong = txtSoLuong.Text;
            String loaiphong = cbLoaiPhong.SelectedValue.ToString();
            sql = "insert into THIET_BI(MaThietBi,TenThietBi,MaLoaiPhong,SoLuong) values ('" + madv + "','" + tentb + "','" + loaiphong + "','" + soluong + "')";
            DataService db = new DataService();
            db.executeQuery(sql);
        }
        public bool check_masv()
        {

            String msv = txtMaTB.Text;
            String sql = "Select MaThietBi from THIET_BI  where   MaThietBi ='" + msv + "'";
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AQ4PTGN;Initial Catalog=QLKS;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                MessageBox.Show("Mã này đã có rồi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadData();
                return true;


            }
            else
            {
                return false;

            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (check_masv() == false)
            {
                click_them();
                MessageBox.Show("Bạn đã thêm thành công ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
        }
        public void click_sua()
        {

            String sql;
            String madv = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MaThietBi").ToString();
            String tentb = txtTenTb.Text;
            String soluong = txtSoLuong.Text;
            String loaiphong = cbLoaiPhong.SelectedValue.ToString();
            sql = "update THIET_BI set  TenThietBi=N'" + tentb + "' ,MaLoaiPhong='" + loaiphong + "',SoLuong='" + soluong + "' where MaThietBi='" + madv + "'";
            DataService db = new DataService();
            db.executeQuery(sql);

        }

        private void bntSua_Click(object sender, EventArgs e)
        {
            click_sua();
            LoadData();
            MessageBox.Show("bạn đã sửa thông tin   ", "thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void click_xoa()
        {
            String sql;
            String madv = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MaThietBi").ToString();
            sql = "delete THIET_BI where MaThietBi='" + madv + "'";
            DataService db = new DataService();
            db.executeQuery(sql);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn xóa một sinh viên !!!", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {

                LoadData();
            }

            click_xoa();
            LoadData();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            DataService db = new DataService();
            string sql = "select * from THIET_BI ";
            DataTable dt = db.getDataTable(sql);
            if (txtTimkiem.Text != " ")
            {
                dt.DefaultView.RowFilter = "MaThietBi LIKE '%" + txtTimkiem.Text + "%' or TenThietBi LIKE '%" + txtTimkiem.Text + "%' ";

            }
            else
            {
                dt.DefaultView.RowFilter = " ";
            }
            gridControl1.DataSource = dt;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            txtMaTB.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MaThietBi").ToString();
            txtTenTb.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TenThietBi").ToString();
            cbLoaiPhong.ValueMember = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MaLoaiPhong").ToString();
            txtSoLuong.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SoLuong").ToString();
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Vui lòng nhập dữ liệu đúng", "Warning", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning);
            }
        }
    }
}