namespace Praktika_4
{
    class Task1
    {
        public void Task_1()
        {
            Console.Write("Введете длину месяца: ");

            short choice = Convert.ToInt16(Console.ReadLine());
            string[] month = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            var selectMonth = from m in month
                              where choice == m.Length
                              select m;
            Console.WriteLine($"Месяцы соотвествующие длине {choice}");
            foreach (var s in selectMonth) 
            {
                Console.WriteLine(s);
            }
            
            var selectedSeason = from m in month
                                 where (m == "January" || m == "February" || m == "December" || m == "June" || m == "July" || m == "August")
                                 select m;
            Console.WriteLine("Список летних и зимних месяцев:");
            foreach (var s in selectedSeason) 
            {
                Console.WriteLine(s); 
            }
            

            var sortMonth = from m in month
                            orderby m
                            select m;
            Console.WriteLine("Отсортированный список месяцев:");
            foreach (var s in sortMonth)
            {
                Console.WriteLine(s);
            }

            char ch = 'u';
            var searchMonth = from m in month
                              where (m.Any(c => c == ch)) && (m.Length > 4)
                              select m;
            Console.WriteLine(@"Месяцы с буквой 'u' и длиной не менее 4 ");
            foreach (var s in searchMonth)
            {
                Console.WriteLine(s);
            }
        }
    }
    class Circle
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Radius { get; set; }
        public Circle(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
        public Circle()
        {
            this.X = 0;
            this.Y = 0;
        }
    }
    internal class Praktika_4
    {
        static void Main(string[] args)
        {
            Task1 task1 = new Task1();
            task1.Task_1();
        }
    }
}
