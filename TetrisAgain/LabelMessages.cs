using System.Windows.Forms;

namespace Tetris
{
    internal class LabelMessages
    {
        private Label scoreLabel;
        private Label linesLabel;
        private Label speedLabel;
        private Label levelLabel;

        public LabelMessages(Label scoreLabel, Label linesLabel, Label speedLabel, Label levelLabel)
        {
            this.scoreLabel = scoreLabel;
            this.linesLabel = linesLabel;
            this.speedLabel = speedLabel;
            this.levelLabel = levelLabel;
        }

        // Метод для оновлення кількості ліній
        public void NumberLines(int lines)
        {
            linesLabel.Text = "Lines: " + lines;
        }

        // Метод для оновлення рахунку
        public void UpdateScore(int score)
        {
            scoreLabel.Text = "Score: " + score;
        }

        // Метод для оновлення швидкості
        public void SpeedLabel(int interval)
        {
            speedLabel.Text = "Speed: " + interval;
        }

        // Метод для оновлення рівня
        public void LevelUp(int level)
        {
            levelLabel.Text = "Level:" + level;
        }
    }
}
