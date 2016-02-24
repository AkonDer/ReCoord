using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReCoord
{
    /// <summary>
    /// Этот класс содержит методы и свойства предназначенные для расчета точек упреждения РЛС
    /// </summary>
    class RCoord
    {
        // приватные поля
        private double dXtr, // Координата Х траектории
                        dYtr, // Координата Y траектории (высота)
                        Xop, // Координата Х огневой позиции
                        Yop, // Координата Y огневой позиции
                        Hop, // высота огневой позиции
                        Xst, // Координата Х станции РЛС
                        Yst, // Координата Y станции РЛС
                        Hst, // высота станции
                        Xtr, // Координата Х траектории в топографических координатах
                        Ytr, // Координата Y траектории в топографических координатах
                        alf; // угол основного направления

        /// <summary>
        /// Конструктор класса в котором происходит инициализация переменных
        /// </summary>
        /// <param name="xtr">Координата Х траектории</param>
        /// <param name="ytr">Координата Y траектории (высота)</param>
        /// <param name="xop">Координата Х огневой позиции</param>
        /// <param name="yop">Координата Y огневой позиции</param>
        /// <param name="hop">Высота огневой позиции</param>
        /// <param name="xst">Координата Х станции РЛС</param>
        /// <param name="yst">Координата Y станции</param>
        /// <param name="hst">Высота станции</param>
        /// <param name="al">Угол основного направления</param>
        public RCoord(double xtr,
                      double ytr,
                      double xop,
                      double yop,
                      double hop,
                      double xst,
                      double yst,
                      double hst,
                      double al)
        {
            dXtr = xtr;
            dYtr = ytr;
            Xop = xop;
            Yop = yop; // инициализация переменных
            Hop = hop;
            Xst = xst;
            Yst = yst;
            Hst = hst;
            alf = al;
        }

        /// <summary>
        /// Возвращает угол от станции на точку упреждения
        /// </summary>
        public double GetAlf
        {
            get
            {
                Xtr = Xop + dXtr * Math.Cos(alf * Math.PI / 180); // получение координаты Х траектории в топографических координатах
                Ytr = Yop + dXtr * Math.Sin(alf * Math.PI / 180); // получение координаты Y траектории в топографических координатах

                double dx = Xtr - Xst; // разница координат станции и точки упреждения - катет
                double dy = Ytr - Yst; // разница координат станции и точки упреждения - катет
                double X = Math.Sqrt(dx * dx + dy * dy); // растояние от станции да точки упреждения высчетаное по формуле сумма квадратов катетов равна квадрату гепотенузы
                if (dy > 0)
                {
                    return Math.Acos(dx / X) * 180 / Math.PI;
                }                                       // возвращаем наш угол
                else return 360 - Math.Acos(dx / X) * 180 / Math.PI;
            }
        }

        /// <summary>
        /// Возвращает угол места точки упреждения
        /// </summary>
        public double GetUMC
        {
            get
            {
                double H = Hop - Hst + dYtr; // высота точки упреждения от станции
                return Math.Asin(H / getDnakl) * 180 / Math.PI;
            }
        }

        /// <summary>
        /// Возвращает наклонную дальность
        /// </summary>
        public double getDnakl
        {
            get
            {
                double H = Hop - Hst + dYtr; // высота точки упреждения от станции
                double dx = Xtr - Xst; // разница координат станции и точки упреждения катеты
                double dy = Ytr - Yst; // разница координат станции и точки упреждения катеты
                double X = Math.Sqrt(dx * dx + dy * dy); // растояние от станции да точки упреждения высчетаное по формуле сумма квадратов катетов равна квадрату гипотенузы
                return Math.Sqrt(H * H + X * X); // наклонная дальность высчетаная по формуле сумма квадратов катетов равна квадрату гипотенузы
            }
        }

        /// <summary>
        /// Метод берет угол в виде десятичной дроби, отсекает дробную часть и возвращает только градусы
        /// </summary>
        /// <param name="a">Угол в градусах</param>
        /// <returns>Возвращает градусы в виде строки</returns>
        public string ToGrad(double a)
        {
            return Math.Truncate((decimal)(a)).ToString();
        }

        /// <summary>
        /// Метод берет угол в виде десятичной дроби, отсекает все ненужное и возвращает только минуты
        /// </summary>
        /// <param name="a">Угол в градусах</param>
        /// <returns>Возвращает минуты в виде строки</returns>
        public string ToMin(double a)
        {
            double x = (double)Math.Truncate((decimal)(a)); // получаем целую часть
            x = a - x; // получаем дробную часть
            return Math.Truncate((decimal)(x * 60)).ToString();
        }

        /// <summary>
        /// Метод берет угол в виде десятичной дроби, отсекает все ненужное и возвращает только секунды
        /// </summary>
        /// <param name="a">Угол в градусах</param>
        /// <returns>Возвращает минуты в виде строки</returns>
        public string ToSec(double a)
        {
            double x = (double)Math.Truncate((decimal)(a * 100)); // получаем целую часть предварительно умножив на 100
            x = Math.Round(a * 100 - x, 2); // получаем дробную часть и округляем до сотых
            return Math.Truncate((decimal)(x * 60)).ToString();
        }

        /// <summary>
        /// Метод берет угол в градусах и возвращает в делениях уголомера
        /// </summary>
        /// <param name="a">Угол в градусах</param>
        /// <returns>угол в делениях угломера в виде строки</returns>
        public string ToDU(double a)
        {
            string b = Math.Round(a * 6000 / 360).ToString();
            string c = b;
            switch (b.Length)
            {
                case 1:
                    c = b.Insert(0, "000");
                    break;
                case 2:
                    c = b.Insert(0, "00");
                    break;
                case 3:
                    c = b.Insert(0, "0");
                    break;
            }
            return c.Insert(c.Length - 2, "-");
        }
    }
}
