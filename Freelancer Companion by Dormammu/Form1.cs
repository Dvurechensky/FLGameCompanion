using Freelancer_Companion_by_Dormammu.Services;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
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

            LogService = new LogService(LoggerRichTextBox);//LoggerTextBox

            InitializeSystems();
        }

        /// <summary>
        /// Загрузка данных систем
        /// </summary>
        private void InitializeSystems()
        {
            SystemService = new SystemService();
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
            var drawService = new DrawService(5, 3);
            drawService.DrawXAxis(new Point(-w, 0), new Point(w, 0), e.Graphics);
            drawService.DrawYAxis(new Point(0, h), new Point(0, -h), e.Graphics);

            //Центр координат
            e.Graphics.FillEllipse(Brushes.Red, -2, -2, 4, 4);
        }

        private void comboBoxSystems_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var listSystem = SystemService.SystemsID[comboBox.SelectedIndex];
            LogService.LogEvent("[" + listSystem + "] " + SystemService.UniverseSystemsData[listSystem].Name);
        }
    }
}
