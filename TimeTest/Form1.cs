using System.Windows.Forms;

namespace TimeTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            TimeLib.Time time1 = ReadTime1();

            if (time1 == null) { return; }

            TimeLib.Time time2 = ReadTime2();

            if (time2 == null) { return; }

            TimeLib.Time time3 = time1 + time2;

            ShowTime3(time3.GetTime());
        }

        private void btnSubtract_Click(object sender, System.EventArgs e)
        {
            TimeLib.Time time1 = ReadTime1();

            if (time1 == null) { return; }

            TimeLib.Time time2 = ReadTime2();

            if (time2 == null) { return; }

            TimeLib.Time time3 = time2 - time1;

            ShowTime3(time3.GetTime());
        }

        private void btnCompare_Click(object sender, System.EventArgs e)
        {
            TimeLib.Time time1 = ReadTime1();

            if (time1 == null) { return; }

            TimeLib.Time time2 = ReadTime2();

            if (time2 == null) { return; }

            lblLeftGtRight.Text = time1 > time2 ? "بله" : "خیر";

            lblLeftGteRight.Text = time1 >= time2 ? "بله" : "خیر";

            lblLeftLtRight.Text = time1 < time2 ? "بله" : "خیر";

            lblLeftLteRight.Text = time1 <= time2 ? "بله" : "خیر";

            lblLeftEqRight.Text = time1 == time2 ? "بله" : "خیر";

            lblLeftNeqRight.Text = time1 != time2 ? "بله" : "خیر";
        }

        private void btnIncrease_Click(object sender, System.EventArgs e)
        {
            TimeLib.Time time1 = ReadTime1();

            if (time1 == null) { return; }

            time1++;

            ShowTime1(time1.GetTime());
        }

        private void btnDecrease_Click(object sender, System.EventArgs e)
        {
            TimeLib.Time time1 = ReadTime1();

            if (time1 == null) { return; }

            time1--;

            ShowTime1(time1.GetTime());
        }

        private void btnPlusNumber_Click(object sender, System.EventArgs e)
        {
            TimeLib.Time time1 = ReadTime1();

            if (time1 == null) { return; }

            int n = 0;

            if (!int.TryParse(txtNumber.Text, out n))
            {
                MessageBox.Show("عدد وارد شده صحیح نیست", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtNumber.Focus();

                return;
            }

            time1 = time1 + n;

            ShowTime1(time1.GetTime());
        }

        private void btnMinusNumber_Click(object sender, System.EventArgs e)
        {
            TimeLib.Time time1 = ReadTime1();

            if (time1 == null) { return; }

            int n = 0;

            if (!int.TryParse(txtNumber.Text, out n))
            {
                MessageBox.Show("عدد وارد شده صحیح نیست", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtNumber.Focus();

                return;
            }

            time1 = time1 - n;

            ShowTime1(time1.GetTime());
        }

        bool ValidateTime1()
        {
            int hour, minute, second;

            return int.TryParse(txtTime1Hour.Text, out hour) && int.TryParse(txtTime1Minute.Text, out minute) && int.TryParse(txtTime1Second.Text, out second);
        }

        bool ValidateTime2()
        {
            int hour, minute, second;

            return int.TryParse(txtTime2Hour.Text, out hour) && int.TryParse(txtTime2Minute.Text, out minute) && int.TryParse(txtTime2Second.Text, out second);
        }

        TimeLib.Time ReadTime1()
        {
            if (!ValidateTime1())
            {
                MessageBox.Show("ساعت اول معتبر نمی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }

            TimeLib.TimeStruct time;

            int.TryParse(txtTime1Hour.Text, out time.Hour);

            int.TryParse(txtTime1Minute.Text, out time.Minute);

            int.TryParse(txtTime1Second.Text, out time.Second);

            return new TimeLib.Time(time);
        }

        TimeLib.Time ReadTime2()
        {
            if (!ValidateTime1())
            {
                MessageBox.Show("ساعت دوم معتبر نمی باشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }

            TimeLib.TimeStruct time;

            int.TryParse(txtTime2Hour.Text, out time.Hour);

            int.TryParse(txtTime2Minute.Text, out time.Minute);

            int.TryParse(txtTime2Second.Text, out time.Second);

            return new TimeLib.Time(time);
        }

        void ShowTime1(TimeLib.TimeStruct time)
        {
            txtTime1Hour.Text = time.Hour.ToString();

            txtTime1Minute.Text = time.Minute.ToString();

            txtTime1Second.Text = time.Second.ToString();
        }

        void ShowTime3(TimeLib.TimeStruct time)
        {
            txtTime3Hour.Text = time.Hour.ToString();

            txtTime3Minute.Text = time.Minute.ToString();

            txtTime3Second.Text = time.Second.ToString();
        }
    }
}