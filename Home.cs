using CafeElAngel.Data.Models;
using CafeElAngel.Data;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeElAngel
{
    public partial class Home : Form
    {
        Reservaciones reservacion = new Reservaciones();
        DashboardDB conec = new DashboardDB();

        public Home()
        {
            InitializeComponent();
            customizeDesign();
        }

        private void customizeDesign()
        {
            subMenuReservas.Visible = false;
            subMenuPedidos.Visible = false;
            subMenuCuadres.Visible = false;
        }

        private void hideSubMenu()
        {
            if (subMenuReservas.Visible = true) 
            {
                subMenuReservas.Visible = false;
            }
            if (subMenuPedidos.Visible = true)
            {
                subMenuPedidos.Visible = false;
            }
            if (subMenuCuadres.Visible = true)
            {
                subMenuCuadres.Visible = false;
            }
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }

        private void btnReservaciones_Click(object sender, EventArgs e)
        {
            showSubMenu(subMenuReservas);
        }

        private void btnRegistrarReserva_Click(object sender, EventArgs e)
        {
            openChildForm(new AgregarReserva());
            
            hideSubMenu();
        }

        private void btnPedidos_Click(object sender, EventArgs e)
        {
            showSubMenu(subMenuPedidos);
        }

        private void btnCuadres_Click(object sender, EventArgs e)
        {
            showSubMenu(subMenuCuadres);
        }

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if(activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChild.Controls.Add(childForm);
            panelChild.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnConsultarReserva_Click(object sender, EventArgs e)
        {
            openChildForm(new Reservas());
            hideSubMenu();
        }

        private void btnActualizarReserva_Click(object sender, EventArgs e)
        {
            openChildForm(new ActualizarReserva()); 
            hideSubMenu();
        }


        private void LoadReservasHoy()
        {
            DataTable reservasHoy = conec.LeerTablaReservasHoy();
            dataGridReservasHoy.DataSource = reservasHoy;
        }


        private void InitializeTimer()
        {
            updateTimer = new Timer();
            updateTimer.Interval = 30000; 
            updateTimer.Tick += new EventHandler(updateTimer_Tick);
            updateTimer.Start();
        }


        private void Home_Load(object sender, EventArgs e)
        {
            LoadReservasHoy();
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            LoadReservasHoy();
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            LoadReservasHoy();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Form1 loggin = new Form1();
            loggin.Show();
            this.Hide();
        }
    }
}
