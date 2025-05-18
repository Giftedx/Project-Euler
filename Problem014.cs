namespace Project_Euler;

public class Problem014 : Problem {
    private static uint _threshold;

    public override object Solve() {
        return FindBestCandidate((uint)1e6);
    }

    private static uint FindBestCandidate(uint maxLimit) {
        _threshold = (uint)(maxLimit * 0.075) + 2;
        ushort[] lookupTable = GenerateSequenceTable();
        uint[] bestNumbers = new uint[16];
        uint[] bestScores = new uint[16];
        uint[] candidates = new uint[16];

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

            for (; currentCandidate < maxLimit; currentCandidate += 48) {
                if (currentCandidate % 9 == 4) continue;

                uint count = 0;
                uint depth = 1;
                ulong transformedValue = currentCandidate;
                transformedValue += currentCandidate >> 1;
                transformedValue++;

                RecursionStart:
                if ((transformedValue & 1) > 0) {
                    transformedValue += transformedValue >> 1;
                    transformedValue++;
                    depth++;
                } else {
                    transformedValue >>= 1;
                    count++;
                    if (transformedValue < _threshold) {
                        count += lookupTable[transformedValue];
                        goto ScoreCheck;
                    }
                }

                goto RecursionStart;

                ScoreCheck:
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

    private static uint ComputeScore(ulong number, ushort[] lookupTable) {
        return number < _threshold
            ? lookupTable[number]
            : (number & 1) > 0
                ? 2 + ComputeScore(number + number / 2 + 1, lookupTable)
                : 1 + ComputeScore(number / 2, lookupTable);
    }

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

            RecurseTable:
            if ((accumulator & 1) > 0) {
                accumulator += accumulator >> 1;
                accumulator++;
                temp2++;
            } else {
                accumulator >>= 1;
                temp1++;
                if (accumulator < seqIndex) {
                    temp1 += table[accumulator];
                    goto StoreValue;
                }
            }

            goto RecurseTable;

            StoreValue:
            table[seqIndex] = (ushort)(temp1 + temp2 * 2);
            seqIndex++;
        }

        return table;
    }
}