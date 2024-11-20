using System.Diagnostics.Contracts;
using System.Net.Http.Headers;
using System.Runtime.ExceptionServices;

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
        public bool CenterOnLine(double k, double b)
        {
            return Math.Abs(CenterY - (k * CenterX + b)) < 1e-9;
        }
    }
    class Task4 : DebugPrinter
    {
        //List<Circle> circles = new List<Circle>();

        // Генерируем n классов Circle со случайными параметрами типа Double
        public static List<Circle> GenerateClassesCircle(int n)
        {
            List<Circle> circles = new List<Circle>();

            var rand = new Random();
            for (int i = 0; i <= n; i++)
            {
                circles.Add(new Circle(rand.NextDouble(), rand.NextDouble(), rand.NextDouble()));
            }
            return circles;
        }
        public void Task4_1()
        {
            var circles = GenerateClassesCircle(100);
            var CircleGroupByRadius = circles.GroupBy(c => c.Radius);
            foreach (var group in CircleGroupByRadius)
            {
                Console.WriteLine($"Radius: {group.Key}");
                foreach (var c in group)
                {
                    Console.WriteLine($" {c.Radius}");
                }
            }

        }
        public void Task4_2()
        {
            Console.WriteLine("Формирование запроса: Центры окружностей которые лежат на заданной прямой");
            Console.WriteLine("Задаётся прямая вида y = kx + b");

            Console.Write("Введите угловой коэффицент k: ");
            double k = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введите коэффицент b: ");
            double b = Convert.ToDouble(Console.ReadLine());

            List<Circle> circles = GenerateClassesCircle(100);
            var CircleOnLine = circles.Where(c => c.CenterOnLine(k,b)).ToList();
            if (CircleOnLine.Any())
            {
                Console.WriteLine("Найденые окружности: ");
                foreach (var circle in CircleOnLine)
                {
                    Console.WriteLine($"Центр: {circle.CenterX}, {circle.CenterY}, Радиус: {circle.Radius}");
                }
            }
            else 
            {
                Console.WriteLine("Окружности не найдены.");
            }
        }
        public void PrintInfo()
        {
            int n = 0;
            var circles = GenerateClassesCircle(100);
            foreach (var i in circles)
            {
                Console.WriteLine("------------------------");
                Console.WriteLine(i.CenterX);
                Console.WriteLine(i.CenterY);
                Console.WriteLine(i.Radius);
            }
            Console.WriteLine("------------------------");
        }
    }
    internal class Praktika_4
    {
        static void Main(string[] args)
        {
            Task1 task1 = new Task1();
            Task4 task4 = new Task4();
            //circle.PrintInfo();
            //task1.Task_1();
            //task4.GenerateClassesCircle(100);
            //task4.Task4_1();
            task4.Task4_2();
        }
    }
}
