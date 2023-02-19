using CsvHelper.Configuration.Attributes;

namespace TP01_Breast_Diagnostic;

class Breast : IData
{
    
    public float[] Features { get; set; }
    
    public bool Label { get; set; }
    
    public Breast()
    {
        Features = new float[6];
    }

    public void PrintInfo()
    {

    }
}
