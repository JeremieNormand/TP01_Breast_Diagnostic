﻿using CsvHelper;
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
        return 0;
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

    }

    public void ShellSort(List<float> distances, List<char> labels)
    {
        var pairs = new List<(float distance, char label)>();
        for (int i = 0; i < distances.Count; i++)
        {
            pairs.Add((distances[i], labels[i]));
        }
        
        pairs.Sort((p1, p2) => p1.distance.CompareTo(p2.distance));
        for (int i = 0; i < pairs.Count; i++)
        {
            distances[i] = pairs[i].distance;
            labels[i] = pairs[i].label;
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
