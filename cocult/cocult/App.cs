using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using cocult.Comands;

namespace cocult
{
    /// <summary>
    /// класс для работы с приложением
    /// </summary>
    class App
    {
        /// <summary>
        /// список для хранения данных о введенных фигурах 
        /// </summary>
        private ListFigure<Figure> listEnteredShapes = new ListFigure<Figure>();

        /// <summary>
        /// путь к файлу с введенными фигурами
        /// </summary>
        private string path = @"C:\Users\aleks\Desktop\Практика\cocult\список введенных фигур.txt";

        /// <summary>
        /// лист для хранения команд
        /// </summary>
        private List<IComand> _comands = new List<IComand>(); 

        /// <summary>
        /// конструктор, который добавляет команды
        /// </summary>
        public App()
        {
            Reg();
        }

        /// <summary>
        /// метод который запускает программу
        /// </summary>
        public void Run()
        {
            ReaderFile(path);
            while (true)
            {       
                Console.WriteLine($"\n1 - квадрат, вводим сторону одну" +
                    "\n2 - прямоугольник, вводим два стороны" +
                    "\n3 - Круг, вводим радиус" +
                    "\n4 - треугольник, вводим три стороны" +
                    "\n5 - многоугольник" +
                    "\n6 - вывести сумму всех периметров" +
                    "\n7 - вывести сумму всех площадей" +
                    "\n8 - вывод списка всех фигур ранее введенных" +
                    "\n9 - вывод суммы площадей всех квадратов" +
                    "\n10 - вывод суммы площадей всех кругов" +
                    "\n11 - вывод суммы площадей всех прямоугольников" +
                    "\n12 - вывод суммы площадей всех треугольников" +
                    "\n13 - вывод суммы площадей всех многоугольников" +
                    "\n14 - вывод суммы периметров всех квадратов" +
                    "\n15 - вывод суммы периметров всех кругов" +
                    "\n16 - вывод суммы периметров всех прямоугольников" +
                    "\n17 - вывод суммы периметров всех треугольников" +
                    "\n18 - вывод суммы периметров всех многоугольников" +
                    "\n19 - выйти из программы\n");

                Console.WriteLine("Введите номер действия:");
                string comand = Console.ReadLine();
                string[] words;

                if (comand.Split(" ", 2).Length == 2)
                { 
                    words = comand.Split(" ", 2);
                    SearhComand(words[0], words[1]);
                } 
                else
                {
                    words = comand.Split(" ", 1);
                    SearhComand(words[0], "");
                }
                WriteFile(listEnteredShapes,path);
            }
        }
        /// <summary>
        /// метод для поиска команды
        /// </summary>
        /// <param name="comand">команда</param>
        private void SearhComand(string comand,string parametrs)
        {
            foreach(IComand el in _comands)
            {
                if (el.NameComand == comand)
                {
                    el.Execute(parametrs);
                    return;
                }

            }
                Console.Clear();
            Console.WriteLine("Ошибка в записи команды");
        }

        /// <summary>
        /// метод для преобразования строки в параметры для фигур
        /// </summary>
        /// <param name="parametr"></param>
        /// <returns></returns>
        public static List<int> ToParametrs(string parametr)
        {
            string[] a = parametr.Split();
            List<int> parametrs = new List<int>();

            foreach (var c in a)
            {

                if (int.TryParse(c, out int d))
                {
                    parametrs.Add(Convert.ToInt32(c));
                }
            }

            return parametrs;
        }

        /// <summary>
        /// метод для регистрации команд
        /// </summary>
        private void Reg()
        {
            _comands.Add(new ComandSquare(ref listEnteredShapes));
            _comands.Add(new ComandRectangle(ref listEnteredShapes));
            _comands.Add(new ComandTringle(ref listEnteredShapes));
            _comands.Add(new ComandCircle(ref listEnteredShapes));
            _comands.Add(new ComandPolygon(ref listEnteredShapes));
            _comands.Add(new ComandAllP(ref listEnteredShapes));
            _comands.Add(new ComandAllS(ref listEnteredShapes));
            _comands.Add(new ComandPrintEnteredShape(ref listEnteredShapes));
            _comands.Add(new ComandPrintSumSSquare(ref listEnteredShapes));
            _comands.Add(new ComandPrintSumSCircle(ref listEnteredShapes));
            _comands.Add(new ComandPrintSumSRectangle(ref listEnteredShapes));
            _comands.Add(new ComandPrintSumSTriangle(ref listEnteredShapes));
            _comands.Add(new ComandPrintSumSPolygon(ref listEnteredShapes));
            _comands.Add(new ComandPrintSumPSquare(ref listEnteredShapes));
            _comands.Add(new ComandPrintSumPCircle(ref listEnteredShapes));
            _comands.Add(new ComandPrintSumPRectangle(ref listEnteredShapes));
            _comands.Add(new ComandPrintSumPTriangle(ref listEnteredShapes));
            _comands.Add(new ComandPrintSumPPolygon(ref listEnteredShapes));
            _comands.Add(new ComandExit());
        }

        /// <summary>
        /// метод для получения данных с файла о введенных фигурах
        /// </summary>
        /// <param name="path">путь к файлу</param>
        /// <returns>данные</returns>
        public void ReaderFile(string path)
        {
            using (StreamReader fs = new StreamReader(path))
            {
                ListFigure<Figure> list = new ListFigure<Figure>();
                string line;
                while ((line = fs.ReadLine()) != null)
                {
                    string[] words = line.Split(" ", 2);
                   CreateFigure(words[0], words[1]);
                }
            }

        }

        private void CreateFigure(string figur, string parametrs)
        {
                if (figur.ToLower() == _comands.OfType<ComandCircle>().FirstOrDefault().NameComand) listEnteredShapes.Add(new Circle(ToParametrs(parametrs)));
                if(figur.ToLower() == _comands.OfType<ComandPolygon>().FirstOrDefault().NameComand) listEnteredShapes.Add( new Polygon(ToParametrs(parametrs)));
                if (figur.ToLower() == _comands.OfType<ComandTringle>().FirstOrDefault().NameComand) listEnteredShapes.Add(new Triangle(ToParametrs(parametrs)));
                if (figur.ToLower() == _comands.OfType<ComandSquare>().FirstOrDefault().NameComand) listEnteredShapes.Add(new Square(ToParametrs(parametrs)));
                if(figur.ToLower() == _comands.OfType<ComandRectangle>().FirstOrDefault().NameComand) listEnteredShapes.Add( new Rectangle(ToParametrs(parametrs)));          
        }

        /// <summary>
        /// метод для записи данных в файл
        /// </summary>
        /// <param name="n">лист с которого читаются данные</param>
        public void WriteFile(ListFigure<Figure> n,string path)
        {
            using (StreamWriter fs = new StreamWriter(path))
            {
               foreach(var el in listEnteredShapes)
                {
                    fs.WriteLine(el.ToString());
                }
            }
        }
    }
}
