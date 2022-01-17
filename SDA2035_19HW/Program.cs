using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA2035_19HW
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ComputerModel computer1 = new ComputerModel("2100", "Джуниор", "ЦП01", 2.1, 2, 256, 2, 500, 32);
                ComputerModel computer2 = new ComputerModel("2100Х", "Джуниор", "ЦП01", 2.1, 4, 512, 2, 700, 36);
                ComputerModel computer3 = new ComputerModel("2400", "ХоумПро", "ЦП02", 2.4, 4, 1024, 4, 900, 25);
                ComputerModel computer4 = new ComputerModel("2800", "Сениор", "ЦП03", 2.8, 8, 1024, 4, 12500, 10);
                ComputerModel computer5 = new ComputerModel("2800Х", "Сениор", "ЦП03", 2.8, 16, 2048, 6, 15500, 8);
                ComputerModel computer6 = new ComputerModel("3800Х", "Геймер", "ЦП05", 3.8, 32, 2048, 8, 25000, 5);
                ComputerModel computer7 = new ComputerModel("3500", "Эксперт", "ЦП04", 3.5, 32, 1024, 4, 11000, 10);
                List<ComputerModel> listComputers = new List<ComputerModel>() { computer1,computer2,computer3,computer4,computer5,computer6,
                                                                                computer7};

                // 1 - Поиск по типу процессора
                Console.WriteLine("Введите желаемый тип процессора (от \"ЦП01\" до \"ЦП05\"");
                string cpuTypeInput = Console.ReadLine();

                IEnumerable<ComputerModel> listCPU = listComputers
                    .Where(p => p.CPUtype == cpuTypeInput);

                foreach (ComputerModel p in listCPU)
                {
                    Console.WriteLine($"{p.PCmarkcode}\t{p.PCmarkname}\t{p.PCprice} у.е.");
                }
                Console.WriteLine("");

                // 2 - Поиск по минимальному ОЗУ
                Console.WriteLine("Введите минимальное значение ОЗУ (в диапазоне от 2 до 32 ГБ)");
                int ramMinInput = Convert.ToInt32(Console.ReadLine());

                IEnumerable<ComputerModel> listRAM = listComputers
                    .Where(r => r.RAMvolume >= ramMinInput);

                foreach (ComputerModel r in listRAM)
                {
                    Console.WriteLine($"{r.PCmarkcode}\t{r.PCmarkname}\t{r.RAMvolume} Гб\t{r.PCprice} у.е.");
                }
                Console.WriteLine("");

                // 3 - Упорядочить по стоимости (от мин. к макс.)
                Console.WriteLine("Список, сортированный по возрастанию цены:");

                IEnumerable<ComputerModel> ascendingPriceList = listComputers
                    .OrderBy(c => c.PCprice);

                foreach (ComputerModel a in ascendingPriceList)
                {
                    Console.WriteLine($"{a.PCmarkcode}\t{a.PCmarkname}\t{a.PCprice} у.е.");
                }
                Console.WriteLine("");

                // 4 - Группировка по типу процессора
                Console.WriteLine("Список, сгруппированный по типу процессора:");

                var cpuGroupList = listComputers
                    .GroupBy(pr => pr.CPUtype);

                foreach (IGrouping<string, ComputerModel> pr in cpuGroupList)
                {
                    Console.WriteLine(pr.Key);
                    foreach (var el in pr)
                    {
                        Console.WriteLine($"\t{el.PCmarkcode}\t{el.PCmarkname}\t{el.CPUspeed}\t{el.PCprice}");
                    }
                }
                Console.WriteLine("");

                // 5 - Самый дорогой и самый бюджетный ПК

                double maxPrice = listComputers.Max(mx => mx.PCprice);
                Console.WriteLine("Стоимость самого дорого компьютера составляет {0:f2}", maxPrice);
                IEnumerable<ComputerModel> maxPriceList = listComputers
                   .Where(max => max.PCprice == maxPrice);

                foreach (ComputerModel max in maxPriceList)
                {
                    Console.WriteLine($"{max.PCmarkcode}\t{max.PCmarkname}");
                }
                Console.WriteLine("");


                double minPrice = listComputers.Min(mn => mn.PCprice);
                Console.WriteLine("Стоимость самого бюджетного компьютера составляет {0:f2}", minPrice);
                IEnumerable<ComputerModel> minPriceList = listComputers
                   .Where(min => min.PCprice == minPrice);

                foreach (ComputerModel min in minPriceList)
                {
                    Console.WriteLine($"{min.PCmarkcode}\t{min.PCmarkname}");
                }
                Console.WriteLine("");

                // 6 - Найти модель ПК в изобилии (не менее 30 шт в наличии)

                int moreThan30 = 0;
                Console.WriteLine("Есть ли хоть один компьютер в количестве больше 30?");

                IEnumerable<ComputerModel> quantityList = listComputers
                  .Where(q => q.PCcount > 30);
                moreThan30 = quantityList.Count();

                if (moreThan30 > 0)
                {
                    Console.WriteLine("Модели ПК в кол-ве более 30 - обнаружено:");
                    foreach (ComputerModel q in quantityList)
                    {
                        Console.WriteLine($"{q.PCmarkcode}\t{q.PCmarkname}\t{q.PCcount}"); ;
                    }
                }
                else
                {
                    Console.WriteLine("Модели ПК в кол-ве более 30 - не обнаружено");
                }

            }
            catch (Exception)
            {

                throw;
            }

            Console.ReadKey();
        }
    }
    class ComputerModel
    {
        #region Поля и свойства
        public string PCmarkcode { get; set; }
        public string PCmarkname { get; set; }
        public string CPUtype { get; set; }

        private double _CPUspeed;
        private int _RAMvolume;
        private int _HDDvolume;
        private int _VRAMvolume;
        private double _PCprice;
        private int _PCcount;

        public double CPUspeed
        {
            set
            {
                if (value > 0)
                {
                    _CPUspeed = value;
                }
                else
                {
                    Console.WriteLine("Частота ЦП должна быть больше нуля");
                    throw new ArgumentOutOfRangeException();
                }
            }
            get
            {
                return _CPUspeed;
            }
        }
        public int RAMvolume
        {
            set
            {
                if (value > 0)
                {
                    _RAMvolume = value;
                }
                else
                {
                    Console.WriteLine("Объем ОЗУ должен быть больше нуля");
                    throw new ArgumentOutOfRangeException();
                }
            }
            get
            {
                return _RAMvolume;
            }
        }
        public int HDDvolume
        {
            set
            {
                if (value > 0)
                {
                    _HDDvolume = value;
                }
                else
                {
                    Console.WriteLine("Объем ЖД должен быть больше нуля");
                    throw new ArgumentOutOfRangeException();
                }
            }
            get
            {
                return _HDDvolume;
            }
        }
        public int VRAMvolume
        {
            set
            {
                if (value > 0)
                {
                    _VRAMvolume = value;
                }
                else
                {
                    Console.WriteLine("Память видеокарты должна быть больше нуля");
                    throw new ArgumentOutOfRangeException();
                }
            }
            get
            {
                return _VRAMvolume;
            }
        }
        public double PCprice
        {
            set
            {
                if (value > 0)
                {
                    _PCprice = value;
                }
                else
                {
                    Console.WriteLine("Стоимость должна быть больше нуля");
                    throw new ArgumentOutOfRangeException();
                }
            }
            get
            {
                return _PCprice;
            }
        }
        public int PCcount
        {
            set
            {
                if (value >= 0)
                {
                    _PCcount = value;
                }
                else
                {
                    Console.WriteLine("Количество компьютеров д.б. не меньше нуля");
                    throw new ArgumentOutOfRangeException();
                }
            }
            get
            {
                return _PCcount;
            }
        }
        #endregion
        public ComputerModel(string markCode, string markName, string cpuType, double cpuSpeed, int RAM, int HDD, int VRAM, double pcPrice, int pcCount)
        {
            PCmarkcode = markCode;
            PCmarkname = markName;
            CPUtype = cpuType;
            CPUspeed = cpuSpeed;
            RAMvolume = RAM;
            HDDvolume = HDD;
            VRAMvolume = VRAM;
            PCprice = pcPrice;
            PCcount = pcCount;
        }
    }
}
