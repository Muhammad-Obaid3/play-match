using System;

namespace PlayMatch
{
    [Serializable]
    public class GameData
    {
        public bool[] CardStates { get; set; }
        public int Score { get; set; }

        public GameData(bool[] cardStates, int score)
        {
            this.CardStates = cardStates;
            this.Score = score;
        }
    }
}

