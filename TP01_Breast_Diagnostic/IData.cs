﻿namespace TP01_Breast_Diagnostic
{
    internal interface IData
    {
        float[] Features { get; }
        bool Label { get; }
        void PrintInfo();
    }
}
