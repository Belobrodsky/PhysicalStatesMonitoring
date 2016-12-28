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
            public static double[] l_metodiki = { 0.0127, 0.0317, 0.1180, 0.3170, 1.4000, 3.9200 };
            //параметры запаздывающих нейтронов (относительные групповые доли - альфа)
            public static double[] aAPIK = { 0.0340, 0.2020, 0.1840, 0.4030, 0.1430, 0.0340 };
            //коэффициент перевода из процентов в бетта эффективность
            public static double Beff = 0.74;
        }

        public double Tok1 { get; set; }
        public double Tok2 { get; set; }

        public double Reactivity1 { get; set; }
        public double Reactivity2 { get; set; }

        public void SearchCurrent(Ipt4 temp)
        {
            double step1 = Math.Pow(10, -temp.Power1);
            double step2 = Math.Pow(10, -temp.Power2);
            Tok1 = (temp.FCurrent1 / 25000.0) * step1;
            Tok2 = (temp.FCurrent2 / 25000.0) * step2;
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
