using System;

namespace Minesweeper
{
    /// <summary>
    /// Class keeping the information on the score records
    /// </summary>
    public class ScoreRecord
    {
        private string personName;
        private int scorePoints;
        /// <summary>
        /// Keeps record of <paramref name="personName"/>
        /// </summary>
        /// <param name="personName">Name of record holder</param>
        /// <param name="points">The points</param>
        public ScoreRecord(string personName, int points)
        {
            this.PersonName = personName;
            this.ScorePoints = points;
        }

        /// <value>PersonName accesses the value of the personName data member</value>
       
        public string PersonName
        {
            get
            {
                return personName;
            }
            set
            {
                if (value == null || value == string.Empty)
                {
                    throw new ArgumentNullException("Person name connot be null or empty");
                }
                personName = value;
            }
        }

        /// <value>ScorePoints accesses the value of the scorePoints data member</value>
       
        public int ScorePoints
        {
            get
            {
                return scorePoints;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Person score connot be negative");
                }
                scorePoints = value;
            }
        }
    }
}