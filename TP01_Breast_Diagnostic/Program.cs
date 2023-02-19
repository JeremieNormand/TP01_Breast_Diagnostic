namespace TP01_Breast_Diagnostic;

class Program
{
    enum Mode
    {
        None,
        Evaluation,
        Prediction
    }

    static Mode mode = Mode.None;

    enum Key
    {
        e,
        p,
        t,
        k
    }

    static string dataFile = string.Empty;
    static string trainFile = string.Empty;

    static int kValue;

    static void Main(string[] arguments)
    {
        ParseArguments(arguments);

        if (ValidateArguments())
        {
            Console.WriteLine(mode);
            Console.WriteLine(dataFile);
            Console.WriteLine(trainFile);
            Console.WriteLine(kValue);
        }
    }

    static void ParseArguments(string[] arguments)
    {
        var tuples = new (string key, string value)[3];

        if (arguments.Length == tuples.Length * 2)
        {
            for (int i = 0; i < tuples.Length; i++)
                tuples[i] = (arguments[i * 2], arguments[i * 2 + 1]);
        }
        else
            Console.WriteLine("Invalid number of arguments");

        MapArguments(tuples);
    }

    static void MapArguments((string key, string value)[] tuples)
    {
        foreach (var tuple in tuples)
        {
            if (tuple.key == $"-{Key.e}")
            {
                if (mode == Mode.None)
                {
                    mode = Mode.Evaluation;
                    dataFile = tuple.value;
                }
                else
                {
                    Console.WriteLine("Invalid number of mode arguments");
                    break;
                }
            }
            if (tuple.key == $"-{Key.p}")
            {
                if (mode == Mode.None)
                {
                    mode = Mode.Prediction;
                    dataFile = tuple.value;
                }
                else
                {
                    Console.WriteLine("Invalid number of mode arguments");
                    break;
                }
            }

            if (tuple.key == $"-{Key.t}")
                trainFile = tuple.value;

            if (tuple.key == $"-{Key.k}")
                if (!int.TryParse(tuple.value, out kValue))
                {
                    Console.WriteLine("Invalid k number format");
                    break;
                }
        }
    }

    static bool ValidateArguments()
    {
        if (mode == Mode.None)
        {
            Console.WriteLine("No mode argument entered");
            return false;
        }

        if (dataFile == string.Empty || !dataFile.Contains(".csv"))
        {
            Console.WriteLine("Invalid data file");
            return false;
        }

        if (trainFile == string.Empty || !trainFile.Contains(".csv"))
        {
            Console.WriteLine("Invalid train file");
            return false;
        }

        if (kValue < 1)
        {
            Console.WriteLine("Invalid k value");
            return false;
        }

        return true;
    }
}