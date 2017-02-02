using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedClassDLL
{
    public class RuleSet
    {
        #region Membervariables
        private string name;
        private int numberOfQuestions;
        private int numberOfAnswers;
        private string[] attributeheader;
        private string[] attributetypeheader;
        private string[,] attributes;
        #endregion

        #region Constructor
        public RuleSet(string name, string[] attributeheader, string[] attributetypeheader, string[,] attributes)
        {
            this.name = name;
            this.numberOfQuestions = attributeheader.Length - 2;
            this.numberOfAnswers = attributes.GetLength(0);
            this.attributeheader = attributeheader;
            this.attributetypeheader = attributetypeheader;
            this.attributes = attributes;
        }
        #endregion
    }
}
