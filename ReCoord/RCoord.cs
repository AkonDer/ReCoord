using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReCoord
{
    class RCoord
    {
        double   dXtr, // Координата Х траектории
                 Xop, // Координата Х огневой позиции
                 Yop, // Координата Y огневой позиции
                 Xst, // Координата Х станции РЛС
                 Yst, // Координата Y станции РЛС
                 Xtr, // Координата Х траектории в топографических координатах
                 Ytr, // Координата Y траектории в топографических координатах
                 alf; // угол основного направления

        public RCoord(  double xtr, // Координата Х траектории
                        double xop, // Координата Х огневой позиции
                        double yop, // Координата Y огневой позиции
                        double xst, // Координата Х станции РЛС
                        double yst, // Координата Y станции 
                        double al) // угол основного направления
        {
            dXtr = xtr;
            Xop = xop;
            Yop = yop; // инициализация переменных
            Xst = xst;
            Yst = yst;
            alf = al;
        }

        // функция получения угла направления на нашу точку траектории (точка упреждения)
        public double getAlf()
        {
            Xtr = Xop + dXtr * Math.Cos(alf * Math.PI / 180); // получение координаты Х траектории в топографических координатах
            Ytr = Yop + dXtr * Math.Sin(alf * Math.PI / 180); // получение координаты Y траектории в топографических координатах

            double dx = Xtr - Xst; // разница координат станции и точки упреждения катеты
            double dy = Ytr - Yst; // разница координат станции и точки упреждения катеты
            double X = Math.Sqrt(dx * dx + dy * dy); // растояние от станции да точки упреждения высчетаное по формуле сумма квадратов катетов равна квадрату гепотенузы
            if (dy > 0)
            {
                return Math.Acos(dx / X) * 180 / Math.PI;
            }                                       // возвращаем наш угол
            else return 360 - Math.Acos(dx / X) * 180 / Math.PI;
        }

    }
}
