namespace Project_Euler;

/// <summary>
/// Solves Project Euler Problem 19: Counting Sundays.
/// Calculates how many Sundays fell on the first of the month during the twentieth century.
/// </summary>
public class Problem019 : Problem {
    /// <summary>
    /// Solves Project Euler Problem 19: Counting Sundays.
    /// </summary>
    /// <returns>The total number of Sundays that fell on the first of a month (1901-2000).</returns>
    public override object Solve() {
        return NumberOfSundays();
    }

    /// <summary>
    /// Counts Sundays on the 1st of the month.
    /// </summary>
    private int NumberOfSundays() {
        int sundays = 0;
        int dayOfWeek = 2; // 1 Jan 1901 was Tuesday

        for (int year = 1901; year <= 2000; year++) {
            for (int month = 1; month <= 12; month++) {
                if (dayOfWeek == 0) sundays++;
                dayOfWeek = (dayOfWeek + GetNumberOfDays(month, year)) % 7;
            }
        }
        return sundays;
    }

    /// <summary>
    /// Returns number of days in a month.
    /// </summary>
    private int GetNumberOfDays(int month, int year) {
        if (month == 4 || month == 6 || month == 9 || month == 11) return 30;
        if (month == 2) return IsLeapYear(year) ? 29 : 28;
        return 31;
    }

    /// <summary>
    /// Checks if a year is a leap year.
    /// </summary>
    private bool IsLeapYear(int year) {
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }
}
