namespace Project_Euler;

public class ProblemData
{
    public int Index { get; }
    public List<double> Times { get; }
    public string Result { get; set; } = "";

    public ProblemData(int index, int runs)
    {
        Index = index;
        Times = new List<double>(runs);
    }

    public double AverageTime => Times.Any() ? Times.Average() : 0.0;
    public double MinTime => Times.Any() ? Times.Min() : 0.0;
    public double MaxTime => Times.Any() ? Times.Max() : 0.0;
}

public class BenchmarkData
{
    public int SlowestProblem;
    public double SlowestTime = double.MinValue;
    public double TotalTime;
}
