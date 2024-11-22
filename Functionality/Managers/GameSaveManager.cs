using DrivinSim.Data;
using System.Collections.Generic;

namespace DrivinSim.Functionality
{
    internal class GameSaveManager
    {
        public GameSave CurrentSave { get; private set; }
        private readonly List<GameSave> gameSaves;

        public GameSaveManager()
        {
            gameSaves = new();
            CurrentSave = new();

            LoadSaves();
        }

        public void LoadSaves()
        {
            if (gameSaves.Count == 0)
            {
                NewGame();
                return;
            }

            // Deserialize gameSaves from JSON here

        }

        public void SaveGame()
        {
            gameSaves.Add(CurrentSave);
            // Serialize gameSaves to JSON here
        }


        public void NewGame()
        {
            CurrentSave = new();
            SaveGame();
            return;
        }
    }
}
