using Freelancer_Companion_by_Dormammu.Data;
using Freelancer_Companion_by_Dormammu.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Freelancer_Companion_by_Dormammu
{
    /// <summary>
    /// Нужно положить в корень приложения  (из Freelancer)
    /// Папку - SYSTEMS
    /// Файл - universe.ini
    /// Dll - nameresources.dll, SBM.dll, SBM2.dll, SBM3.dll
    /// </summary>
    public partial class FreelancerCompanionDvurechensky : Form
    {
        private LogService LogService { get; set; }
        private DrawService DrawService { get; set; }
        private SystemService SystemService { get; set; }
        private List<ObjectSystem> ObjectPoints { get; set; } 

        public FreelancerCompanionDvurechensky()
        {
            InitializeComponent();

            LogService = new LogService(LoggerRichTextBox);//LoggerTextBox

            InitializeSystems();
        }

        /// <summary>
        /// Загрузка данных систем
        /// </summary>
        private void InitializeSystems()
        {
            SystemService = new SystemService();
            DrawService = new DrawService(5, 3);
            SystemService.GetInfo(comboBoxSystems, LogService);
            labelSystemss.Text = comboBoxSystems.Items.Count.ToString();
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
            DrawService.DrawXAxis(new Point(-w, 0), new Point(w, 0), e.Graphics);
            DrawService.DrawYAxis(new Point(0, h), new Point(0, -h), e.Graphics);
            //Центр координат
            e.Graphics.FillEllipse(Brushes.Red, -2, -2, 4, 4);
        }

        //первая координата это X отрицательно влево - положительно вправо
        //вторая коордианат это Y отрицательно вверх - положительно вниз
        //третья координата это Z отрицательно вниз - положительно вверх
        private void comboBoxSystems_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var comboBox = (ComboBox)sender;
            ObjectPoints = new List<ObjectSystem>();
            var listSystem = SystemService.SystemsID[comboBox.SelectedIndex];
            var kef = (double)500 / SystemService.UniverseSystemsData[listSystem].Radius;
            Bitmap Pix = new Bitmap(Map.Width, Map.Height);
            var tmpListCoords = new List<int>();
            using (Graphics gr = Graphics.FromImage(Pix))
            {
                foreach (var baseID in SystemService.UniverseSystemsData[listSystem].Objects.FindAll((baseId) => baseId.BaseID != null).ToArray())
                {
                    int x = (int)Math.Round(kef * baseID.Pos[0], MidpointRounding.AwayFromZero);
                    int y = (int)Math.Round(kef * baseID.Pos[2], MidpointRounding.AwayFromZero);
                    int[] mapPos = new int[3];
                    mapPos[0] = x;
                    mapPos[1] = y;
                    mapPos[2] = baseID.Pos[1];
                    baseID.MapPos = mapPos;
                    ObjectPoints.Add(baseID);
                    DrawService.DrawPoint(x, y, Map.Width, Map.Height, gr);
                }

                //DrawService.DrawPoint(500, 889, Map.Width, Map.Height, gr);

                Map.Image = Pix;
            }

            LogService.LogEvent("");
        }
    }
}
