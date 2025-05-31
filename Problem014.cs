namespace Project_Euler;

public class Problem014 : Problem {
    private static uint _threshold;

    /// <summary>
    /// Solves Project Euler Problem 14: Longest Collatz sequence.
    /// Finds the starting number, under one million, that produces the longest Collatz chain.
    /// </summary>
    /// <returns>The starting number under one million that produces the longest Collatz chain.</returns>
    public override object Solve() {
        return FindBestCandidate((uint)1e6);
    }

    /// <summary>
    /// Finds the starting number producing the longest Collatz chain within a given range, processed in parallel threads.
    /// Uses numerous advanced heuristics and a lookup table for optimization.
    /// </summary>
    /// <param name="maxLimit">The upper limit (exclusive) for starting numbers.</param>
    /// <returns>The starting number under maxLimit that produces the longest chain found by this method/thread.</returns>
    private static uint FindBestCandidate(uint maxLimit) {
        _threshold = (uint)(maxLimit * 0.075) + 2;
        ushort[] lookupTable = GenerateSequenceTable();
        uint[] bestNumbers = new uint[16];
        uint[] bestScores = new uint[16];
        uint[] candidates = new uint[16];

        // Focusing on numbers with specific modular properties known to yield longer sequences or to optimize search space division.
        // Start with values where candidate % 6 = 1
        uint candidateSeed = (maxLimit / 2) | 1;
        candidateSeed += ((candidateSeed + 5) % 3) << 1;
        for (int i = 15; i >= 0; i -= 2) {
            candidates[i] = candidateSeed;
            candidateSeed += 6;
        }

        // Start with values where candidate % 6 = 3
        candidateSeed = (maxLimit / 2) | 1;
        candidateSeed += ((candidateSeed + 3) % 3) << 1;
        for (int i = 14; i >= 0; i -= 2) {
            candidates[i] = candidateSeed;
            candidateSeed += 6;
        }

        Parallel.For(0, 16, threadIndex => {
            uint currentCandidate = candidates[threadIndex];
            if (((currentCandidate + 3) & 7) == 0) return;

            uint maxScore = 0, bestCandidate = 0;

            // Skipping candidates based on modular arithmetic properties, e.g., numbers congruent to X mod 48 might have provably shorter chains or be covered by other paths.
            for (; currentCandidate < maxLimit; currentCandidate += 48) {
                // Further pruning based on modulo 9 properties.
                if (currentCandidate % 9 == 4) continue;

                uint count = 0; // Counts the n/2 steps
                uint depth = 1; // Counts the combined (3n+1)/2 steps
                ulong transformedValue = currentCandidate;
                transformedValue += currentCandidate >> 1; // Optimized (2*val + val)/2 = 1.5*val
                transformedValue++;                       // Optimized (3*val+1)/2 (initial step for odd currentCandidate, or part of (3*(val/2)+1)/2 if even)
                                                          // This logic appears to be part of an optimization where steps are combined.
                                                          // Assuming 'transformedValue' starts related to the Collatz sequence.

                while(transformedValue >= _threshold) { // Refactored from goto loop
                    if ((transformedValue & 1) > 0) { // If odd
                        // This is equivalent to (3*val + 1)/2, a 2-step Collatz operation (3n+1 followed by n/2).
                        transformedValue += transformedValue >> 1;
                        transformedValue++;
                        depth++;
                    } else { // If even
                        transformedValue >>= 1; // n/2 step
                        count++;
                        if (transformedValue < _threshold) {
                            count += lookupTable[transformedValue];
                            break; // Exit while loop, equivalent to goto ScoreCheck
                        }
                    }
                }
                // ScoreCheck: (equivalent to the point after break from while loop)
                // The score count + depth * 2 correctly represents the total standard Collatz steps.
                if (maxScore >= count + depth * 2) continue;
                maxScore = count + depth * 2;
                bestCandidate = currentCandidate;
            }

            bestNumbers[threadIndex] = bestCandidate;
            bestScores[threadIndex] = maxScore;
        });

        Array.Sort(bestScores, bestNumbers);

        for (int i = 14; i > 3; i--)
            if (bestScores[i] == bestScores[15]) {
                if (bestNumbers[i] < bestNumbers[15])
                    bestNumbers[15] = bestNumbers[i];
            } else {
                break;
            }

        return 2ul * bestNumbers[15] < maxLimit
            ? 2 * bestNumbers[15]
            : ComputeScore(bestNumbers[15] - 1, lookupTable) == bestScores[15]
                ? bestNumbers[15] - 1
                : bestNumbers[15];
    }

    /// <summary>
    /// Calculates the length of the Collatz sequence for a given number, using a lookup table for memoization.
    /// Note: The step calculation (odd: (3n+1)/2, even: n/2) means odd steps effectively count as 2 standard steps.
    /// </summary>
    /// <param name="number">The starting number.</param>
    /// <param name="lookupTable">Memoization table for sequence lengths of small numbers.</param>
    /// <returns>The length of the Collatz sequence.</returns>
    private static uint ComputeScore(ulong number, ushort[] lookupTable) {
        return number < _threshold
            ? lookupTable[number]
            : (number & 1) > 0
                ? 2 + ComputeScore(number + number / 2 + 1, lookupTable) // (3n+1)/2 step, counts as 2 standard steps
                : 1 + ComputeScore(number / 2, lookupTable);             // n/2 step, counts as 1 standard step
    }

    /// <summary>
    /// Generates a lookup table for Collatz sequence lengths for numbers up to a pre-defined threshold.
    /// Uses highly optimized, pattern-based logic for table population.
    /// </summary>
    /// <returns>An array where the index is the number and the value is its Collatz sequence length.</returns>
    private static ushort[] GenerateSequenceTable() {
        ushort[] table = new ushort[_threshold + 3];
        table[2] = 1;
        table[3] = 7;

        uint bitAdder = 8;
        uint skipAt11 = 11, lookupAt7 = 7, skipAt31 = 31, lookupAt27 = 27;
        uint seqIndex = 4, jumpIndex = 4;

        while (seqIndex < _threshold) {
            table[seqIndex] = (ushort)(table[seqIndex >> 1] + 1);
            seqIndex++;

            table[seqIndex] = (ushort)(table[jumpIndex] + 3);
            jumpIndex += 3;
            seqIndex++;

            table[seqIndex] = (ushort)(table[seqIndex >> 1] + 1);
            seqIndex++;

            bitAdder += 9;

            if (seqIndex == skipAt11) {
                table[seqIndex] = (ushort)(table[lookupAt7] - 2);
                seqIndex++;
                skipAt11 += 12;
                lookupAt7 += 8;
                continue;
            }

            if (seqIndex == skipAt31) {
                table[seqIndex] = (ushort)(table[lookupAt27] - 5);
                seqIndex++;
                skipAt31 += 36;
                lookupAt27 += 32;
                continue;
            }

            ulong accumulator = bitAdder;
            uint temp1 = 0;
            uint temp2 = 2;
            
            // Refactored goto loop for table generation
            while(accumulator >= seqIndex) {
                if ((accumulator & 1) > 0) { // If odd
                    accumulator += accumulator >> 1;
                    accumulator++;
                    temp2++;
                } else { // If even
                    accumulator >>= 1;
                    temp1++;
                    if (accumulator < seqIndex) {
                        temp1 += table[accumulator];
                        break; // Exit while loop, equivalent to goto StoreValue
                    }
                }
            }
            // StoreValue:
            table[seqIndex] = (ushort)(temp1 + temp2 * 2);
            seqIndex++;
        }

        return table;
    }
}