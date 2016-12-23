using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Itp
{
    class Current
    {
        class MyConst
        {
            //параметры запаздывающих нейтронов (постоянные распада - лямбда, взятые из методик физических испытаний)
            public static double[] l_metodiki = { 0.0127, 0.0317, 0.118, 0.317, 1.40, 3.92 };
            //параметры запаздывающих нейтронов (относительные групповые доли - альфа)
            public static double[] aAPIK =      { 0.034, 0.202, 0.184, 0.403, 0.143, 0.034 };
            //коэффициент перевода из процентов в бетта эффективность
            public static double Beff = 0.74;
        }

        public double Tok1 { get; set; }
        public double Tok2 { get; set; }

        public double Reactivity1 { get; set; }
        public double Reactivity2 { get; set; }

        public void SearchCurrent(Itp4 temp)
        {
            string st1 = temp.Power1.ToString();
            string st2 = temp.Power2.ToString();
            //st это степень то есть 1e-st это допустим 1e-5 степени
            string stlong1 ="1e-"+st1;
            string stlong2 ="1e-"+st2;
            double step1 = double.Parse(stlong1);
            double step2 = double.Parse(stlong2);
            Tok1 = (temp.FCurrent1/25000.0)*step1;
            Tok2 = (temp.FCurrent2/25000.0)*step2;
        }
        //для расчета реактивностей необходимы следующие параметры
        //предыдущее значение времени
        private double Time_Old;
        //последнее значение времени зарегистрированного параметра
        private double Time_Now;
        public double Ro; ////возвращает реактивность, рассчитанную в Беттах
        //разность времени последнего и предпоследнего значений времени в формате UNIX
        public double dT;

        //6 групп запаздывающих нейтронов
        //два массива для упрощения вычислений реактивности в дальнейшем
        double[] one = new double[6];
        double[] two = new double[6];

        //для поиска реактивности по формуле обращенного уравнения кинетики
        //это массив первых 6-ти значений, они все равны первому вычисленному току
        //так как нет предыдущего времени, а есть только первое значение
        double[] psi0 = new double[6];
      //  List<double> MyPsi = new List<double>();


       //эти методы должны расчитывать реактивности из Ток1 и Ток2
        //так как предыдущего значения тока нет , то приравиваем все 6 значений массива одному току
        public void PSI0(double J_Old)
        {
            for (int i = 0; i < 6; i++)
            {
                psi0[i] = J_Old;
            }
        }
        //метод когда уже более 1-го значения тока для расчета реактивности на каждом шаге
        public double SearchReactivity(double Time_Now, double Time_Old, double J_Now, double J_Old, double[] l, double[] a)
        {
            double Reactivity = 0;

            dT = Time_Now - Time_Old;

            for (int i = 0; i < one.Length; i++)
            {
                double Const_t_raspada = l[i] * dT;
                one[i] = Math.Exp(-Const_t_raspada);
                two[i] = (1 - one[i]) / Const_t_raspada;
                psi0[i] = psi0[i] * one[i] - (J_Now - J_Old) * (two[i]) - J_Old * one[i] + J_Now;
                double yt = a[i] * psi0[i];
                Reactivity = Reactivity + yt;
            }
            Reactivity = 1 - Reactivity / J_Now;
            return Reactivity;
        }  
    }
}
