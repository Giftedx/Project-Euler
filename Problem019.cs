namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 19: Counting Sundays.
/// Further details can be found at https://projecteuler.net/problem=19
/// </summary>
public class Problem019 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 19: Counting Sundays.
    /// Calculates how many Sundays fell on the first of the month during the twentieth century (1 Jan 1901 to 31 Dec 2000).
    /// </summary>
    /// <returns>The total number of Sundays that fell on the first of a month during the 20th century.</returns>
    public override object Solve() {
        return NumberOfSundays();
    }

    /// <summary>
    /// Counts the number of Sundays that fell on the first day of a month between 1 Jan 1901 and 31 Dec 2000.
    /// The algorithm starts by establishing the day of the week for 1 Jan 1901 (Tuesday).
    /// It then iterates through each month of each year in the specified range.
    /// For each month, it first checks if the 1st of that month is a Sunday.
    /// Then, it advances the day of the week by the number of days in the current month to find the
    /// day of the week for the 1st of the next month.
    /// Day encoding: 0 = Sunday, 1 = Monday, ..., 6 = Saturday.
    /// 1 Jan 1900 was a Monday. 1900 was not a leap year (365 days % 7 = 1).
    /// So, 1 Jan 1901 was a Tuesday.
    /// </summary>
    /// <returns>The total count of Sundays that were the first day of a month.</returns>
    private int NumberOfSundays() {
        int numberOfSundays = 0;
        int dayOfTheWeek = 2; // 0=Sun, 1=Mon, ..., 6=Sat.  1 Jan 1901 was a Tuesday.

        for (int year = 1901; year <= 2000; year++) {
            for (int month = 1; month <= 12; month++) {
                // Check if the first day of the current month is a Sunday
                if (dayOfTheWeek == 0) {
                    numberOfSundays++;
                }
                // Advance the day of the week by the number of days in the current month
                dayOfTheWeek = (dayOfTheWeek + GetNumberOfDays(month, year)) % 7;
            }
        }
        return numberOfSundays;
    }

    /// <summary>
    /// Gets the number of days in a specified month and year.
    /// Accounts for leap years when determining the number of days in February.
    /// </summary>
    /// <param name="month">The month (1 for January, 12 for December).</param>
    /// <param name="year">The year.</param>
    /// <returns>The number of days in the given month and year.</returns>
    private int GetNumberOfDays(int month, int year) {
        return month switch {
            4 or 6 or 9 or 11 => 30, // April, June, September, November have 30 days
            2 => IsLeapYear(year) ? 29 : 28, // February has 29 days in a leap year, 28 otherwise
            _ => 31 // All other months (January, March, May, July, August, October, December) have 31 days
        };
    }

    /// <summary>
    /// Determines if a given year is a leap year.
    /// A year is a leap year if it is divisible by 4, unless it is divisible by 100 but not by 400.
    /// </summary>
    /// <param name="year">The year to check.</param>
    /// <returns>True if the year is a leap year; false otherwise.</returns>
    private bool IsLeapYear(int year) {
        // Leap year conditions:
        // 1. Divisible by 4, but not by 100 unless...
        // 2. Divisible by 400.
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }
}