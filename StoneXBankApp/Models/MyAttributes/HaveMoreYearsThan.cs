using System.ComponentModel.DataAnnotations;

namespace StoneXBankApp.Models.MyAttributes
{
    public class HaveMoreYearsThan : ValidationAttribute
    {
        private int _age;

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public HaveMoreYearsThan(int age)
        {
            Age = age;
        }

        public override bool IsValid(object value)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);

            DateTime date = Convert.ToDateTime(value);

            TimeSpan span = DateTime.Now - date;
            
            int years = (zeroTime + span).Year - 1;
            return Age > years;

        }
    }
}
