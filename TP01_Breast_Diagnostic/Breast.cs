namespace TP01_Breast_Diagnostic
{
    internal class Breast : IData
    {
       
       
        public float[] Features { get; }
        public char Label { get; }

        public Breast(float[] features, char label)
        {
            Features = features;
            Label = label;
        }

        public void PrintInfo() { }


    }
}
