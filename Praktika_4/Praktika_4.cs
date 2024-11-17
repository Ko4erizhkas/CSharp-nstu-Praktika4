using System.Diagnostics.Contracts;
using System.Net.Http.Headers;

namespace Praktika_4
{
    interface DebugPrinter
    {
        void PrintInfo();
    }
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
    class Circle : DebugPrinter
    {
        private double radius;
        private double _centerX;
        private double _centerY;
        public double CenterX
        {
            get { return _centerX; }
            set { _centerX = value; }
        }
        public double CenterY
        {
            get { return _centerY; }
            set { _centerY = value; }
        }
        public double Radius
        {
            get { return radius; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Радиус должен быть больше нуля!");
                }
                radius = value;
            }
        }
        public Circle(double centerX, double centerY, double raduis)
        {
            CenterX = centerX;
            CenterY = centerY;
            Radius = raduis;
        }
        public Circle(double x1, double x2, double y1, double y2)
        {
            CenterX = (x1 + x2) / 2;
            CenterY = (y1 + y2) / 2;
            Radius = Math.Sqrt(Math.Pow (x2 - x1, 2) + Math.Pow (y2 - y1,2)) / 2;
        }
        public double GetArea()
        {
            return Math.PI * Math.Pow(Radius, 2);
        }
        public void PrintInfo()
        {
            Console.WriteLine(_centerX);
            Console.WriteLine(_centerY);
            Console.WriteLine(radius);
            Console.WriteLine(GetArea());
        }
    }
    class Task4
    {
        List<Circle> circles = new List<Circle>();

        public void Task4_1()
        {
            circles.Add(new Circle(5, 2, 65));
            circles.Add(new Circle(7, 4, 5.3));
            circles.Add(new Circle(5, 7, 5.3));
            circles.Add(new Circle(1, 2, 6.5));
            circles.Add(new Circle(12, 8, 65));
            circles.Add(new Circle(9, 4.5, 6.55));
        }
    }
    internal class Praktika_4
    {
        static void Main(string[] args)
        {
            Task1 task1 = new Task1();
            Circle circle = new Circle(2.213,4.12312,3.3246,6.43296785);
            circle.PrintInfo();
            //task1.Task_1();
        }
    }
}
