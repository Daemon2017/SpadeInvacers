using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Resources;



namespace Daemon002
{
    public partial class Form1 : Form
    {
        public PictureBox[] EnemyVolnorezPB;
        public PictureBox[] EnemyGromolomPB;

        public PictureBox[] PBullet;
        public PictureBox[] GAIBullet;
        public PictureBox[] VAIBullet;
        
        public Form1()
        {
            InitializeComponent();
        }

        bool left, right, up, down, fire;
        int bulnum = 0, hitpoints = 100, lifes = 3, kills = 0;
        int p, g, v;

        int rCoordVolnorezX;
        int rCoordVolnorezY;
        int CoordVolnorezX;
        int CoordGromolomX;
        int CoordVolnorezY;
        int CoordGromolomY;
        int VNumber;
        int GNumber;

        //Генерация изображений 2-х видов противников по 3 штуки каждого типа.
        public void Form1_Load(object sender, EventArgs e)
        {
            Random StartRnd = new Random();

            EnemyVolnorezPB = new PictureBox[3];

            for (VNumber = 0; VNumber < 3; VNumber++)
            {
                rCoordVolnorezX = StartRnd.Next(0, 875);
                rCoordVolnorezY = StartRnd.Next(0, 151);

                EnemyVolnorezPB[VNumber] = new PictureBox();
                EnemyVolnorezPB[VNumber].Location = new Point(rCoordVolnorezX, rCoordVolnorezY);
                EnemyVolnorezPB[VNumber].Name = "EnemyVolnorez" + VNumber.ToString();
                EnemyVolnorezPB[VNumber].Size = new System.Drawing.Size(66, 65);
                EnemyVolnorezPB[VNumber].BackColor = Color.Transparent;
                EnemyVolnorezPB[VNumber].TabIndex = VNumber;
                EnemyVolnorezPB[VNumber].Visible = true;
                EnemyVolnorezPB[VNumber].Image = Daemon002.Properties.Resources.volnorez;

                this.Controls.Add(EnemyVolnorezPB[VNumber]);
            }

            EnemyGromolomPB = new PictureBox[3];

            for (GNumber = 0; GNumber < 3; GNumber++)
            {
                int rCoordGromolomX = StartRnd.Next(0, 875);
                int rCoordGromolomY = StartRnd.Next(0, 151);

                EnemyGromolomPB[GNumber] = new PictureBox();
                EnemyGromolomPB[GNumber].Location = new Point(rCoordGromolomX, rCoordGromolomY);
                EnemyGromolomPB[GNumber].Name = "EnemyGromolom" + GNumber.ToString();
                EnemyGromolomPB[GNumber].Size = new System.Drawing.Size(66, 65);
                EnemyGromolomPB[GNumber].BackColor = Color.Transparent;
                EnemyGromolomPB[GNumber].TabIndex = GNumber;
                EnemyGromolomPB[GNumber].Visible = true;
                EnemyGromolomPB[GNumber].Image = Daemon002.Properties.Resources.gromolom;

                this.Controls.Add(EnemyGromolomPB[GNumber]);
            }

        }

