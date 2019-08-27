using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Configuration;
using System.Data.SQLite;


namespace AltayFaz1
{
    public partial class ehopesForm : Form
    {
        Image mainImage;
        List<Image> images = new List<Image>();
        int activeImage = 0;
        bool editmode = false;
        Point mdown;
        Point mup;
        bool rubbersuruyor = false;
        bool rubberbitti = false;
        SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(50, 0, 0, 255));
        Pen editPen = new Pen(Color.Blue, 3);
        Pen savedPen = new Pen(Color.Green, 3);
        Pen transPen = new Pen(Color.Transparent, 3);
        List<StaRect> starects;
        StaRect tempStaRect;
        Rectangle currRect;
        bool currRectSaved = false;


        
        String[] imageNames;
        List<Panel> panels = new List<Panel>();
        Panel currentPanel;
        Point PanelMouseDownLocation;
        Cursor questionMark;
        string dbName;
        bool isAdding = false;
        //SQLiteConnection cnn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
        SQLiteConnection cnn;
        public ehopesForm()
        {


            InitializeComponent();


            // adjust form size to fit screen and hide some portions
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Size = Screen.PrimaryScreen.Bounds.Size;
            this.Location = new Point(0, 0);

            //user Configuration//
            
            String[] args = Environment.GetCommandLineArgs();
            if (args[1] == "1") editmode = true;
            else if(args[1]=="0")
            {
                btSave.Hide();
                btDelete.Hide();
                txtStatement.ReadOnly = true;
            }
            
             //editmode=true;
          
 

            //image configuration
            string path = args[2];
            //string path = "C:\\Users\\yeolm\\Desktop\\images";
            imageConfiguration(path);
            


            //database configuration
            string dbPath = path + Path.DirectorySeparatorChar + dbName;
            cnn = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;");
            cnn.Open();


            

            //statementPanel
            staPanel.Location = new Point((this.Size.Width / 2) - staPanel.Width / 2, (this.Size.Height / 2) - staPanel.Height / 2);
            Bitmap cross = new Bitmap(Properties.Resources.cross,crossBox.Size);
            crossBox.Image = (Image)cross;
            staPanel.Hide();


            // Adding Images to StripMenu
            
            foreach (String name in imageNames)
            {
                ToolStripMenuItem image = new ToolStripMenuItem(name, null, new EventHandler(image_Click));
                image.Name = name;

                
            }

            currentPanel = panels[0];

            //Read database
            initializeStaRects();

            //enable form to get keyboard input
            this.KeyPreview = true;
            this.KeyUp += form1_keyPress;
            crossBox.Click += cross_Click;
            // reduce flicker
            this.DoubleBuffered = true;

        }
        private void initializeStaRects()
        {

            String query = "SELECT* FROM user WHERE pageName=@pageName";

            SQLiteCommand cmd = new SQLiteCommand(query, cnn);
            cmd.Parameters.Add("@pageName", DbType.String);
            cmd.Parameters["@pageName"].Value = imageNames[activeImage];
            SQLiteDataReader reader = cmd.ExecuteReader();
            starects = new List<StaRect>();
            if (!reader.Read())
            {
                return;
            }

            
            do
            {

                String[] points = reader.GetValue(2).ToString().Split(':');

                int x1 = Convert.ToInt32(points[0].Split(',')[0]);
                int y1 = Convert.ToInt32(points[0].Split(',')[1]);
                Point first = new Point(x1, y1);

                int x2 = Convert.ToInt32(points[1].Split(',')[0]);

                int y2 = Convert.ToInt32(points[1].Split(',')[1]);
                Point second = new Point(x2, y2);

                StaRect temp = new StaRect();
                temp.Rectangle = new Rectangle(x1, y1, x2, y2);

                temp.Statement = (String)reader.GetValue(3);
                temp.id = Convert.ToInt32(reader.GetValue(1).ToString());
                starects.Add(temp);

            } while (reader.Read());

        }
        private void imageConfiguration(String path)
        {
            
            DirectoryInfo systemFiles = new DirectoryInfo(path);
            FileInfo[] files = systemFiles.GetFiles();
            imageNames = new string[files.Length - 1];
            int index = 0;
            char sep = Path.DirectorySeparatorChar;
            foreach (FileInfo file in files)
            {
                if (file.Extension == ".db")
                {
                    dbName = file.Name;
                    continue;
                }
                imageNames[index++] = file.Name;
                images.Add(Image.FromFile(path + sep + file.Name));
                Panel tempPanel = new Panel();
                tempPanel.Size = this.Size;

                Image tempImage = Image.FromFile(path + sep+ file.Name);
                tempPanel.BackgroundImage = Image.FromFile(path + sep + file.Name);
               

                panels.Add(tempPanel);
                assignEvents(tempPanel);
                this.Controls.Add(tempPanel);
            }
        }

