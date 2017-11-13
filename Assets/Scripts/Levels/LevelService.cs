using System.Collections.Generic;
using Utils;

namespace Levels
{
    public class LevelService
    {
        public static Level Read(int levelNum)
        {
            if(!LevelRepository.IsInitialized(levelNum))
                LevelRepository.Initialize();
            
            return LevelRepository.Read(levelNum);
        }
        
        public static List<Level> ReadAll()
        {
            if(!LevelRepository.IsInitialized(0))
                LevelRepository.Initialize();
            
            List<Level> levels = new List<Levels.Level>(Const.LEVEL_COUNT);
            for(int i = 0; i < Const.LEVEL_COUNT; i++)
                levels.Add(LevelRepository.Read(i));
            return levels;
        }

        public static void Save(Level level, int levelNum)
        {
            LevelRepository.Save(level, levelNum);
        }
    }
}