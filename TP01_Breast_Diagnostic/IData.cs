namespace TP01_Breast_Diagnostic
{
    interface IData
    {
        float[] Features { get; }
        char Label { get; }
        void PrintInfo();
    }
}
