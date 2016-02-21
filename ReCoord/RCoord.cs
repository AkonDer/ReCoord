using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReCoord
{
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

        // конструктор в котором происходит инициализация переменных
        public RCoord(double xtr, // Координата Х траектории
                      double ytr, // Координата Y траектории (высота)
                      double xop, // Координата Х огневой позиции
                      double yop, // Координата Y огневой позиции
                      double hop, // высота огневой позиции
                      double xst, // Координата Х станции РЛС
                      double yst, // Координата Y станции 
                      double hst, // высота станции
                      double al)  // угол основного направления
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

        // свойство - угол направления на нашу точку траектории (точка упреждения)
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

        // свойство - угол места цели
        public double GetUMC
        {
            get
            {
                double H = Hop - Hst + dYtr; // высота точки упреждения от станции
                return Math.Asin(H / getDnakl) * 180 / Math.PI;
            }
        }

        // Свойство - наклонная дальность
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

        //функция возвращает градусы
        public string ToGrad(double a)
        {
            return Math.Truncate((decimal)(a)).ToString();
        }

        //функция возвращает минуты
        public string ToMin(double a)
        {
            double x = (double)Math.Truncate((decimal)(a)); // получаем целую часть
            x = a - x; // получаем дробную часть
            return Math.Truncate((decimal)(x * 60)).ToString();
        }

        // функция возыращает секунды
        public string ToSec(double a)
        {
            double x = (double)Math.Truncate((decimal)(a * 100)); // получаем целую часть предварительно умножив на 100
            x = Math.Round(a * 100 - x, 2); // получаем дробную часть и округляем до сотых
            return Math.Truncate((decimal)(x * 60)).ToString();
        }
    }
}
