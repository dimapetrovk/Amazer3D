using System;

namespace Levels
{
    [Serializable]
    public class Level
    {
        public bool locked;
        public int score;
        public int width;
        public int height;
        public char[,] maze;
        public float time;

        public float bestTime;

        public float timeConst;
        public int[] portal;
        public float[] character;

        public Level() {}

        public Level(int width, int height, int timeConst){
            this.width = width;
            this.height = height;
            this.timeConst = timeConst;
        }

        public void Clear(){
            maze = null;
            portal = null;
            character = null;
            time = 0;
        }
    }
}