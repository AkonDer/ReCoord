﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReCoord
{
    public class RCoord
    {
        int Xtr; // Координата Х траектории
        int Xop; // Координата Х огневой позиции
        int Yop; // Координата Y огневой позиции
        int Xst; // Координата Х станции РЛС
        int Yst; // Координата Y станции РЛС

        public RCoord(  int xtr, // Координата Х траектории
                        int xop, // Координата Х огневой позиции
                        int yop, // Координата Y огневой позиции
                        int xst, // Координата Х станции РЛС
                        int yst) // Координата Y станции 
        {
            Xtr = xtr;
            Xop = xop;
            Yop = yop; // инициализация переменных
            Xst = xst;
            Yst = yst;
        }

    }
}
