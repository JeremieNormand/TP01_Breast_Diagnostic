namespace TP01_Breast_Diagnostic
{
    interface IData
    {
        float[] Features { get; }
        bool Label { get; }
        void PrintInfo();
    }
}
