using System;

namespace Project_Euler {
    public class Problem19 : Problem {
        public override void Solve() {
            Print(NumberOfSundays());
        }

        private int NumberOfSundays() {
            int numberOfSundays = 0;
            int dayOfTheWeek = 2;
            for (int year = 1901; year <= 2000; year++) {
                for (int month = 1; month <= 12; month++) {
                    for (int day = 1; day <= GetNumberOfDays(month, year); day++) {
                        dayOfTheWeek++;
                        if (dayOfTheWeek == 7) {
                            if (day == 1) numberOfSundays++;
                            dayOfTheWeek = 0;
                        }
                    }
                }
            }
            return numberOfSundays;
        }

        private int GetNumberOfDays(int month, int year) {
            if (month == 2) return year % 4 == 0 ? 29 : 28;
            if (month == 4 || month == 6 || month == 9 || month == 11) return 30;
            return 31;
        }
    }
}