        private void assignEvents(Panel p)
        {
            p.Paint += currentPanel_Paint;
            p.MouseClick += currentPanel_MouseClick;
            p.MouseMove += currentPanel_MouseMove;
            p.MouseUp += currentPanel_MouseUp;
            p.MouseDown += currentPanel_MouseDown;
            p.MouseHover += currentPanel_MouseHover;


        }
        private void cross_Click(object sender,EventArgs e)
        {
            staPanel.Hide();
        }
        private void form1_keyPress(object sender, KeyEventArgs e)
        {

             if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left)
            {

                int i;
                if (e.KeyCode == Keys.Right) i = 1;

                else i = -1;

                activeImage += i;
                if (activeImage < 0)
                {
                    activeImage = panels.Count - 1;
                }
                else if (activeImage + 1 > panels.Count)
                {
                    activeImage = 0;
                }

                mainImage = images[activeImage];
                

                currentPanel.Hide();
                currentPanel = panels[activeImage];

                currentPanel.Show();
                initializeStaRects();
                txtStatement.Text = "";
                staPanel.Hide();

            }
             else if (e.KeyCode==Keys.Escape)
            {
                staPanel.Hide();
            }


        }


        private void image_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            int i = 0;
            String itemName = item.Name;
            for (; i < imageNames.Length; i++)
            {
                if (itemName.Equals(imageNames[i])) break;
            }

            mainImage = images[i];
            activeImage = i;
            //editmode = false;

            currentPanel.Hide();
            currentPanel = panels[i];

            currentPanel.Show();
            initializeStaRects();
            txtStatement.Text = "";
            staPanel.Hide();

        }


        private void currentPanel_Paint(object sender, PaintEventArgs e)
        {


           
            for (int i = 0; i < starects.Count; i++)
            {
                if (editmode)
                {
                    e.Graphics.DrawRectangle(savedPen, starects[i].Rectangle);
                }
                else
                {
                    e.Graphics.DrawRectangle(transPen, starects[i].Rectangle);
                }

            }

            if (rubberbitti)
            {
                if (currRectSaved == false)
                {
                    currRect = GetRectangle(mdown, mup);
                    e.Graphics.DrawRectangle(editPen, currRect);
                    e.Graphics.FillRectangle(semiTransBrush, currRect);
                    isAdding = true;
                }

            }

        }


        private void currentPanel_MouseClick(object sender, MouseEventArgs e)
        {
            currentPanel.Invalidate();
            
            if (e.Button == MouseButtons.Right && editmode && rubberbitti)
            {
                
                rubberbitti = false;
                staPanel.Hide();
                isAdding = false;


            }
            if (e.Button == MouseButtons.Left && editmode)


            {
                txtStatement.Text= "";
                staPanel.Show();
      
           }


            
        }
        static public Rectangle GetRectangle(Point p1, Point p2)
        {
            return new Rectangle(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y),
                Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
        }


        long i = 0;
        private void currentPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (editmode)
                {
                    currRectSaved = false;
                    currentPanel.Refresh();
                    using (Graphics g = currentPanel.CreateGraphics())
                    {
                        Rectangle rect = GetRectangle(mdown, e.Location);
                        g.DrawRectangle(Pens.Red, rect);

                    }
                    mup = e.Location;

                }

                i++;
            }

            if (!isAdding)
            {
                for (int i = 0; i < starects.Count; i++)
                {
                    if (starects[i].Rectangle.Contains(e.Location))
                    {
                        txtStatement.Text = starects[i].Statement;
                        tempStaRect = starects[i];
                        this.Cursor = questionMark;
                        if (editmode)
                        {
                            staPanel.Location = e.Location;

                        }
                        staPanel.Show();
                        return;

                    }

                }
            }

            //txtStatement.Text = "";
            if (editmode && !rubberbitti) { 

            staPanel.Hide();

            }
            else if (!editmode)
            {
                staPanel.Hide();
            }
            
            tempStaRect = null;
            this.Cursor = Cursors.Default;

        }

        private void currentPanel_MouseHover(object sender, EventArgs e)
        {
            currentPanel.Focus();
        }


        private void currentPanel_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                if (editmode)
                {
                    mdown = e.Location;
                    rubberbitti = false;
                    rubbersuruyor = true;
                }
            }
        }

        private void currentPanel_MouseUp(object sender, MouseEventArgs e)
        {

            if (rubbersuruyor)
            {
                rubbersuruyor = false;
                rubberbitti = true;
            }
        }


        private void btSave_Click(object sender, EventArgs e)
        {
            String query;
            SQLiteCommand cmd;
            


            if (starects.Contains(tempStaRect))
            {

                String pointss = tempStaRect.Rectangle.X.ToString() + "," + tempStaRect.Rectangle.Y.ToString() + ":" + tempStaRect.Rectangle.Width.ToString() + "," + tempStaRect.Rectangle.Height.ToString();
                tempStaRect.Statement = txtStatement.Text;
                query = "UPDATE user SET statement=@statement WHERE pageName=@activeImageName AND points=@point";
                cmd = new SQLiteCommand(query,cnn);
                cmd.Parameters.Add("@activeImageName",DbType.String);
                cmd.Parameters.Add("@point", DbType.String);
                cmd.Parameters["@activeImageName"].Value = imageNames[activeImage];
                cmd.Parameters["@point"].Value= pointss;
                cmd.Parameters.Add("@statement", DbType.AnsiString);
                cmd.Parameters["@statement"].Value=tempStaRect.Statement;
                cmd.ExecuteNonQuery();
                staPanel.Hide();
                return;
            }
            tempStaRect = new StaRect();
            tempStaRect.Rectangle = currRect;
            tempStaRect.Statement = txtStatement.Text;

   

            currRectSaved = false ;
            txtStatement.Text = "";
            staPanel.Hide();

            String points = tempStaRect.Rectangle.X.ToString() + "," + tempStaRect.Rectangle.Y.ToString() + ":" + tempStaRect.Rectangle.Width.ToString() + "," + tempStaRect.Rectangle.Height.ToString();
            //save database
            try
            {
                query = "INSERT INTO user (pageName,points,statement) VALUES (@activeImageName,@point,@statement)";
                cmd = new SQLiteCommand(query, cnn);
                cmd.Parameters.Add("@activeImageName", DbType.String);
                cmd.Parameters["@activeImageName"].Value = imageNames[activeImage];
                cmd.Parameters.Add("@point", DbType.String);

                cmd.Parameters["@point"].Value = points;
                cmd.Parameters.Add("@statement", DbType.AnsiString);
                cmd.Parameters["@statement"].Value = tempStaRect.Statement;
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                return;
            }


            

            query = "SELECT rectId FROM user WHERE pageName=@activeImageName AND points=@point";
            cmd = new SQLiteCommand(query,cnn);
            cmd.Parameters.Add("@activeImageName", DbType.String);
            cmd.Parameters["@activeImageName"].Value = imageNames[activeImage];
            cmd.Parameters.Add("@point", DbType.String);
            cmd.Parameters["@point"].Value = points;
            SQLiteDataReader reader = cmd.ExecuteReader();
            reader.Read();
           
          
            starects.Add(tempStaRect);

            currRectSaved = true;
            rubberbitti = false;
            isAdding = false;
            
            currentPanel.Refresh();


        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cnn.Close();
            this.Close();
        }
        private void staPanel_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PanelMouseDownLocation = e.Location;

            }
        }

        private void staPanel_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = e.X - PanelMouseDownLocation.X + staPanel.Left;
                int y = e.Y - PanelMouseDownLocation.Y + staPanel.Top;
                staPanel.Location = new Point(x, y);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtStatement.Text = "";
            staPanel.Hide();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            if (tempStaRect == null ) return;

            String query = "DELETE  FROM user WHERE pageName=@pageName AND (rectId=@rectId OR points=@point)";
            SQLiteCommand cmd = new SQLiteCommand(query, cnn);
            cmd.Parameters.Add("@pageName", DbType.String);
            cmd.Parameters["@pageName"].Value = imageNames[activeImage];
            cmd.Parameters.Add("@rectId", DbType.Int16);
            cmd.Parameters["@rectId"].Value = tempStaRect.id;
            cmd.Parameters.Add("@point", DbType.String);
            int x = tempStaRect.Rectangle.X + tempStaRect.Rectangle.Width;
            int y = tempStaRect.Rectangle.Y - tempStaRect.Rectangle.Height;
            String points = tempStaRect.Rectangle.X.ToString() + "," + tempStaRect.Rectangle.Y.ToString() + ":" + tempStaRect.Rectangle.Width.ToString() + "," + tempStaRect.Rectangle.Height.ToString();
            cmd.Parameters["@point"].Value = points;
            int nested=cmd.ExecuteNonQuery();
            while (nested > 0)
            {
                starects.Remove(tempStaRect);
                nested--;
            }
            
            txtStatement.Text = "";
            tempStaRect = null;
            staPanel.Hide();
            currentPanel.Refresh();
          
        }

        private void ehopesForm_Load(object sender, EventArgs e)
        {
            Size size = this.Cursor.Size;
            
            Bitmap bmp = new Bitmap(Properties.Resources.questionMark,size);
            questionMark = new Cursor(bmp.GetHicon());
        }
    }
}
