namespace Project_Euler {
    public abstract class Problem {
        public abstract void Solve();

        protected static void Print(object text) {
            if (text == null) throw new ArgumentNullException(nameof(text));
            Console.WriteLine(text.ToString());
        }
    }
}