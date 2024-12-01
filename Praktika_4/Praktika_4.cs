using System.Diagnostics.Contracts;
using System.Net.Http.Headers;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;

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
        public double GetArea(double Radius)
        {
            return Math.PI * Math.Pow(Radius, 2);
        }
        public void PrintInfo()
        {
            Console.WriteLine(_centerX);
            Console.WriteLine(_centerY);
            Console.WriteLine(radius);
            Console.WriteLine(GetArea(radius));
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
        public List<Circle> GenerateClassesCircle(int n)
        {
            List<Circle> circles = new List<Circle>();

            var rand = new Random();
            for (int i = 0; i <= n; i++)
            {
                int x = rand.Next();
                int y = rand.Next();
                int radius = rand.Next();
                circles.Add(new Circle(x, y, radius));
            }
            return circles;
        }
        public void Task4_1()
        {
            var circles = GenerateClassesCircle(100);
            var CircleGroupByRadius = circles.GroupBy(c => c.Radius);
            Console.WriteLine("Сгруппированные окружности по радиусу:");
            foreach (var group in CircleGroupByRadius)
            {
                Console.WriteLine($"Radius: {group.Key}");
            }
            circles.Clear();
        }
        // Сделать заранее заготовки для поиска центров окружностей, т.к поиск коэф затратен 
        public void Task4_2()
        {
            Console.WriteLine("Формирование запроса: Центры окружностей которые лежат на заданной прямой");
            Console.WriteLine("Задаётся прямая вида y = kx + b");

            List<Circle> circles = new List<Circle>
            {
                new Circle (5,6,12),
                new Circle (5,6,12),
                new Circle (5,1,4),
                new Circle (5,2,4),
                new Circle (6,5,6)
            };

            var results = new List<(double k, double b, List<Circle> circlesOnLine)>();

            for (int i = 0; i < circles.Count; i++)
            {
                for (int j = 0; j < circles.Count; j++)
                {
                    var c1 = circles[i];
                    var c2 = circles[j];
                    double deltaX = c2.CenterX - c1.CenterX;
                    if (Math.Abs(deltaX) < 1e-9) continue;
                    double k = (c2.CenterY - c1.CenterY) / deltaX;
                    double b = c1.CenterY - k * c1.CenterX;

                    var circleOnLine = circles.Where(c => c.CenterOnLine(k, b)).ToList();
                    if (circleOnLine.Count > 2)
                    {
                        results.Add((k, b, circleOnLine));
                    }
                }
            }
            if (results.Any())
            {
                foreach (var res in results)
                {
                    Console.WriteLine($"Прямая y = {res.k}x + {res.b}");
                    Console.WriteLine("Центры окружностей: ");
                    foreach (var circ in res.circlesOnLine)
                    {
                        Console.WriteLine($"{circ.CenterX}, {circ.CenterY}");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Не найденно прямой на которой лежат центры окружности");
            }

            circles.Clear();
            results.Clear();
        }
        public void PrintInfo()
        {
            var circles = GenerateClassesCircle(100);
            foreach (var i in circles)
            {
                Console.WriteLine("------------------------");
                Console.WriteLine(i.CenterX);
                Console.WriteLine(i.CenterY);
                Console.WriteLine(i.Radius);
            }
            Console.WriteLine("------------------------");
            circles.Clear();
        }
        public void Task4_3()
        {
            var circles = GenerateClassesCircle(100);
            List<double> circ = new List<double>();

            foreach (var c in circles)
            {
                circ.Add(c.GetArea(c.Radius));
            }
            Console.WriteLine($"Наибольший периметр окружности -> {circ.Max()}");
            Console.WriteLine($"Наименьший периметр окружности -> {circ.Min()}");



            circles.Clear();
            circ.Clear();

        }
        public void Task4_4()
        {
            var circles = GenerateClassesCircle(100);
            int rad = 0;
            Console.WriteLine("Поиск окружности по заданному радиусу.");
            Console.Write("Задайте радиус окружности: ");
            rad = Convert.ToInt32(Console.ReadLine());
            
            if (circles.Any())
            {
                foreach (var c in circles)
                {
                    if (c.Radius == rad) 
                    {
                        rad++;
                    }
                }
            }
            Console.WriteLine($"Найдено {rad} окружностей");
            
            circles.Clear();
        }
        public void Task4_5()
        {
            var circles = GenerateClassesCircle(5);
            Console.WriteLine($"Координаты центра -> X:{circles[0].CenterX}, Y:{circles[0].CenterY}");
            Console.WriteLine($"Радиус: {circles[0].Radius}");
            circles.Clear();
        }
        public void Task4_6()
        {
            List <Circle> circles = GenerateClassesCircle(100);

            var circleInFirstQuadrant = circles
                .Where(c => c.CenterX > 0 && c.CenterY > 0)
                .ToList();
            Console.WriteLine("Запрос: Найти окружности которые лежат в первой четверти координат");
            Console.WriteLine("Найденные окружности:");
            if (circleInFirstQuadrant.Any())
            {
                foreach (var c in circleInFirstQuadrant)
                {
                    Console.WriteLine($"Центр: {c.CenterX}, {c.CenterY}, Радиус: {c.Radius}");
                }
            }
            else 
            {
                Console.WriteLine("Окружности не найдены!");
            }
            circles.Clear();
        }
        public void Task4_7()
        {
            var circles = GenerateClassesCircle(20);
            var SortUpCircles = from c in circles
                              orderby c.Radius
                              select c;
            Console.WriteLine("Сортировка радиусов окружности:");
            foreach (var s in SortUpCircles)
            {
                Console.WriteLine(s.Radius);
            }
            circles.Clear();
        }
    }
    internal class Praktika_4
    {
        static void Main(string[] args)
        {
            Task1 task1 = new Task1();
            Task4 task4 = new Task4();
            //Раскоментировать для проверки работоспособности.
            //task1.Task_1();
            //task4.Task4_1();
            //task4.Task4_2();
            //task4.Task4_3();
            //task4.Task4_4();
            //task4.Task4_5();
            //task4.Task4_6();
            //task4.Task4_7();
        }
    }
}
