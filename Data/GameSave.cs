namespace DrivingSimulator.Data
{
    internal class GameSave
    {
        public Player Player { get; private set; }

        public Location Location { get; private set; }

        public Settings Settings { get; private set; }

        public GameSave()
        {
            Player = new Player();
            Location = new Location();
            Settings = new Settings();
        }

    }

}
