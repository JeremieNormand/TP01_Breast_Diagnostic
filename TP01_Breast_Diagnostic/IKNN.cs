namespace TP01_Breast_Diagnostic;

interface IKNN
{
    /*  main methods  */

    void Train(string filename_train_samples_csv, int k = 1, int distance = 1);
    float Evaluate(string filename_test_samples_csv);
    char Predict(Breast sample_to_predict);

    /*  utils  */

    float EuclideanDistance(Breast first_sample, Breast second_sample);
    char Vote(List<char> sorted_labels);
    void ConfusionMatrix(List<char> predicted_labels, List<char> expert_labels, char[] labels);
    void ShellSort(List<float> distances, List<char> labels);
    List<Breast> ImportSamples(string filename_samples_csv);
}