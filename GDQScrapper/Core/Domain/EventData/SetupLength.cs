namespace GDQScrapper.Core.Domain.EventData
{
    public class SetupLenght
    {
        private string setupLength;

        public SetupLenght(string setupLength)
        {
            this.setupLength = setupLength;
        }

        public override string ToString()
        {
            return setupLength;
        }
    }
}