        //Принятие команд направления движения.
        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 'W':
                    {
                        up = true;
                        button1_Click(sender, e);
                        Player.Image = Daemon002.Properties.Resources.rock_t;
                        break;
                    }
                case 'A':
                    {
                        left = true;
                        button2_Click(sender, e);
                        Player.Image = Daemon002.Properties.Resources.rock_l;
                        break;
                    }
                case 'D':
                    {
                        right = true;
                        button3_Click(sender, e);
                        Player.Image = Daemon002.Properties.Resources.rock_r;
                        break;
                    }
                case 'S':
                    {
                        down = true;
                        button4_Click(sender, e);
                        Player.Image = Daemon002.Properties.Resources.rock_b;
                        break;
                    }
                case 32:
                    {
                        fire = true;
                        timer2.Start();
                        button5_Click(sender, e);
                        break;
                    }
            }
        }

        //Движение вперёд
        public void button1_Click(object sender, EventArgs e)
        {
            if (Player.Top > 0)
            {
                Player.Top = Player.Top - 20;
                label1.Text = Player.Top.ToString();
            }

            else
            {
                Player.Top = Player.Top > 0 ? Player.Top = this.ClientSize.Height - Player.Height : 0;
            }
        }

        //Движение влево
        public void button2_Click(object sender, EventArgs e)
        {
            if (Player.Left > 0)
            {
                Player.Left = Player.Left - 20;
                label2.Text = ((Player.Left) + 33).ToString();
            }

            else
            {
                Player.Left = Player.Left > 0 ? Player.Left = this.ClientSize.Width - Player.Width : 0;
            }
        }

        //Движение вправо
        public void button3_Click(object sender, EventArgs e)
        {
            if (Player.Left < this.ClientSize.Width - Player.Width)
            {
                Player.Left = Player.Left + 20;
                label2.Text = ((Player.Left) + 33).ToString();
            }

            else
            {
                Player.Left = Player.Left > 0 ? Player.Left = this.ClientSize.Width - Player.Width : 0;
            }
        }

        //Движение назад
        public void button4_Click(object sender, EventArgs e)
        {
            if (Player.Top < this.ClientSize.Height - Player.Height)
            {
                Player.Top = Player.Top + 20;
                label1.Text = Player.Top.ToString();
            }

            else
            {
                Player.Top = Player.Top > 0 ? Player.Top = this.ClientSize.Height - Player.Height : 0;
                hitpoints = hitpoints - 2;
                label12.Text = hitpoints.ToString();

                if (hitpoints < 1)
                {
                    lifes = lifes - 1;
                    hitpoints = 100;
                }

                label13.Text = lifes.ToString();

                if (lifes <= 0)
                {
                    timer1.Stop();
                    timer2.Stop();
                    timer3.Stop();
                    timer4.Stop();
                    timer5.Stop();
                    timer6.Stop();
                    timer7.Stop();
                    timer8.Stop();
                    timer9.Stop();
                    timer10.Stop();
                    timer11.Stop();
                    timer12.Stop();
                    timer13.Stop();
                    timer14.Stop();
                    MessageBox.Show("Конец игры", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    hitpoints = 0;
                }
            }
        }

        public void button5_Click(object sender, EventArgs e)
        {

        }

        //Генерация пуль 
        public void timer2_Tick(object sender, EventArgs e)
        {
            if (fire == true)
            {
                PictureBox PBullet = new PictureBox();
                PBullet.Name = string.Format("Pbullet");
                PBullet.BackColor = Color.Transparent;
                PBullet.Image = Daemon002.Properties.Resources.bullet;
                PBullet.Location = new Point(Player.Left + 23, Player.Top - 10);
                PBullet.SizeMode = PictureBoxSizeMode.StretchImage;
                PBullet.ClientSize = new Size(20, 20);
                PBullet.Image = Daemon002.Properties.Resources.bullet;

                this.Controls.Add(PBullet);

                timer1.Start();
                bulnum = bulnum + 1;
                label7.Text = bulnum.ToString();
            }
            
            if ((fire == true) && (right == true))
            {
                PictureBox PBullet = new PictureBox();
                PBullet.Name = string.Format("Pbullet");
                PBullet.BackColor = Color.Transparent;
                PBullet.Image = Daemon002.Properties.Resources.bullet;
                PBullet.Location = new Point(Player.Left + 23, Player.Top - 10);
                PBullet.SizeMode = PictureBoxSizeMode.StretchImage;
                PBullet.ClientSize = new Size(20, 20);
                PBullet.Image = Daemon002.Properties.Resources.bullet;

                this.Controls.Add(PBullet);

                timer1.Start();
                bulnum = bulnum + 1;
                label7.Text = bulnum.ToString();

                button3_Click(sender, e);
                Player.Image = Daemon002.Properties.Resources.rock_r;
            }

            if ((fire == true) && (left == true))
            {
                PictureBox PBullet = new PictureBox();
                PBullet.Name = string.Format("Pbullet");
                PBullet.BackColor = Color.Transparent;
                PBullet.Location = new Point(Player.Left + 23, Player.Top - 10);
                PBullet.SizeMode = PictureBoxSizeMode.StretchImage;
                PBullet.ClientSize = new Size(20, 20);
                PBullet.Image = Daemon002.Properties.Resources.bullet;

                this.Controls.Add(PBullet);

                timer1.Start();
                bulnum = bulnum + 1;
                label7.Text = bulnum.ToString();

                button2_Click(sender, e);
                Player.Image = Daemon002.Properties.Resources.rock_l;
            }
        }

        //Полёт пуль
        public void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Control Pbullet in Controls)
                if (Pbullet.Name == "Pbullet")
                {
                    Point p = Pbullet.Location;
                    p.Y -= 20;
                    Pbullet.Location = p;
                }
        }

        //Отключает стрельбу и движение после стрельбы в движении
        public void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 'W':
                    {
                        up = false;
                        Player.Image = Daemon002.Properties.Resources.rock;
                        break;
                    }
                case 'A':
                    {
                        left = false;
                        Player.Image = Daemon002.Properties.Resources.rock;
                        break;
                    }
                case 'D':
                    {
                        right = false;
                        Player.Image = Daemon002.Properties.Resources.rock;
                        break;
                    }
                case 'S':
                    {
                        down = false;
                        Player.Image = Daemon002.Properties.Resources.rock;
                        break;
                    }
                case 32:
                    {
                        fire = false;
                        timer2.Stop();
                        break;
                    }
            }
        }

        public enemy[] mas = null;

        public class enemy
        {
            protected int x;
            protected int y;

            public enemy(Random r)
            {
                x = r.Next(0, 874);
                y = r.Next(0, 100);
            }
        }

        //Тактические данные противника типа Волнорез - HP, сила атаки
        public class EnemyVolnorez : enemy
        {
            public EnemyVolnorez(Random r) : base(r) { }

        }

        //Тактические данные противника типа Громолом - HP, сила атаки
        public class enemyThunder : enemy
        {
            public enemyThunder(Random r) : base(r) { }

        }

        //Генерация новых противников по мере уничтожения игроком старых
        public void timer3_Tick(object sender, EventArgs e)
        {
            mas = new enemy[3];
            Random Randvalue = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < 3; i++)
            {
                switch (Randvalue.Next(0, 1))
                {
                    case 0: mas[i] = new EnemyVolnorez(Randvalue);
                        break;
                    case 1: mas[i] = new enemyThunder(Randvalue);
                        break;
                }
            }
        }

        //Горизонтальное перемещение Волнорезов по таймеру
        public void timer4_Tick(object sender, EventArgs e)
        {
            for (VNumber = 0; VNumber < 3; VNumber++)
            {
                if (this.Controls.Contains(EnemyVolnorezPB[VNumber]))
                {
                    if (EnemyVolnorezPB[VNumber].Location.X < 0)
                    {
                        EnemyVolnorezPB[VNumber].Location = new Point(EnemyVolnorezPB[VNumber].Location.X + 60, EnemyVolnorezPB[VNumber].Location.Y);
                    }

                    else
                    {
                        Random VolnorezMoveRndX = new Random();
                        CoordVolnorezX = VolnorezMoveRndX.Next(-20, 20);
                        EnemyVolnorezPB[VNumber].Location = new Point(EnemyVolnorezPB[VNumber].Location.X + CoordVolnorezX, EnemyVolnorezPB[VNumber].Location.Y);
                    }

                    if (EnemyVolnorezPB[VNumber].Location.X > 874)
                    {
                        EnemyVolnorezPB[VNumber].Location = new Point(EnemyVolnorezPB[VNumber].Location.X - 60, EnemyVolnorezPB[VNumber].Location.Y);
                    }

                    else
                    {
                        Random VolnorezMoveRndX = new Random();
                        CoordVolnorezX = VolnorezMoveRndX.Next(-20, 20);
                        EnemyVolnorezPB[VNumber].Location = new Point(EnemyVolnorezPB[VNumber].Location.X + CoordVolnorezX, EnemyVolnorezPB[VNumber].Location.Y);
                    }
                }
            }

        }

        //Горизонтальное перемещение Громоломов по таймеру
        public void timer13_Tick(object sender, EventArgs e)
        {
            for (GNumber = 0; GNumber < 3; GNumber++)
            {
                if (this.Controls.Contains(EnemyGromolomPB[GNumber]))
                {
                    if (EnemyGromolomPB[GNumber].Location.X < 0)
                    {
                        EnemyGromolomPB[GNumber].Location = new Point(EnemyGromolomPB[GNumber].Location.X + 60, EnemyGromolomPB[GNumber].Location.Y);
                    }

                    else
                    {
                        Random GromolomMoveRndX = new Random();
                        CoordGromolomX = GromolomMoveRndX.Next(-40, 40);
                        EnemyGromolomPB[GNumber].Location = new Point(EnemyGromolomPB[GNumber].Location.X + CoordGromolomX, EnemyGromolomPB[GNumber].Location.Y);
                    }

                    if (EnemyGromolomPB[GNumber].Location.X > 874)
                    {
                        EnemyGromolomPB[GNumber].Location = new Point(EnemyGromolomPB[GNumber].Location.X - 60, EnemyGromolomPB[GNumber].Location.Y);
                    }

                    else
                    {
                        Random GromolomMoveRndX = new Random();
                        CoordGromolomX = GromolomMoveRndX.Next(-40, 40);
                        EnemyGromolomPB[GNumber].Location = new Point(EnemyGromolomPB[GNumber].Location.X + CoordGromolomX, EnemyGromolomPB[GNumber].Location.Y);
                    }
                }
            }
        }

        //Движение Волнорезов вниз, к Игроку
        public void timer5_Tick(object sender, EventArgs e)
        {
            for (VNumber = 0; VNumber < 3; VNumber++)
            {
                if (this.Controls.Contains(EnemyVolnorezPB[VNumber]))
                {
                    Random VolnorezMoveRndY = new Random();
                    CoordVolnorezY = VolnorezMoveRndY.Next(0, 10);

                    EnemyVolnorezPB[VNumber].Location = new Point(EnemyVolnorezPB[VNumber].Location.X, EnemyVolnorezPB[VNumber].Location.Y + CoordVolnorezY);
                }
            }
        }

        //Движение Громоломов вниз, к Игроку
        public void timer14_Tick(object sender, EventArgs e)
        {
            for (GNumber = 0; GNumber < 3; GNumber++)
            {
                if (this.Controls.Contains(EnemyGromolomPB[GNumber]))
                {
                    Random GromolomMoveRndY = new Random();
                    CoordGromolomY = GromolomMoveRndY.Next(0, 15);

                    EnemyGromolomPB[GNumber].Location = new Point(EnemyGromolomPB[GNumber].Location.X, EnemyGromolomPB[GNumber].Location.Y + CoordGromolomY);
                }
            }
        }

        //Стрельба противника типа Волнорез
        public void timer6_Tick(object sender, EventArgs e)
        {
            for (VNumber = 0; VNumber < 3; VNumber++)
            {
                if (this.Controls.Contains(EnemyVolnorezPB[VNumber]))
                {
                    PictureBox VAIBullet = new PictureBox();
                    VAIBullet.Name = string.Format("VAIbullet");
                    VAIBullet.BackColor = Color.Transparent;
                    VAIBullet.Image = Daemon002.Properties.Resources.bullet;
                    VAIBullet.Location = new Point(EnemyVolnorezPB[VNumber].Location.X + 23, EnemyVolnorezPB[VNumber].Location.Y + 75);
                    VAIBullet.SizeMode = PictureBoxSizeMode.StretchImage;
                    VAIBullet.ClientSize = new Size(20, 20);
                    VAIBullet.Image = Daemon002.Properties.Resources.bullet;

                    this.Controls.Add(VAIBullet);

                    timer7.Start();
                }
            }
        }

        //Движение снаряда, выпущенного Волнорезом
        public void timer7_Tick(object sender, EventArgs e)
        {
            foreach (Control VAIbullet in Controls)
                if (VAIbullet.Name == "VAIbullet")
                {
                    Point v = VAIbullet.Location;
                    v.Y += 25;
                    VAIbullet.Location = v;
                }
        }

        //Стрельба противника типа Громолом
        public void timer8_Tick(object sender, EventArgs e)
        {
            for (GNumber = 0; GNumber < 3; GNumber++)
            {
                if (this.Controls.Contains(EnemyGromolomPB[GNumber]))
                {
                    PictureBox GAIBullet = new PictureBox();
                    GAIBullet.Name = string.Format("GAIbullet");
                    GAIBullet.BackColor = Color.Transparent;
                    GAIBullet.Location = new Point(EnemyGromolomPB[GNumber].Location.X + 23, EnemyGromolomPB[GNumber].Location.Y + 75);
                    GAIBullet.SizeMode = PictureBoxSizeMode.StretchImage;
                    GAIBullet.ClientSize = new Size(20, 20);
                    GAIBullet.Image = Daemon002.Properties.Resources.bullet;

                    this.Controls.Add(GAIBullet);

                    timer9.Start();
                }
            }
        }

        //Движение снаряда, выпущенного Громоломом
        public void timer9_Tick(object sender, EventArgs e)
        {
            foreach (Control GAIbullet in Controls)
                if (GAIbullet.Name == "GAIbullet")
                {
                    Point g = GAIbullet.Location;
                    g.Y += 30;
                    GAIbullet.Location = g;
                }
        }

        //Фиксация попадания снаряда Громолома в Игрока
        public void timer10_Tick(object sender, EventArgs e)
        {
            foreach (Control GAIbullet in Controls)
                if (GAIbullet.Location.Y > Player.Location.Y && GAIbullet.Location.Y < (Player.Location.Y + 66) && GAIbullet.Location.X > Player.Location.X && GAIbullet.Location.X < (Player.Location.X + 66))
                {

                hitpoints = hitpoints - 5;

                label12.Text = hitpoints.ToString();

                if (hitpoints < 1)
                {
                    lifes = lifes - 1;
                    hitpoints = 100;
                }

                label13.Text = lifes.ToString();

                if (lifes <= 0)
                {
                    timer1.Stop();
                    timer2.Stop();
                    timer3.Stop();
                    timer4.Stop();
                    timer5.Stop();
                    timer6.Stop();
                    timer7.Stop();
                    timer8.Stop();
                    timer9.Stop();
                    timer10.Stop();
                    timer11.Stop();
                    timer12.Stop();
                    timer13.Stop();
                    timer14.Stop();
                    MessageBox.Show("Конец игры", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    hitpoints = 0;
                }
            }
        }
        
        //Фиксация попадания снаряда Волнореза в Игрока
        public void timer11_Tick(object sender, EventArgs e)
        {
            foreach (Control VAIbullet in Controls)
                if (VAIbullet.Location.Y > Player.Location.Y && VAIbullet.Location.Y < (Player.Location.Y + 66) && VAIbullet.Location.X > Player.Location.X && VAIbullet.Location.X < (Player.Location.X + 66))
                {

                    hitpoints = hitpoints - 2;
                    label12.Text = hitpoints.ToString();

                    if (hitpoints < 1)
                    {
                        lifes = lifes - 1;
                        hitpoints = 100;
                    }

                    label13.Text = lifes.ToString();

                    if (lifes <= 0)
                    {
                        timer1.Stop();
                        timer2.Stop();
                        timer3.Stop();
                        timer4.Stop();
                        timer5.Stop();
                        timer6.Stop();
                        timer7.Stop();
                        timer8.Stop();
                        timer9.Stop();
                        timer10.Stop();
                        timer11.Stop();
                        timer12.Stop();
                        timer13.Stop();
                        timer14.Stop();
                        MessageBox.Show("Конец игры", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        hitpoints = 0;
                    }
                }
        }
        
        //Фиксация попадания снаряда Игрока в Противников
        public void timer12_Tick(object sender, EventArgs e)
        {
            foreach (Control Pbullet in Controls)
            {
                if (Pbullet.Name == "Pbullet")
                {
                    for (GNumber = 0; GNumber < 3; GNumber++)
                    {
                        if ((Pbullet.Location.Y >= (EnemyGromolomPB[GNumber].Location.Y - 23)) && (Pbullet.Location.Y <= (EnemyGromolomPB[GNumber].Location.Y + 66)) && (Pbullet.Location.X >= EnemyGromolomPB[GNumber].Location.X) && (Pbullet.Location.X <= (EnemyGromolomPB[GNumber].Location.X + 66)))
                        {
                            this.Controls.Remove(EnemyGromolomPB[GNumber]);
                            EnemyGromolomPB[GNumber].Location = new Point(0, 0);
                            this.Controls.Remove(Pbullet);
                            kills = kills + 1;
                            label14.Text = kills.ToString();

                            if (kills >= 6)
                            {
                                MessageBox.Show("Вы победили!", "You win!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                        }
                    }

                    for (VNumber = 0; VNumber < 3; VNumber++)
                    {
                        if ((Pbullet.Location.Y >= (EnemyVolnorezPB[VNumber].Location.Y - 23)) && (Pbullet.Location.Y <= (EnemyVolnorezPB[VNumber].Location.Y + 66)) && (Pbullet.Location.X >= EnemyVolnorezPB[VNumber].Location.X) && (Pbullet.Location.X <= (EnemyVolnorezPB[VNumber].Location.X + 66)))
                        {
                            this.Controls.Remove(EnemyVolnorezPB[VNumber]);
                            EnemyVolnorezPB[VNumber].Location = new Point(0, 0);
                            this.Controls.Remove(Pbullet);
                            kills = kills + 1;
                            label14.Text = kills.ToString();

                            if (kills >= 6)
                            {
                                MessageBox.Show("Вы победили!", "You win!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                        }
                    }
                }
            }
        }
   
    }
}
