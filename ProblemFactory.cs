using System.Diagnostics;
using static System.Reflection.Assembly;

namespace Project_Euler;

public static class ProblemFactory {
    private static readonly Dictionary<int, Type> ProblemTypes;

    static ProblemFactory() {
        ProblemTypes = GetExecutingAssembly().GetTypes()
                                             .Where(t => typeof(Problem).IsAssignableFrom(t) && !t.IsAbstract)
                                             .Select(t => new {
                                                 Type = t,
                                                 Id = ExtractProblemId(t.Name)
                                             })
                                             .Where(x => x.Id.HasValue && x.Id.Value < 900) // Exclude problem 999 and similar
                                             .ToDictionary(x => {
                                                 Debug.Assert(x.Id != null); // Id is checked by x.Id.HasValue
                                                 return x.Id!.Value; // Null-forgiving: Id is checked by x.Id.HasValue
                                             }, x => x.Type);
    }

    public static Problem CreateProblem(int id) {
        if (ProblemTypes.TryGetValue(id, out Type? type)) {
            // Type 'type' here is known to be non-null if TryGetValue returns true and it's a value type,
            // but for reference types, it can be null if the dictionary stores nulls.
            // However, our dictionary construction ensures types are non-null.
            // So, type will not be null here.
            object? instance = Activator.CreateInstance(type!); // Null-forgiving: type is guaranteed by TryGetValue and dictionary construction.
            if (instance is Problem problemInstance) {
                return problemInstance;
            }
            // It's also possible Activator.CreateInstance itself returns null
            if (instance == null) {
                 throw new InvalidOperationException($"Activator.CreateInstance returned null for type {type!.FullName} for Problem{id:D3}.");
            }
            // If it's not null but not a Problem instance (shouldn't happen if type is correct)
            throw new InvalidOperationException($"Created instance for Problem{id:D3} was not of type Problem (actual type: {instance.GetType().FullName}).");
        }

        throw new ArgumentOutOfRangeException(nameof(id), $"Problem with ID {id} not found.");
    }

    public static int SolvedProblems() {
        return ProblemTypes.Count;
    }

    private static int? ExtractProblemId(string typeName) {
        if (typeName.StartsWith("Problem") && int.TryParse(typeName.AsSpan(7), out int id)) return id;

        return null;
    }
}