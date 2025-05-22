using System; // Ensure this is at the top of the file if not already.

namespace Project_Euler;

public class Problem019 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 19: Counting Sundays.
    /// Counts how many Sundays fell on the first of the month during the twentieth century (1 Jan 1901 to 31 Dec 2000).
    /// </summary>
    /// <returns>The number of Sundays that fell on the first of the month during the 20th century.</returns>
    public override object Solve() {
        return NumberOfSundays();
    }

    private int NumberOfSundays() {
        int sundayCount = 0;
        for (int year = 1901; year <= 2000; year++) {
            for (int month = 1; month <= 12; month++) {
                // Create a DateTime object for the first day of the given month and year.
                // Note: DateTime constructor is DateTime(int year, int month, int day)
                DateTime date = new DateTime(year, month, 1);
                
                // Check if the day of the week for this date is Sunday.
                if (date.DayOfWeek == DayOfWeek.Sunday) {
                    sundayCount++;
                }
            }
        }
        return sundayCount;
    }
}