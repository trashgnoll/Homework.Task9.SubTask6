namespace ConsoleApp3
{
    internal class Program
    {
        public class EInputException : Exception
        {
            public EInputException()
            { }

            public EInputException(string message)
                : base(message)
            { }
        }
        public delegate void OnInput( int kind);
        public class TSorter
        {
            public event OnInput? OnSorterInput;
            protected virtual void SorterInput( int kind )
            {
                OnSorterInput?.Invoke( kind );
            }
            public void Process( int kind)
            {
                SorterInput( kind );
            }
        }
        static void Main()
        {
            TSorter sorter = new();
            sorter.OnSorterInput += OnInputEvent;
            try
            {
                Console.WriteLine("Enter 1 to sort A-Z or 2 to sort Z-A");
                string? answer = Console.ReadLine();
                if (answer is null || answer == string.Empty || !int.TryParse(answer, out int answerI) ||
                    (answerI != 1 && answerI != 2))
                    throw new EInputException("Incorrect input: must be \"1\" or \"2\"");
                else
                    sorter.Process(answerI);
            }
            catch( EInputException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally {}
        }
        public static void OnInputEvent( int kind )
        {
            string[] LastNames = { "Clarke", "Azimov", "Efremov", "Dekart", "Booba" };
            string[] sorted = LastNames;
            Array.Sort(sorted);
            Console.WriteLine("Result: " + string.Join(", ", (kind==1?sorted:sorted.Reverse())));
        }
    }
}