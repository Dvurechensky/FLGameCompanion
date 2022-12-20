using Freelancer_Companion_by_Dormammu.Data;
using Freelancer_Companion_by_Dormammu.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Freelancer_Companion_by_Dormammu
{
    public partial class FreelancerCompanionDvurechensky : Form
    {
        private LogService LogService { get; set; }
        private SystemService SystemService { get; set; }

        public FreelancerCompanionDvurechensky()
        {
            InitializeComponent();

            LogService = new LogService(LoggerTextBox);

            InitializeSystems();
            InitializeData();
        }

        /// <summary>
        /// Загрузка данных систем
        /// </summary>
        private void InitializeSystems()
        {
            SystemService = new SystemService();
            SystemService.GetInfo(comboBoxSystems, LogService);
            LogService.LogEvent("Список ID систем определён");
            labelSystemss.Text = comboBoxSystems.Items.Count.ToString();
            if(comboBoxSystems.SelectedIndex > 0)
                comboBoxSystems.SelectedIndex = 0;
        }

        private void InitializeData()
        {
            string loadedString = SystemService.ExtractStringFromDLL("SBM3.dll", 534601);
        }


        /// <summary>
        /// Отрисовка первоначального вида карты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Map_Paint(object sender, PaintEventArgs e)
        {
            int w = Map.ClientSize.Width / 2;
            int h = Map.ClientSize.Height / 2;

            //Смещение начала координат в центр PictureBox
            e.Graphics.TranslateTransform(w, h);

            //отрисовка осей и стрелок
            var drawService = new DrawService(5, 3);
            drawService.DrawXAxis(new Point(-w, 0), new Point(w, 0), e.Graphics);
            drawService.DrawYAxis(new Point(0, h), new Point(0, -h), e.Graphics);

            //Центр координат
            e.Graphics.FillEllipse(Brushes.Red, -2, -2, 4, 4);
        }
    }
}
