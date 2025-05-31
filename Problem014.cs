namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 14: Longest Collatz sequence.
/// Finds the starting number under one million that produces the longest Collatz chain.
/// The Collatz sequence is defined as: n → n/2 (if n is even) or n → 3n+1 (if n is odd).
/// Further details can be found at https://projecteuler.net/problem=14
/// </summary>
public class Problem014 : Problem {
    /// <summary>
    /// Defines the upper limit for the pre-computed Collatz sequence lengths in the `lookupTable`.
    /// Numbers below this threshold will have their sequence lengths directly retrieved from the table,
    /// speeding up calculations for longer chains that eventually drop below this value.
    /// </summary>
    private static uint _threshold;

    /// <summary>
    /// Calculates the solution for Project Euler Problem 14 by finding the number under one million
    /// that produces the longest Collatz sequence.
    /// </summary>
    /// <returns>The starting number under one million that produces the longest Collatz sequence.</returns>
    public override object Solve() {
        return FindBestCandidate((uint)1e6); // Limit is 1 million
    }

    /// <summary>
    /// Orchestrates the search for the number under <paramref name="maxLimit"/> with the longest Collatz sequence.
    /// This method employs several optimizations:
    /// 1. Pre-computation: A 'lookupTable' stores sequence lengths for numbers below a calculated '_threshold'.
    /// 2. Parallel Processing: The search space is divided among 16 threads.
    /// 3. Candidate Selection:
    ///    - Initial candidates for threads are chosen strategically (numbers congruent to 1 or 3 mod 6,
    ///      starting from near maxLimit/2 and increasing). These congruences often relate to how sequences merge or their typical lengths.
    ///    - Threads iterate candidates with a step of 48, a large step chosen for diverse coverage.
    ///    - A skip condition `currentCandidate % 9 == 4` is used, likely a pattern-based optimization to avoid candidates known to have shorter sequences.
    /// 4. Optimized Sequence Calculation:
    ///    - The Collatz step `3n+1` (for odd n) is immediately followed by `n/2`, combined as `(3n+1)/2`.
    ///    - Bitwise operations are used for speed (e.g., `n >> 1` for `n/2`, `n & 1` for parity check).
    ///    - The length calculation distinguishes between `n/2` steps ('count') and `(3n+1)/2` steps ('depth'), where each 'depth' step adds 2 to the total length.
    /// 5. Result Refinement: Results from parallel threads are sorted, and the best candidate is chosen, considering that the actual starting number
    ///    of the longest sequence might be `2 * bestFoundCandidate` or `bestFoundCandidate - 1` due to sequence properties and search strategy.
    /// </summary>
    /// <param name="maxLimit">The upper bound (exclusive) for numbers to check.</param>
    /// <returns>The starting number under <paramref name="maxLimit"/> that produces the longest Collatz chain.</returns>
    private static uint FindBestCandidate(uint maxLimit) {
        // Threshold is set to about 7.5% of maxLimit. This size is a balance between
        // the time taken to generate the table and the speedup it provides.
        _threshold = (uint)(maxLimit * 0.075) + 2;
        ushort[] lookupTable = GenerateSequenceTable();

        // Arrays to store the best candidate and score found by each of the 16 threads.
        uint[] bestNumbers = new uint[16];
        uint[] bestScores = new uint[16]; // Scores corresponding to bestNumbers for each thread
        uint[] candidates = new uint[16]; // Starting candidate numbers for each thread

        // Initialize candidate starting points for parallel threads.
        // Strategy: Start searches from numbers around maxLimit / 2.
        // Numbers congruent to 1 or 3 mod 6 are chosen. These congruences can influence sequence behavior.
        // Two loops interleave these starting candidates.
        uint candidateSeed = (maxLimit / 2) | 1; // Ensure seed is odd, around half the limit.
        candidateSeed += ((candidateSeed + 5) % 3) << 1; // Adjust seed to be ~1 mod 6 (complex adjustment)
        for (int i = 15; i >= 0; i -= 2) { // Threads for ~1 mod 6 candidates
            candidates[i] = candidateSeed;
            candidateSeed += 6; // Next candidate in this congruence class
        }

        candidateSeed = (maxLimit / 2) | 1; // Reset seed
        candidateSeed += ((candidateSeed + 3) % 3) << 1; // Adjust seed to be ~3 mod 6 (complex adjustment)
        for (int i = 14; i >= 0; i -= 2) { // Threads for ~3 mod 6 candidates
            candidates[i] = candidateSeed;
            candidateSeed += 6; // Next candidate in this congruence class
        }

        // Parallel execution over 16 threads
        Parallel.For(0, 16, threadIndex => {
            uint currentCandidate = candidates[threadIndex];
            // Optimization: if ((currentCandidate + 3) & 7) == 0 means currentCandidate % 8 == 5.
            // This skips candidates that might quickly merge or have specific properties not conducive to long chains.
            if (((currentCandidate + 3) & 7) == 0) return;

            uint maxScore = 0, bestCandidate = 0;

            // Each thread iterates its candidates with a step of 48.
            // This large step ensures diverse coverage of the search space by different threads.
            for (; currentCandidate < maxLimit; currentCandidate += 48) {
                // Optimization: Skip candidates where currentCandidate % 9 == 4.
                // This is likely a pattern found to produce shorter sequences.
                if (currentCandidate % 9 == 4) continue;

                uint count = 0; // Tracks n/2 steps and lookupTable additions.
                uint depth = 1; // Tracks (3n+1)/2 steps. Initialized to 1 for the first step.

                // Initial step for an odd candidate: n -> (3n+1)/2
                // This is equivalent to: currentCandidate -> 3*currentCandidate + 1 -> (3*currentCandidate + 1)/2.
                // The code calculates (3*currentCandidate+1)/2 as currentCandidate + (currentCandidate>>1) + 1.
                ulong transformedValue = currentCandidate;
                transformedValue += currentCandidate >> 1; // currentCandidate * 1.5
                transformedValue++;                       // (3 * currentCandidate + 1) / 2 (integer arithmetic for odd currentCandidate)

                // Collatz sequence calculation loop
                RecursionStart:
                if ((transformedValue & 1) > 0) { // If transformedValue is odd
                    // Apply n -> (3n+1)/2 step
                    transformedValue += transformedValue >> 1; // transformedValue * 1.5
                    transformedValue++;                       // (3 * transformedValue + 1) / 2
                    depth++; // Each (3n+1)/2 step increments depth
                } else { // If transformedValue is even
                    transformedValue >>= 1; // Apply n -> n/2 step
                    count++; // Each n/2 step increments count
                    if (transformedValue < _threshold) { // If number is small enough for lookup table
                        count += lookupTable[transformedValue]; // Add pre-computed length
                        goto ScoreCheck; // Sequence computation finished for this candidate
                    }
                }
                goto RecursionStart; // Continue sequence

                ScoreCheck: // Check and update score for the current thread
                // Total length is count (n/2 steps) + depth * 2 (each (3n+1)/2 step counts as 2 operations: *3+1, then /2)
                if (maxScore >= count + depth * 2) continue;
                maxScore = count + depth * 2;
                bestCandidate = currentCandidate;
            }

            // Store best result for this thread
            bestNumbers[threadIndex] = bestCandidate;
            bestScores[threadIndex] = maxScore;
        });

        // Consolidate results from all threads
        Array.Sort(bestScores, bestNumbers); // Sort by scores, affecting bestNumbers accordingly

        // Refine the best candidate: if multiple threads found the same max score, pick the smallest starting number.
        // Starts from the second-to-last element (index 14) and compares with the highest score (index 15).
        for (int i = 14; i > 3; i--) { // The `> 3` seems arbitrary, perhaps top few distinct scores are enough.
            if (bestScores[i] == bestScores[15]) { // If score is same as the max
                if (bestNumbers[i] < bestNumbers[15]) { // If this candidate number is smaller
                    bestNumbers[15] = bestNumbers[i]; // Update to the smaller candidate
                }
            } else {
                break; // Scores are sorted, so no need to check further if a smaller score is found.
            }
        }

        // Final return logic:
        // The search strategy might find a number `N` whose sequence `... -> 2N` is long.
        // Or, a sequence for `N-1` might be equally long.
        // 1. If 2 * bestNumbers[15] is within limit, it's a potential candidate (its sequence is one step longer).
        //    This is because if N is odd, 2N -> N. If N is even, 2N -> N.
        //    However, the problem asks for the starting number. If we found X, 2X is a different starting number.
        //    The logic `2ul * bestNumbers[15] < maxLimit ? 2 * bestNumbers[15] : ...` seems to imply that if 2*best is valid, it's preferred.
        //    This is likely an error in interpretation or a specific optimization nuance.
        //    A Collatz sequence for X is X, X/2, ... or X, 3X+1, ...
        //    A sequence for 2X is 2X, X, ... (1 step longer than X if X was the next term).
        //    The problem asks for the number *under* maxLimit. This part of the logic might be trying to find the true origin of the "longest part" of a sequence.
        //    Given the problem statement, this final adjustment step is complex and might be specific to the search strategy's properties.
        //    A simpler interpretation is just returning bestNumbers[15].
        //    Let's assume bestNumbers[15] is the primary result.
        //    If 2 * bestNumbers[15] is also under maxLimit, its Collatz sequence length is `ComputeScore(2*bestNumbers[15])` which is `1 + ComputeScore(bestNumbers[15])`.
        //    This would be `1 + bestScores[15]`. This would make `2 * bestNumbers[15]` the winner if its score is higher.
        //    The current code `? 2 * bestNumbers[15]` implies it *is* the winner without re-evaluating its score. This is confusing.
        //    A common optimization is that if N is odd, its chain includes (3N+1). (3N+1)/2.
        //    If we only search odd numbers, we might miss an even number 2N whose chain is 2N -> N -> ...
        //    The current code's final check is:
        //    If `2 * bestNumber` is a valid candidate (under `maxLimit`), return `2 * bestNumber`. (This seems to assume its score will be higher implicitly).
        //    Else, check if `bestNumber - 1` has the same score. If so, return `bestNumber - 1` (smaller candidate).
        //    Else, return `bestNumber`.
        //    This logic needs careful interpretation in documentation.
        // For now, we will document what the code does.
        if (2ul * bestNumbers[15] < maxLimit) { // Check if 2*best is a valid starting number
             // If bestNumbers[15] is part of a chain that starts from 2*bestNumbers[15],
             // and 2*bestNumbers[15] is within the limit, it might be the true answer.
             // The sequence for 2N is (2N -> N -> ...), which is 1 step longer than the sequence from N.
             // So, if bestScores[15] was for N, then 2N would have score bestScores[15]+1.
             // This part of the code directly returns 2*bestNumbers[15] without explicitly checking if its score is indeed the new maximum.
             // This implies an assumption that if 2*N is valid, it's a better starting point for a longer sequence.
             // This is typically true if N was the first term after 2N in a sequence.
            return 2 * bestNumbers[15];
        } else if (ComputeScore(bestNumbers[15] - 1, lookupTable) == bestScores[15]) {
            // If N-1 has the same score as N, prefer N-1 (smaller starting number).
            // This can happen if N-1 is even and (N-1)/2 leads to a path that merges with N's path length calculation.
            return bestNumbers[15] - 1;
        } else {
            return bestNumbers[15];
        }
    }

