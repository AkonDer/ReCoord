using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReCoord
{
    public class RCoord
    {
        double dXtr; // Координата Х траектории
        double Xop; // Координата Х огневой позиции
        double Yop; // Координата Y огневой позиции
        double Xst; // Координата Х станции РЛС
        double Yst; // Координата Y станции РЛС
        double Xtr; // Координата Х траектории в топографических координатах
        double Ytr; // Координата Y траектории в топографических координатах
        double alf; // угол основного направления

        public RCoord(  double xtr, // Координата Х траектории
                        double xop, // Координата Х огневой позиции
                        double yop, // Координата Y огневой позиции
                        double xst, // Координата Х станции РЛС
                        double yst, // Координата Y станции 
                        double alf) // угол основного направления
        {
            dXtr = xtr;
            Xop = xop;
            Yop = yop; // инициализация переменных
            Xst = xst;
            Yst = yst;
        }

        // функция получения координаты Х траектории в топографических координатах
        double getXtr()
        {
            return Xop + dXtr*Math.Cos(alf*Math.PI/180);
        }
        
        // функция получения координаты Y траектории в топографических координатах
        int getYtr()
        {
            return 0;
        }
        
    }
}
