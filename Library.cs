using System.Numerics;

namespace Project_Euler {
    public static class Library { 
        
        public static List<string> ReadFile(string fileName) {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = basePath + fileName;
            string fileContent = File.ReadAllText(filePath);
            fileContent = fileContent.Replace("\"", "");
            List<string> data = fileContent.Split(',').ToList();
            return data;
        }
        
        public static int SumDigits(BigInteger digits) {
            BigInteger sum = 0;
            while (digits != 0) {
                BigInteger last =  digits % 10;
                sum += last;
                digits /= 10;
            }
            return (int)sum;
        }
        
        public static BigInteger Factorial(int n) {
            BigInteger factorial = 1;
            for (int i = 2; i <= n; i++) factorial *= i;
            return factorial;
        }
    }
}