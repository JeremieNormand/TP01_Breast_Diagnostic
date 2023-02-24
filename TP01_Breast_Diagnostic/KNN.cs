using CsvHelper;
using System.Globalization;

namespace TP01_Breast_Diagnostic
{
    internal class KNN : IKNN
    {
        public void Train(string filename_train_samples_csv, int k = 1, int distance = 1)
        {
            var records = new List<Breast>();
            using (var reader = new StreamReader(filename_train_samples_csv))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    //  recuperer le champ diagnoctic a partir du fichier csv*/
                    char label = csv.GetField<char>("diagnosis");
                    float[] features =
                    {
                        //reccuperation des des autres champs dans le fichier csv sauf le cghamps diognostic et selected*/
                         float.Parse(csv.GetField("radius_worst")),
                         float.Parse(csv.GetField("area_worst")),
                         float.Parse(csv.GetField("perimeter_worst")),
                         float.Parse(csv.GetField("concave points_worst")),
                         float.Parse(csv.GetField("concave points_mean")),
                         float.Parse(csv.GetField("perimeter_mean")),


                    };
                    // creer un objet de type Breast*/
                    var record = new Breast(features, label);
                    // ajouter tout les objest creer dans la liste records
                    records.Add(record);

                }



            }





        }




        public float Evaluate(string filename_test_samples_csv)
        {
            return 5;
        }
        public char Predict(Breast sample_to_predict)
        {

            return 'j';


        }
        public float EuclideanDistance(Breast first_sample, Breast second_sample)
        {
            return 6;
        }
        public char Vote(List<char> sorted_labels)
        {
            return 'j';
        }
        public void ConfusionMatrix(List<char> predicted_labels, List<char> expert_labels, char[] labels)
        {

        }
        public void ShellSort(List<float> distances, List<char> labels)
        {

        }
        public List<Breast> ImportSamples(string filename_samples_csv)
        {
            List<Breast> breasts = new List<Breast>();
            return breasts;
        }

    }
}
