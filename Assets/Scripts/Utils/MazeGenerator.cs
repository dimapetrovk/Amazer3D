namespace Utils
{
	public class MazeGenerator
	{
		char[,] maze;
		int height;
		int width;
		CustomRandom rand;

		public MazeGenerator(int width, int height, int level){
			this.width = width;
			this.height = height;
			rand = new CustomRandom(width, height, level);
		}

		public char[,] Generate(){
			Clear ();
			SetupWalls ();
			return maze;
		}

		void Clear(){
			maze=new char[height + 1, 2 * width + 1];
			for (short i = 0; i <= height; i++) {
				if (i == 0 || i == height) {
					for (short t = 0; t <= 2 * width; t++)
						if(t % 2==0) maze [i, t] = ' ';
						else maze [i, t] = '_';
				} else {
					for (short t = 1; t < 2 * width; t++)
						maze [i, t] = ' ';
				}
				if (i != 0) {
					maze [i, 0] = '|';
					maze [i, 2 * width] = '|';
				}
			}
		}

		void SetupWalls(){
			short[] row = new short[width];
			for (short x = 0; x < width; x++)
				row [x] = x;
			for (short y = 0; y < height - 1; y++) {
				SetId (ref row);
				SetupVerticalWalls (ref row, ref maze, y);
				for (short x = 0; x < width; x++) {
					short startGroup = x;
					bool notPut = false;
					while (x < width && row [x] == row [startGroup]) {
						if (rand.Next (9) < 7)
							maze [y + 1, 2 * x + 1] = '_';
						else
							notPut = true;
						x++;
					}
					x--;
					if (!notPut)
						maze [y + 1, 2 * (startGroup + rand.Next (x - startGroup + 1)) + 1] = ' ';
				}
				if (y < height - 1)
					for (short x = 0; x < width; x++)
						if (maze [y + 1, 2 * x + 1] == '_')
							row [x] = 255;
			}
			LastRow(ref row, ref maze);
		}

		void SetId(ref short[] row){
			short ID = 0;
			for (short x = 0; x < width; x++) {
				if (row[x] == 255)
				{
					bool unic = false;
					while (!unic)
					{
						unic = true;
						foreach (short a in row)
							if (ID == a)
								unic = false;
						ID++;
					}
					row[x] = (short)(ID-1);
				}
				else ID = (short)(row[x]+1);
			}
		}

		void SetupVerticalWalls(ref short[] row, ref char[,] maze, short y){
			for (short x = 0; x < width-1; x++) {
				if (row[x] == row[x + 1] || rand.Next(8) < 3)
					maze[y + 1, 2 * (x + 1)] = '|';
				else {
					short nextGroup = row[x+1];
					for (int i = x + 1; i < width; i++)
					{
						if(row[i] == nextGroup)
							row[i] = row[x];
					}
				}
			}
		}

		void LastRow(ref short[] row, ref char[,] maze)
		{
			short y = (short)(height - 1);
			SetId(ref row);
			SetupVerticalWalls(ref row, ref maze, y);
			for (short x = 0; x < width - 1; x++)
			{
				if (row[x] != row[x + 1])
				{
					maze[y + 1, 2 * (x + 1)] = ' ';
					short nextGroup = row[x + 1];
					for (int i = x + 1; i < width; i++)
					{
						if (row[i] == nextGroup)
							row[i] = row[x];
					}
				}
			}
		}
	}
}