    /// <summary>
    /// Recursively computes the length of the Collatz sequence starting from a given number.
    /// If the number is below the pre-computed `_threshold`, its sequence length is taken from `lookupTable`.
    /// Otherwise, the Collatz rules (n/2 for even, 3n+1 for odd) are applied, and the function calls itself.
    /// Each step in the sequence adds to the length: 1 for n/2, and 2 for (3n+1) then n/2 (as 3n+1 is always even).
    /// </summary>
    /// <param name="number">The starting number for the Collatz sequence.</param>
    /// <param name="lookupTable">The pre-computed table of sequence lengths for numbers below `_threshold`.</param>
    /// <returns>The length of the Collatz sequence for <paramref name="number"/>.</returns>
    private static uint ComputeScore(ulong number, ushort[] lookupTable) {
        if (number == 0) return 0; // Or throw, as Collatz is usually for positive integers.
        if (number == 1) return 1; // Base case for Collatz sequence.

        if (number < _threshold) {
            return lookupTable[number]; // Use pre-computed value if available.
        }

        if ((number & 1) > 0) { // If number is odd
            // Apply n -> 3n+1. Since 3n+1 is even, next step is (3n+1)/2.
            // This is (number * 3 + 1) / 2, or number + number/2 + 1 using integer arithmetic.
            // These two steps (to odd, then to even, then divide by 2) add 2 to the chain length.
            return 2 + ComputeScore(number + (number >> 1) + 1, lookupTable);
        } else { // If number is even
            // Apply n -> n/2. This adds 1 to the chain length.
            return 1 + ComputeScore(number >> 1, lookupTable);
        }
    }

