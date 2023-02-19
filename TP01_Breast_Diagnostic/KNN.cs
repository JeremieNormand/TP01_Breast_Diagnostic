using CsvHelper;
using System.Globalization;

namespace TP01_Breast_Diagnostic;

class KNN : IKNN
{
    public void Train(string filename_train_samples_csv, int k = 1, int distance = 1)
    {
        var breasts = ImportSamples(filename_train_samples_csv);
        foreach (var breast in breasts)
        {
            Console.Write($"{breast.Label} :");
            foreach (var feature in breast.Features)
            {
                Console.Write($" {feature}");
            }
            Console.WriteLine();
        }
    }

    public float Evaluate(string filename_test_samples_csv)
    {
        return 0;
    }

    public char Predict(Breast sample_to_predict)
    {
        return ' ';
    }

    public float EuclideanDistance(Breast first_sample, Breast second_sample)
    {
        return 0;
    }

    public char Vote(List<char> sorted_labels)
    {
        return ' ';
    }

    public void ConfusionMatrix(List<char> predicted_labels, List<char> expert_labels, char[] labels)
    {

    }

    public void ShellSort(List<float> distances, List<char> labels)
    {

    }

    public List<Breast> ImportSamples(string filename_samples_csv)
    {
        var breasts = new List<Breast>();

        using (var reader = new StreamReader(filename_samples_csv))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                var breast = new Breast();

                breast.Features[0] = csv.GetField<float>("radius_worst");
                breast.Features[1] = csv.GetField<float>("area_worst");
                breast.Features[2] = csv.GetField<float>("perimeter_worst");
                breast.Features[3] = csv.GetField<float>("concave points_worst");
                breast.Features[4] = csv.GetField<float>("concave points_mean");
                breast.Features[5] = csv.GetField<float>("perimeter_mean");

                breast.Label = csv.GetField("diagnosis") == "B" ? false : true;

                breasts.Add(breast);
            }
        }
        return breasts;
    }
}
