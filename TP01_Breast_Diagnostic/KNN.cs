using CsvHelper;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace TP01_Breast_Diagnostic
{
    internal class KNN : IKNN
    {

        public void Train(string filename_train_samples_csv, int k = 1, int distance = 1)
        {


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
            float distanceEclidienne = 0;

            for (int i = 0; i < first_sample.Features.Length; i++)
            {
                distanceEclidienne += (float)Math.Pow(first_sample.Features[i] - second_sample.Features[i], 2);

            }
            distanceEclidienne = (float)Math.Sqrt(distanceEclidienne);

            return distanceEclidienne;
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
            int cpt, n, i, j;
            float tmp;
            cpt = 10;
            n = 0;

            while (cpt < 0)
            {
                n = 3 * n + 1;
            }
            while (n != 0)
            {
                n = n / 3;
                for (i= 0; i < cpt; i++)
                {
                    tmp = distances[i];

                    j = i;
                   
                    while (j < n - 1 && distances[j - n] > tmp)
                    {
                        distances[j] = distances[j - n];
                        j = j - n;

                    }


                    distances[j] = tmp;

                }

            }


        }
        public List<Breast> ImportSamples(string filename_samples_csv)
        {


            var records = new List<Breast>();
            using (var reader = new StreamReader(filename_samples_csv))
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

                return records;
            }

        }

    }
}