    /// <summary>
    /// Pre-computes and stores the lengths of Collatz sequences for numbers up to `_threshold`.
    /// This table (`ushort[]`) is used by `ComputeScore` and `FindBestCandidate` to quickly retrieve
    /// sequence lengths for smaller numbers, significantly speeding up calculations for longer chains
    /// that eventually drop below the `_threshold`.
    /// The generation uses known patterns and recursive calculations for efficiency:
    /// - `table[even_n] = table[even_n / 2] + 1`.
    /// - Specific patterns like `table[odd_n] = table[related_smaller_n] + 3` are used for certain sequences.
    /// - Skip conditions (`skipAt11`, `skipAt31`) handle irregularities or apply specific known values.
    /// - A recursive helper (`RecurseTable` logic) calculates lengths for numbers not fitting simple patterns,
    ///   using `bitAdder` as a starting point for new sequences.
    /// </summary>
    /// <returns>An array where the index is the number (up to `_threshold`) and the value at that index
    /// is its Collatz sequence length.</returns>
    private static ushort[] GenerateSequenceTable() {
        ushort[] table = new ushort[_threshold + 3]; // +3 for safety margin with indexing.
        if (_threshold > 0) table[1] = 1; // Base case: sequence length of 1 is 1.
        if (_threshold > 1) table[2] = 2; // 2 -> 1 (length 2 including itself, or 1 step) - problem usually counts starting number. Let's assume it's steps to reach 1.
                                          // Problem context usually means length including the number itself.
                                          // So, 1 is length 1. 2->1 is length 2.
                                          // The reference code has table[2]=1, table[3]=7. This means steps *after* the first number.
                                          // Or, it is length of sequence including 1.
                                          // Let's assume table[n] = number of steps from n to 1.
                                          // 2->1 (1 step). So table[2]=1 is steps.
                                          // 3->10->5->16->8->4->2->1 (7 steps). So table[3]=7 is steps. This seems consistent.
                                          // The problem asks for "longest chain", implying number of terms. Score is often terms.
                                          // The ComputeScore returns 1 for n=1.
                                          // ComputeScore(2, table) -> 1 + ComputeScore(1) = 1+1=2.
                                          // ComputeScore(3, table) -> 2 + ComputeScore(5)
                                          //   ComputeScore(5) -> 2 + ComputeScore(8)
                                          //     ComputeScore(8) -> 1 + ComputeScore(4)
                                          //       ComputeScore(4) -> 1 + ComputeScore(2)
                                          //         ComputeScore(2) -> 1 + ComputeScore(1) = 2
                                          //       -> 1+2 = 3 (for 4)
                                          //     -> 1+3 = 4 (for 8)
                                          //   -> 2+4 = 6 (for 5)
                                          // -> 2+6 = 8 (for 3).
                                          // So the table stores number of terms in sequence.
                                          // If table[3]=7 in code, but ComputeScore(3) is 8, there's a mismatch.
                                          // The provided code has table[2]=1, table[3]=7. Let's use that.
                                          // This means the values in the table are "score" not "number of terms".
                                          // Let's assume the table stores "score" as defined by the problem's logic.
        table[2] = 1; // This might be "steps to reach 1" or a "score" value.
                      // If 2->1, score is 1.
        table[3] = 7; // If 3->10->5->16->8->4->2->1, score could be 7 (steps) or 8 (terms).
                      // The ComputeScore logic: (odd) 2 + func(), (even) 1 + func().
                      // ComputeScore(1) = 1 (base from lookup or threshold)
                      // ComputeScore(2) = 1 + CS(1) = 2.
                      // ComputeScore(3) = 2 + CS(5). CS(5)=2+CS(8). CS(8)=1+CS(4). CS(4)=1+CS(2)=1+2=3. CS(8)=1+3=4. CS(5)=2+4=6. CS(3)=2+6=8.
                      // So, the table values should align with ComputeScore.
                      // If table[2]=1, then ComputeScore(2) should be 1 if lookup is used.
                      // This implies the table stores values that are directly comparable to ComputeScore results.
                      // The code's GenerateSequenceTable is highly optimized and might use a specific definition of "score" for its patterns.

        uint bitAdder = 8; // Starting point for sequences calculated by RecurseTable logic.
        // These are control variables for specific optimized insertion patterns or corrections.
        uint skipAt11 = 11, lookupAt7 = 7, skipAt31 = 31, lookupAt27 = 27;
        uint seqIndex = 4; // Current index being filled in the table.
        uint jumpIndex = 4; // Used for a specific pattern: table[seqIndex] = table[jumpIndex] + 3.

        while (seqIndex < _threshold) {
            // Pattern: Fill entry for an even number (seqIndex). Length is 1 + length of (seqIndex/2).
            table[seqIndex] = (ushort)(table[seqIndex >> 1] + 1);
            seqIndex++;
            if (seqIndex >= _threshold) break;

            // Pattern: Fill entry for an odd number (seqIndex). Uses a "jump" based on `jumpIndex`.
            // This pattern likely corresponds to sequences like: seqIndex -> (3*seqIndex+1)/2 -> ... -> jumpIndex
            // where the intermediate steps sum to 3.
            table[seqIndex] = (ushort)(table[jumpIndex] + 3);
            jumpIndex += 3; // The next `jumpIndex` for this pattern.
            seqIndex++;
            if (seqIndex >= _threshold) break;

            // Pattern: Fill entry for the next even number.
            table[seqIndex] = (ushort)(table[seqIndex >> 1] + 1);
            seqIndex++;
            if (seqIndex >= _threshold) break;

            bitAdder += 9; // Update starting point for general recursion, seems to target specific odd numbers.

            // Special case corrections or pattern overrides.
            // These handle points where the general patterns might not hold or can be done faster.
            if (seqIndex == skipAt11) {
                table[seqIndex] = (ushort)(table[lookupAt7] - 2); // Correct value for 11 based on 7.
                seqIndex++;
                skipAt11 += 12; // Schedule next skip for this pattern.
                lookupAt7 += 8;
                if (seqIndex >= _threshold) break;
                continue; // Skip general recursion for this specific seqIndex.
            }

            if (seqIndex == skipAt31) {
                table[seqIndex] = (ushort)(table[lookupAt27] - 5); // Correct value for 31 based on 27.
                seqIndex++;
                skipAt31 += 36; // Schedule next skip.
                lookupAt27 += 32;
                if (seqIndex >= _threshold) break;
                continue; // Skip general recursion.
            }

            // General Collatz sequence calculation for current `seqIndex` (which should be odd here).
            // `bitAdder` is the starting number for this calculation.
            // `temp1` will count `n/2` steps.
            // `temp2` will count `(3n+1)/2` type steps (each effectively 2 operations). Initialized to 2.
            ulong accumulator = bitAdder;
            uint temp1_steps_div_by_2 = 0;
            uint temp2_steps_3n_plus_1_div_by_2 = 2; // Starts at 2, consistent with 'depth' in FindBestCandidate.

            RecurseTable: // Label for goto, simulates recursion.
            if ((accumulator & 1) > 0) { // If accumulator is odd
                accumulator += accumulator >> 1; // accumulator = (3*accumulator+1)/2
                accumulator++;
                temp2_steps_3n_plus_1_div_by_2++;
            } else { // If accumulator is even
                accumulator >>= 1; // accumulator = accumulator/2
                temp1_steps_div_by_2++;
                if (accumulator < seqIndex) { // If it drops into already computed part of the table
                    temp1_steps_div_by_2 += table[accumulator]; // Add pre-computed steps/score
                    goto StoreValue; // Calculation finished.
                }
            }
            goto RecurseTable; // Continue sequence.

            StoreValue: // Store the calculated score.
            // The score is (n/2 steps) + ( (3n+1)/2 steps ) * 2.
            table[seqIndex] = (ushort)(temp1_steps_div_by_2 + temp2_steps_3n_plus_1_div_by_2 * 2);
            seqIndex++;
        }
        return table;
    }
}