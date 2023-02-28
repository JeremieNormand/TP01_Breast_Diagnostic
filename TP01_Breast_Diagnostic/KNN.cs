using CsvHelper;
using System.Globalization;

namespace TP01_Breast_Diagnostic;

class KNN : IKNN
{
    List<Breast> breasts;
    int k;

    public KNN()
    {
        breasts = new List<Breast>();
    }

    public void Train(string filename_train_samples_csv, int k = 1, int distance = 1)
    {
        breasts = ImportSamples(filename_train_samples_csv);
        this.k = k;
    }

    public float Evaluate(string filename_test_samples_csv)
    {
        var testBreasts = ImportSamples(filename_test_samples_csv);

        var predicted_labels = new List<char>();
        var expert_labels = new List<char>();
        float predicted = 0;
        for (int idx = 0; idx < testBreasts.Count; idx++)
        {
            predicted_labels.Add(Predict(testBreasts[idx]));
            expert_labels.Add(testBreasts[idx].Label == true ? 'M' : 'B');
            if (predicted_labels[idx] == expert_labels[idx]) predicted++;
        }

        ConfusionMatrix(predicted_labels, expert_labels, new char[2] { 'M', 'B'} );

        return predicted / testBreasts.Count;
    }

    public char Predict(Breast sample_to_predict)
    {
        var distances = new List<float>();
        var labels = new List<char>();
        foreach (var breast in breasts)
        {
            distances.Add(EuclideanDistance(breast, sample_to_predict));
            labels.Add(breast.Label == true ? 'M' : 'B');
        }

        ShellSort(distances, labels);

        return Vote(labels);
    }

    public float EuclideanDistance(Breast first_sample, Breast second_sample)
    {
        float sum = 0;
        for (int i = 0; i < first_sample.Features.Length; i++)
        {
            sum += (float)Math.Pow(first_sample.Features[i] - second_sample.Features[i], 2);
        }
        return (float)Math.Sqrt(sum);
    }

    public char Vote(List<char> sorted_labels)
    {
        int mCount = 0;
        int bCount = 0;
        for (int i = 0; i < k; i++)
        {
            if (sorted_labels[i] == 'M')
                mCount++;
            else
                bCount++;
        }

        char label;
        if (mCount > bCount)
            label = 'M';
        else if (bCount > mCount)
            label = 'B';
        else
        {
            if (new Random().Next(1) == 0)
                label = 'M';
            else
                label = 'B';
        }
        return label;
    }

    public void ConfusionMatrix(List<char> predicted_labels, List<char> expert_labels, char[] labels)
    {
        var matrix = new int[labels.Length, labels.Length];
        for (int idx = 0; idx < predicted_labels.Count; idx++)
        {
            if (predicted_labels[idx] == labels[0] && expert_labels[idx] == labels[0])
                matrix[0, 0]++;
            if (predicted_labels[idx] == labels[1] && expert_labels[idx] == labels[1])
                matrix[1, 1]++;
            if (predicted_labels[idx] == labels[0] && expert_labels[idx] == labels[1])
                matrix[1, 0]++;
            if (predicted_labels[idx] == labels[1] && expert_labels[idx] == labels[0])
                matrix[0, 1]++;
        }

        Console.WriteLine("Confusion Matrix :");
        Console.WriteLine();
        Console.WriteLine($"{"", -4}{labels[0], 8}{labels[1], 8}");
        Console.WriteLine($"{labels[0], -4}{matrix[0, 0], 8}{matrix[0, 1], 8}");
        Console.WriteLine($"{labels[1], -4}{matrix[1, 0], 8}{matrix[1, 1], 8}");
        Console.WriteLine();
    }

    public void ShellSort(List<float> distances, List<char> labels)
    {
        var tuples = new List<(float distance, char label)>();
        for (int i = 0; i < distances.Count; i++)
        {
            tuples.Add((distances[i], labels[i]));
        }

        int step = 0;
        while (step < tuples.Count)
        {
            step = 3 * step + 1;
        }

        while (step > 0)
        {
            step = step / 3;
            for (int i = step; i < tuples.Count; i++)
            {
                (float distance, char label) temporary = tuples[i];
                int j = i;
                while (j > step - 1 && tuples[j - step].distance > temporary.distance)
                {
                    tuples[j] = tuples[j - step];
                    j = j - step;
                }
                tuples[j] = temporary;
            }
        }

        for (int i = 0; i < tuples.Count; i++)
        {
            distances[i] = tuples[i].distance;
            labels[i] = tuples[i].label;
        }
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
