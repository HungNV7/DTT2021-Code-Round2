using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTT2021_Round2.sample.dtos
{
    public class Question
    {
        public int ID { get; set; }
        public string Detail { get; set; }
        public string Answer { get; set; }
        public int NumberOfDigits { get; set; }

        public Question()
        {

        }

        public Question(int id, string detail, string answer, int numberOfDigits)
        {
            this.ID = id;
            this.Detail = detail;
            this.Answer = answer;
            this.NumberOfDigits = numberOfDigits;
        }
    }
}
