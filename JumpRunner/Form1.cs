using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JumpRunner
{
    public partial class Form1 : Form
    {
        //VARIAVEIS GLOBAIS
        
        int gravity;
        int gravityValue = 8;
        int obstacleSpeed = 10;
        int score = 0;
        int highScore = 0;
        bool gameOver = false;
        Random random = new Random();

        
        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            // Update das labels
            lblscore.Text = "Score: " + score;
            lblhighscore.Text = "Record: " + highScore;

            // Quando o jogo comeca aplicar a gravidade ao player
            player.Top += gravity;


            // If statment para quando o player chegar ao chao mudar a imagem para a normal
            if(player.Top > 352)
            {
                gravity = 0;
                player.Top = 352;
                player.Image = Properties.Resources.normalfixed;
            }

            // If statment para quando o player chegar ao teto mudar a imagem para a invertida
            else if (player.Top < 44)
            {
                gravity = 0;
                player.Top = 44;
                player.Image = Properties.Resources.reversedfixed;
            }

            foreach (Control x in this.Controls)
            {
                // para cada obstaculo no form
                if(x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= obstacleSpeed;

                    // place de obstaculos na direita do ecra fora de visao
                    if(x.Left < -100)
                    {
                        // remover os obstaculos da esquerda do ecra e dar um ponto ao player 
                        x.Left = random.Next(1200, 3000);
                        score += 1;
                    }
                    // se os bounds do player tocarem nas bounds do obstaculos
                    if (x.Bounds.IntersectsWith(player.Bounds))
                    {
                        // Parar o gameTimer
                        gameTimer.Stop();

                        // mudar a label do score para mostrar que o jogo acabou e como recomecar
                        lblscore.Text += "   Game Over! ENTER para recomecar.";

                        // Acabar o jogo com a variavel "gameOver"
                        gameOver = true;

                        // Mudar o highscore
                        if (score > highScore)
                        {
                            highScore = score;
                        }
                    }
                }
            }

            
            // mudar a dificuldade quando o score e maior ou igual a 5
            if(score >= 5)
            {
                obstacleSpeed = 18;
                gravityValue = 10;
            }

            // mudar a dificuldade quando o score e maior ou igual a 15
            if (score >= 15)
            {
                obstacleSpeed = 20;
                gravityValue = 12;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            // Binds para interagir com o jogo (Space = saltar)
            if(e.KeyCode == Keys.Space)
            {
                if(player.Top == 352)
                {
                    player.Top -= 10;
                    gravity = -gravityValue;
                }
                else if(player.Top == 44)
                {
                    player.Top += 10;
                    gravity = gravityValue;
                }
            }

            // Binds para interagir com o jogo (Enter = Restart game)
            if (e.KeyCode == Keys.Enter && gameOver == true)
            {
                RestartGame();
            }
        }

        private void RestartGame()
        {
            lblscore.Parent = pictureBox1;
            lblhighscore.Parent = pictureBox2;
            lblhighscore.Top = 10;
            player.Location = new Point(155, 352);
            player.Image = Properties.Resources.normalfixed;
            score = 0;
            gravityValue = 8;
            gravity = gravityValue;
            obstacleSpeed = 10;

            foreach(Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left = random.Next(1200, 3000);
                }
            }

            gameTimer.Start();
        }

        private void player_Click(object sender, EventArgs e)
        {

        }
    }
}
