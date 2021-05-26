using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_System
{
    public partial class ManageReservationsForm : Form
    {
        public ManageReservationsForm()
        {
            InitializeComponent();
        }

        Room room = new Room();
        Reservation reservation = new Reservation();
        private void ManageReservationsForm_Load(object sender, EventArgs e)
        {
            //display room's type
            cbRoomType.DataSource = room.RoomTypeList();
            cbRoomType.DisplayMember = "label";
            cbRoomType.ValueMember = "id";

            //display free room's number depending on selected type
            int type = Convert.ToInt32(cbRoomType.SelectedValue.ToString());
            cbRoomNumber.DataSource = room.RoomByType(type);
            cbRoomNumber.DisplayMember = "number";
            cbRoomNumber.ValueMember = "number";

            dgvReservations.DataSource = reservation.GetAllReservations();

        }

        private void btnClearFields_Click(object sender, EventArgs e)
        {
            tbReservID.Text = "";
            tbClientID.Text = "";
            cbRoomType.SelectedIndex = 0;
            dateTimePickerIN.Value = DateTime.Now;
            dateTimePickerOUT.Value = DateTime.Now;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int clientID = Convert.ToInt32(tbClientID.Text);
                int roomNumber = Convert.ToInt32(cbRoomNumber.SelectedValue);
                DateTime dateIn = dateTimePickerIN.Value;
                DateTime dateOut = dateTimePickerOUT.Value;

                if (dateIn < DateTime.Now)
                {
                    MessageBox.Show("The Date must be Greater Or Equal than Current Date", "Invalid Date In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dateOut < dateIn)
                {
                    MessageBox.Show("The DateOut must be Greater Or Equal than DateIn", "Invalid Date Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (reservation.MakeReservation(roomNumber, clientID, dateIn, dateOut))
                    {
                        room.SetRoomFree(roomNumber,"NO");
                        dgvReservations.DataSource = reservation.GetAllReservations();
                        MessageBox.Show("Reservation made successfully!", "Make Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnClearFields.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("ERROR - Reservation not added!", "Make Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Make Reservation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int reservationID = Convert.ToInt32(tbReservID.Text);
                int clientID = Convert.ToInt32(tbClientID.Text);
                int roomNumber = Convert.ToInt32(dgvReservations.CurrentRow.Cells[1].Value.ToString());
                DateTime dateIn = dateTimePickerIN.Value;
                DateTime dateOut = dateTimePickerOUT.Value;

                if (dateIn < DateTime.Now)
                {
                    MessageBox.Show("The Date must be Greater Or Equal than Current Date", "Invalid Date In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dateOut < dateIn)
                {
                    MessageBox.Show("The DateOut must be Greater Or Equal than DateIn", "Invalid Date Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (reservation.EditReservation(reservationID,roomNumber, clientID, dateIn, dateOut))
                    {
                        room.SetRoomFree(roomNumber,"NO");
                        dgvReservations.DataSource = reservation.GetAllReservations();
                        MessageBox.Show("Reservation data updated!", "Edit Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("ERROR - Reservation not updated!", "Edit Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Edit Reservation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

            try
            {
                int reservationID = Convert.ToInt32(tbReservID.Text);
                int roomNumber = Convert.ToInt32(dgvReservations.CurrentRow.Cells[1].Value.ToString());

                if (reservation.RemoveReservation(reservationID))
                {
                    room.SetRoomFree(roomNumber, "YES");
                    dgvReservations.DataSource = reservation.GetAllReservations();
                    MessageBox.Show("Reservation deleted successfully!", "Reservation Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClearFields.PerformClick();
                }
                else
                {
                    MessageBox.Show("ERROR - Reservation not deleted!", "Reservation Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete reservation error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //display room's number depending on selected type
                int type = Convert.ToInt32(cbRoomType.SelectedValue.ToString());
                cbRoomNumber.DataSource = room.RoomByType(type);
                cbRoomNumber.DisplayMember = "number";
                cbRoomNumber.ValueMember = "number";
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Room number error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvReservations_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbReservID.Text = dgvReservations.CurrentRow.Cells[0].Value.ToString();

            //room ID
            int roomID = Convert.ToInt32(dgvReservations.CurrentRow.Cells[1].Value.ToString());

            //room type from ddl
            cbRoomType.SelectedValue = room.GetRoomType(roomID);

            //room number from ddl
            cbRoomNumber.SelectedValue = roomID;

            tbClientID.Text = dgvReservations.CurrentRow.Cells[2].Value.ToString();

            dateTimePickerIN.Value = Convert.ToDateTime(dgvReservations.CurrentRow.Cells[3].Value.ToString());
            dateTimePickerOUT.Value = Convert.ToDateTime(dgvReservations.CurrentRow.Cells[4].Value.ToString());


        }
    }
}
