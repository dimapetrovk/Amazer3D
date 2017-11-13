using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Utils;

namespace Levels
{
    class LevelRepository
    {

        public static bool IsInitialized(int levelNum)
        {
            return File.Exists(Application.persistentDataPath + "level" + levelNum + ".dat");
        }
        
        public static Level Read(int levelNum)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "level" + levelNum + ".dat", FileMode.Open);
            Level data = (Level)bf.Deserialize(file);
            file.Close();
            return data;
        }
        
        public static void Save(Level level, int levelNum)
        {			
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "level" + levelNum + ".dat");
            bf.Serialize(file, level);
            file.Close();
        }
        
        public static void Initialize()
        {
            Level[] levels = new Level[Const.LEVEL_COUNT];
            levels[0] = new Level(7, 7, 34);
            levels[1] = new Level(10, 10, 80);
            levels[2] = new Level(15, 15, 80);
            levels[3] = new Level(20, 20, 32);
            
            BinaryFormatter bf = new BinaryFormatter();
            for (int levelNum = 0; levelNum < Const.LEVEL_COUNT; levelNum++)
            {
                FileStream file = File.Create(Application.persistentDataPath + "level" + levelNum + ".dat");
                
                if(levelNum > 0)
                    levels[levelNum].locked = true;
                
                bf.Serialize(file, levels[levelNum]);
                file.Close();
            }
        }
    }
}