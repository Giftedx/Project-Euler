namespace Project_Euler;

/// <summary>
/// Base class for all Project Euler problem solutions.
/// </summary>
public abstract class Problem<T> where T : struct
{
    /// <summary>
    /// Solves the problem and returns the result.
    /// </summary>
    /// <returns>The solution to the problem.</returns>
    public abstract T Solve();

    /// <summary>
    /// Gets the problem number from the class name.
    /// </summary>
    /// <returns>The problem number.</returns>
    public int GetProblemNumber()
    {
        string className = GetType().Name;
        if (className.StartsWith("Problem") && int.TryParse(className.AsSpan(7), out int id))
            return id;
        throw new InvalidOperationException($"Cannot extract problem number from class name: {className}");
    }
}

/// <summary>
/// Legacy base class for backward compatibility.
/// </summary>
public abstract class Problem
{
    public abstract object Solve();
}