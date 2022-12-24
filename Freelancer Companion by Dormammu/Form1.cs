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
        private Bitmap ImageMap { get; set; }
        private double KeyResize { get; set; }
        private double KeyOverSize { get; set; }
        private string CurrentSystem { get; set; }

        public FreelancerCompanionDvurechensky()
        {
            InitializeComponent();

            LogService = new LogService(LoggerRichTextBox);//LoggerTextBox
            Map.MouseWheel += Map_MouseWheel;

            InitializeSystems();
        }

        private void Map_MouseWheel(object sender, MouseEventArgs e)
        {
            if(e.Delta > 0)
            {
                Map.Location = new Point(Map.Location.X - 10, Map.Location.Y - 10);
                Map.Width += 10;
                Map.Height += 10;
            }
            else
            {
                Map.Location = new Point(Map.Location.X + 10, Map.Location.Y + 10);
                Map.Width -= 10;
                Map.Height -= 10;
            }
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
        }

        //первая координата это X отрицательно влево - положительно вправо
        //вторая коордианат это Y отрицательно вверх - положительно вниз
        //третья координата это Z отрицательно вниз - положительно вверх
        private void comboBoxSystems_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var comboBox = (ComboBox)sender;
            Map.Location = new Point(0, 0);
            Map.Width = 1000;
            Map.Height = 1000;
            flowLayoutPanelNames.Controls.Clear();
            ObjectPoints = new List<ObjectSystem>();
            CurrentSystem = SystemService.SystemsID[comboBox.SelectedIndex];
            KeyResize = (double)500 / SystemService.UniverseSystemsData[CurrentSystem].Radius;
            ImageMap = new Bitmap(Map.Width, Map.Height);
            Map.Image = ImageMap;
            checkBoxContainers.Checked = false;
            checkBoxBases.Checked = false;

            var listMaxTmp = new List<int>();
            var radius = SystemService.UniverseSystemsData[CurrentSystem].Radius;

            foreach (var bases in SystemService.UniverseSystemsData[CurrentSystem].Objects.FindAll((objectEl) => !objectEl.ID.Contains("Zone_") && !objectEl.ID.ToLower().Contains("_sun")))
            {
                listMaxTmp.Add(Math.Abs(bases.Pos[0]));
                listMaxTmp.Add(Math.Abs(bases.Pos[1]));
                listMaxTmp.Add(Math.Abs(bases.Pos[2]));
            }

            var maxCoordSystem = listMaxTmp.Max();
            if (maxCoordSystem > radius)
            {
                KeyOverSize = Math.Round((double)maxCoordSystem / radius, 5);
                KeyResize /= KeyOverSize;
                KeyResize = Math.Round(KeyResize, 5);
            }

            RepaintAxis();
            LogService.LogEvent($"Open [{comboBox.SelectedIndex + 1}] {comboBox.Text}");
        }

        private void RepaintAxis()
        {
            var gr = Graphics.FromImage(Map.Image);
            ClearMap(gr);
            //отрисовка осей
            int w = Map.ClientSize.Width / 2;
            int h = Map.ClientSize.Height / 2;
            //Смещение начала координат в центр PictureBox
            gr.TranslateTransform(w, h);
            int newSizeW = 0;
            int newSizeH = 0;
            if (KeyOverSize > 0) newSizeW = (int)Math.Round((double)(w / KeyOverSize), MidpointRounding.AwayFromZero);
            else newSizeW = w;
            if (KeyOverSize > 0) newSizeH = (int)Math.Round((double)(double)(h / KeyOverSize), MidpointRounding.AwayFromZero);
            else newSizeH = h;
            //X
            DrawService.DrawXAxis(new Point(-newSizeW, newSizeW), new Point(newSizeW, newSizeW), gr, false);
            DrawService.DrawXAxis(new Point(-newSizeW, 0), new Point(newSizeW, 0), gr, false);
            DrawService.DrawXAxis(new Point(-newSizeW, newSizeW - newSizeW / 2), new Point(newSizeW, newSizeW - newSizeW / 2), gr, false);
            DrawService.DrawXAxis(new Point(-newSizeW, newSizeW - (newSizeW / 2 + newSizeW / 4)), new Point(newSizeW, newSizeW - (newSizeW / 2 + newSizeW / 4)), gr, false);
            DrawService.DrawXAxis(new Point(-newSizeW, newSizeW - newSizeW / 4), new Point(newSizeW, newSizeW - newSizeW / 4), gr, false);
            DrawService.DrawXAxis(new Point(-newSizeW, -newSizeW), new Point(newSizeW, -newSizeW), gr, false);
            DrawService.DrawXAxis(new Point(-newSizeW, -(newSizeW - newSizeW / 2)), new Point(newSizeW, -(newSizeW - newSizeW / 2)), gr, false);
            DrawService.DrawXAxis(new Point(-newSizeW, -(newSizeW - (newSizeW / 2 + newSizeW / 4))), new Point(newSizeW, -(newSizeW - (newSizeW / 2 + newSizeW / 4))), gr, false);
            DrawService.DrawXAxis(new Point(-newSizeW, -(newSizeW - newSizeW / 4)), new Point(newSizeW, -(newSizeW - newSizeW / 4)), gr, false);
            //Y
            DrawService.DrawYAxis(new Point(-newSizeH, newSizeH), new Point(-newSizeH, -newSizeH), gr, false);
            DrawService.DrawYAxis(new Point(0, newSizeH), new Point(0, -newSizeH), gr, false);
            DrawService.DrawYAxis(new Point(newSizeW - newSizeW / 2, newSizeH), new Point(newSizeW - newSizeW / 2, -newSizeH), gr, false);
            DrawService.DrawYAxis(new Point(newSizeW - (newSizeW / 2 + newSizeW / 4), newSizeH), new Point(newSizeW - (newSizeW / 2 + newSizeW / 4), -newSizeH), gr, false);
            DrawService.DrawYAxis(new Point(newSizeW - newSizeW / 4, newSizeH), new Point(newSizeW - newSizeW / 4, -newSizeH), gr, false);
            DrawService.DrawYAxis(new Point(newSizeH, newSizeH), new Point(newSizeH, -newSizeH), gr, false);
            DrawService.DrawYAxis(new Point(-(newSizeW - newSizeW / 2), newSizeH), new Point(-(newSizeW - newSizeW / 2), -newSizeH), gr, false);
            DrawService.DrawYAxis(new Point(-(newSizeW - (newSizeW / 2 + newSizeW / 4)), newSizeH), new Point(-(newSizeW - (newSizeW / 2 + newSizeW / 4)), -newSizeH), gr, false);
            DrawService.DrawYAxis(new Point(-(newSizeW - newSizeW / 4), newSizeH), new Point(-(newSizeW - newSizeW / 4), -newSizeH), gr, false);

            checkBoxAll.Checked = false;
            checkBoxBases.Checked = false;
            checkBoxContainers.Checked = false;
            checkBoxHoll.Checked = false;
        }

        private void ClearMap(Graphics graphics)
        {
            DrawService.DrawPoint(-Map.Width, -Map.Height, Map.Width, Map.Height, graphics, Color.White, Map.Width * 2, Map.Height * 2);
        }

        private void checkBoxBases_CheckedChanged(object sender, EventArgs e)
        {
            var checkBoxBases = (CheckBox)sender;
            using (Graphics gr = Graphics.FromImage(ImageMap))
            {
                var counter = 0;
                foreach (var baseID in SystemService.UniverseSystemsData[CurrentSystem].Objects.FindAll((baseId) => baseId.BaseID != null).ToArray())
                {
                    int x = (int)Math.Round(KeyResize * baseID.Pos[0], MidpointRounding.AwayFromZero);
                    int y = (int)Math.Round(KeyResize * baseID.Pos[2], MidpointRounding.AwayFromZero);
                    int[] mapPos = new int[3];
                    mapPos[0] = x;
                    mapPos[1] = y;
                    mapPos[2] = baseID.Pos[1];
                    baseID.MapPos = mapPos;
                    if (!ObjectPoints.Contains(baseID))
                        ObjectPoints.Add(baseID);

                    //рисую или стираю точки на карте
                    if (checkBoxBases.Checked == true)
                    {
                        //Формирую вывод UI
                        var button = new Button();
                        button.Width = 231;
                        button.Height = 30;
                        var tooltip = new ToolTip();
                        tooltip.SetToolTip(button, $"Z: [{baseID.Pos[1]}] X: [{baseID.Pos[0]}] Y: [{baseID.Pos[2]}]\n" + ((baseID.Archetype != null) ? baseID.Archetype : string.Empty));
                        button.MouseEnter += Base_MouseEnter;
                        button.MouseLeave += Base_MouseLeave;
                        button.Name = baseID.BaseID;
                        var nameTmp = baseID.BaseID.ToLower();
                        button.Text = (!string.IsNullOrEmpty(SystemService.UniverseBasesData[nameTmp].Name)) ? "[" + counter + "]" + SystemService.UniverseBasesData[nameTmp].Name : "[" + counter + "]" + baseID.ID;
                        flowLayoutPanelNames.Controls.Add(button);
                    }
                    else flowLayoutPanelNames.Controls.Clear();

                    //рисую или стираю точки на карте
                    if (checkBoxBases.Checked == true) DrawService.DrawPoint(x, y, Map.Width, Map.Height, gr, Color.Black, 10, 10);
                    else RepaintAxis();
                }
                Map.Image = ImageMap;
            }
        }

        private void checkBoxContainers_CheckedChanged(object sender, EventArgs e)
        {
            var checkBoxContainers = (CheckBox)sender;

            using (Graphics gr = Graphics.FromImage(ImageMap))
            {
                var counter = 0;
                foreach (var objectElement in SystemService.UniverseSystemsData[CurrentSystem].Objects)
                {
                    var id = objectElement.ID.ToLower();
                    bool ok = false;
                    if (id.Contains("suprise"))
                    {
                        ok = true;
                    }
                    else
                    {
                        if (objectElement.Archetype != null && objectElement.Archetype.Contains("suprise"))
                        {
                            ok = true;
                        }
                        else
                        {
                            if (objectElement.Loadout != null && SystemService.SupriseID.Contains(objectElement.Loadout)) ok = true;
                            else ok = false;
                        }
                    }

                    if (!ok) continue;

                    int x = (int)Math.Round(KeyResize * objectElement.Pos[0], MidpointRounding.AwayFromZero);
                    int y = (int)Math.Round(KeyResize * objectElement.Pos[2], MidpointRounding.AwayFromZero);
                    int[] mapPos = new int[3];
                    mapPos[0] = x;
                    mapPos[1] = y;
                    mapPos[2] = objectElement.Pos[1];
                    objectElement.MapPos = mapPos;
                    if (!ObjectPoints.Contains(objectElement))
                        ObjectPoints.Add(objectElement);

                    //рисую или стираю точки на карте
                    if (checkBoxContainers.Checked == true)
                    {
                        counter++;
                        //Формирую вывод UI
                        var button = new Button();
                        button.Width = 231;
                        button.Height = 30;
                        var tooltip = new ToolTip();
                        tooltip.SetToolTip(button, $"Z: [{objectElement.Pos[1]}] X: [{objectElement.Pos[0]}] Y: [{objectElement.Pos[2]}]\n" + ((objectElement.Archetype != null) ? objectElement.Archetype : string.Empty));
                        button.MouseEnter += Container_MouseEnter; ;
                        button.MouseLeave += Container_MouseLeave; ;
                        button.Name = objectElement.ID;
                        var nameTmp = objectElement.ID.ToLower();
                        button.Text = "[" + counter + "]" + objectElement.ID;
                        flowLayoutPanelNames.Controls.Add(button);
                    }
                    else flowLayoutPanelNames.Controls.Clear();

                    //рисую или стираю точки на карте
                    if (checkBoxContainers.Checked == true) DrawService.DrawPoint(x, y, Map.Width, Map.Height, gr, Color.Red);
                    else RepaintAxis();
                }
                Map.Image = ImageMap;
            }
        }

        private void checkBoxHoll_CheckedChanged(object sender, EventArgs e)
        {
            var checkBoxHoll = (CheckBox)sender;

            using (Graphics gr = Graphics.FromImage(ImageMap))
            {
                var counter = 0;
                foreach (var objectElement in SystemService.UniverseSystemsData[CurrentSystem].Objects.FindAll((objectEl) => (objectEl.ID.Contains("_hole") || objectEl.ID.Contains(CurrentSystem.ToUpper()+"_to")) && !objectEl.ID.Contains("Zone_")))
                {
                    int x = (int)Math.Round(KeyResize * objectElement.Pos[0], MidpointRounding.AwayFromZero);
                    int y = (int)Math.Round(KeyResize * objectElement.Pos[2], MidpointRounding.AwayFromZero);
                    int[] mapPos = new int[3];
                    mapPos[0] = x;
                    mapPos[1] = y;
                    mapPos[2] = objectElement.Pos[1];
                    objectElement.MapPos = mapPos;
                    if (!ObjectPoints.Contains(objectElement))
                        ObjectPoints.Add(objectElement);

                    //рисую или стираю точки на карте
                    if (checkBoxHoll.Checked == true)
                    {
                        counter++;
                        //Формирую вывод UI
                        var button = new Button();
                        button.Width = 231;
                        button.Height = 30;
                        var tooltip = new ToolTip();
                        tooltip.SetToolTip(button, $"Z: [{objectElement.Pos[1]}] X: [{objectElement.Pos[0]}] Y: [{objectElement.Pos[2]}]\n" + ((objectElement.Archetype != null) ? objectElement.Archetype : string.Empty));
                        button.MouseEnter += All_MouseEnter;
                        button.MouseLeave += All_MouseLeave;
                        button.Name = objectElement.ID;
                        var nameTmp = objectElement.ID.ToLower();
                        button.Text = "[" + counter + "]" + (!string.IsNullOrEmpty(objectElement.Goto) ? objectElement.Goto : objectElement.ID);
                        flowLayoutPanelNames.Controls.Add(button);
                    }
                    else flowLayoutPanelNames.Controls.Clear();

                    //рисую или стираю точки на карте
                    if (checkBoxHoll.Checked == true) DrawService.DrawPoint(x, y, Map.Width, Map.Height, gr, Color.Brown, 12, 12);
                    else RepaintAxis();
                }
                Map.Image = ImageMap;
            }
        }

        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            var checkBoxAll = (CheckBox)sender;
            using (Graphics gr = Graphics.FromImage(ImageMap))
            {
                var counter = 0;
                foreach (var objectEl in SystemService.UniverseSystemsData[CurrentSystem].Objects)
                {
                    int x = (int)Math.Round(KeyResize * objectEl.Pos[0], MidpointRounding.AwayFromZero);
                    int y = (int)Math.Round(KeyResize * objectEl.Pos[2], MidpointRounding.AwayFromZero);
                    int[] mapPos = new int[3];
                    mapPos[0] = x;
                    mapPos[1] = y;
                    mapPos[2] = objectEl.Pos[1];
                    objectEl.MapPos = mapPos;
                    if (!ObjectPoints.Contains(objectEl))
                        ObjectPoints.Add(objectEl);

                    //рисую или стираю точки на карте
                    if (checkBoxAll.Checked == true)
                    {
                        //Формирую вывод UI
                        var button = new Button();
                        button.Width = 231;
                        button.Height = 30;
                        var tooltip = new ToolTip();
                        tooltip.SetToolTip(button, $"Z: [{objectEl.Pos[1]}] X: [{objectEl.Pos[0]}] Y: [{objectEl.Pos[2]}]\n" + ((objectEl.Archetype != null) ? objectEl.Archetype : string.Empty));
                        button.MouseEnter += All_MouseEnter;
                        button.MouseLeave += All_MouseLeave;
                        button.Name = objectEl.ID;
                        var nameTmp = objectEl.ID.ToLower();
                        button.Text = "[" + counter + "]" + objectEl.ID;
                        flowLayoutPanelNames.Controls.Add(button);
                    }
                    else flowLayoutPanelNames.Controls.Clear();

                    //рисую или стираю точки на карте
                    if (checkBoxAll.Checked == true) DrawService.DrawPoint(x, y, Map.Width, Map.Height, gr, Color.Blue);
                    else RepaintAxis();
                }
                Map.Image = ImageMap;
            }
        }


        private void comboBoxSystems_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                int index = comboBoxSystems.FindStringExact(comboBoxSystems.Text);
                comboBoxSystems.SelectedIndex = index;
            }
        }

        private void Base_MouseLeave(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var obj = ObjectPoints.Find((baseEl) => baseEl.BaseID == button.Name);
            var name = (string.IsNullOrEmpty(obj.NameBase)) ? button.Name : obj.NameBase;
            //сделать точку обычной
            using (Graphics gr = Graphics.FromImage(ImageMap))
            {
                DrawService.DrawText(new Point(obj.MapPos[0] + 15, obj.MapPos[1] + 15), Map.Width, Map.Height, name, gr, Brushes.White, 15);
                DrawService.DrawPoint(obj.MapPos[0], obj.MapPos[1], Map.Width, Map.Height, gr, Color.White, 15, 15);
                DrawService.DrawPoint(obj.MapPos[0], obj.MapPos[1], Map.Width, Map.Height, gr, Color.Red);
                Map.Image = ImageMap;
            }
        }

        private void Base_MouseEnter(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var obj = ObjectPoints.Find((baseEl) => baseEl.BaseID == button.Name);
            var name = (string.IsNullOrEmpty(obj.NameBase)) ? button.Name : obj.NameBase;
            //сделать точку крупнее
            using (Graphics gr = Graphics.FromImage(ImageMap))
            {
                DrawService.DrawPoint(obj.MapPos[0], obj.MapPos[1], Map.Width, Map.Height, gr, Color.Red, 15, 15);
                DrawService.DrawText(new Point(obj.MapPos[0] + 15, obj.MapPos[1] + 15), Map.Width, Map.Height, name, gr, Brushes.Black, 15);
                Map.Image = ImageMap;
            }
        }

        private void Container_MouseLeave(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var obj = ObjectPoints.Find((baseEl) => baseEl.ID == button.Name);
            var name = button.Name;
            //сделать точку обычной
            using (Graphics gr = Graphics.FromImage(ImageMap))
            {
                DrawService.DrawText(new Point(obj.MapPos[0] + 15, obj.MapPos[1] + 15), Map.Width, Map.Height, name, gr, Brushes.White, 15);
                DrawService.DrawPoint(obj.MapPos[0], obj.MapPos[1], Map.Width, Map.Height, gr, Color.White, 15, 15);
                DrawService.DrawPoint(obj.MapPos[0], obj.MapPos[1], Map.Width, Map.Height, gr, Color.LightGreen);
                Map.Image = ImageMap;
            }
        }

        private void Container_MouseEnter(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var obj = ObjectPoints.Find((objectEl) => objectEl.ID == button.Name);
            var name = button.Name;
            //сделать точку крупнее
            using (Graphics gr = Graphics.FromImage(ImageMap))
            {
                DrawService.DrawPoint(obj.MapPos[0], obj.MapPos[1], Map.Width, Map.Height, gr, Color.LightGreen, 15, 15);
                DrawService.DrawText(new Point(obj.MapPos[0] + 15, obj.MapPos[1] + 15), Map.Width, Map.Height, name, gr, Brushes.LightGreen, 15);
                Map.Image = ImageMap;
            }
        }

        private void All_MouseLeave(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var obj = ObjectPoints.Find((baseEl) => baseEl.ID == button.Name);
            var name = button.Name;
            //сделать точку обычной
            using (Graphics gr = Graphics.FromImage(ImageMap))
            {
                DrawService.DrawText(new Point(obj.MapPos[0] + 15, obj.MapPos[1] + 15), Map.Width, Map.Height, name, gr, Brushes.White, 15);
                DrawService.DrawPoint(obj.MapPos[0], obj.MapPos[1], Map.Width, Map.Height, gr, Color.White, 15, 15);
                DrawService.DrawPoint(obj.MapPos[0], obj.MapPos[1], Map.Width, Map.Height, gr, Color.LightGreen);
                Map.Image = ImageMap;
            }
        }

        private void All_MouseEnter(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var obj = ObjectPoints.Find((objectEl) => objectEl.ID == button.Name);
            var name = button.Name;
            //сделать точку крупнее
            using (Graphics gr = Graphics.FromImage(ImageMap))
            {
                DrawService.DrawPoint(obj.MapPos[0], obj.MapPos[1], Map.Width, Map.Height, gr, Color.LightGreen, 15, 15);
                DrawService.DrawText(new Point(obj.MapPos[0] + 15, obj.MapPos[1] + 15), Map.Width, Map.Height, name, gr, Brushes.LightGreen, 15);
                Map.Image = ImageMap;
            }
        }
    }
}
