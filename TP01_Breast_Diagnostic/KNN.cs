namespace TP01_Breast_Diagnostic;

class KNN : IKNN
{
    public void Train(string filename_train_samples_csv, int k = 1, int distance = 1)
    {
        List<Breast> breasts = ImportSamples(filename_train_samples_csv);
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
        return new List<Breast>();
    }
}
