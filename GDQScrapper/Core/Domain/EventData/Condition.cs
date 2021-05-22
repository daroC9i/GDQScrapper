namespace GDQScrapper.Core.Domain.EventData
{
    public class Condition
    {
        private string condition;

        public Condition(string condition)
        {
            this.condition = condition;
        }

        public override string ToString()
        {
            return condition;
        }
    }
}
