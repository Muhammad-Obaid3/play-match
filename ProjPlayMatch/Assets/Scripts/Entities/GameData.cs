using System;

namespace PlayMatch
{
    [Serializable]
    public class GameData
    {
        public bool[] cardStates { get; set; }
        public int score { get; set; }

        public GameData(bool[] cardStates, int score)
        {
            this.cardStates = cardStates;
            this.score = score;
        }
    }
}

