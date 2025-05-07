using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Project_Euler {
    public class Problem22 :  Problem {
        private List<string> _names;
        public override void Solve() {
            Stopwatch fileReadTimer =  Stopwatch.StartNew();
            _names = Library.ReadFile("names.txt");
            fileReadTimer.Stop();
            Print(SumNameScores());
            Console.WriteLine("File read in {0} ms", fileReadTimer.ElapsedMilliseconds);
        }
        
        private long SumNameScores(){
            _names = _names.OrderBy(line => line).ToList();
            long sum = 0;
            for (int i = 0; i < _names.Count; i++) {
                sum += NameScore(_names[i], i);
            }
            return sum;
        }

        private int NameScore(string s, int pos){
            int sum = s.Sum(c => c - 'A' + 1);
            return (pos + 1) * sum;
        }
    }
}