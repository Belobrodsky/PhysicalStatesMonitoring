using System;

namespace Ipt
{
    class Current
    {
        #region Свойства

        //6 групп запаздывающих нейтронов
        //два массива для упрощения вычислений реактивности в дальнейшем
        double[] _one = new double[6];

        //для поиска реактивности по формуле обращенного уравнения кинетики
        //это массив первых 6-ти значений, они все равны первому вычисленному току
        //так как нет предыдущего времени, а есть только первое значение
        double[] _psi01 = new double[6];
        double[] _psi02 = new double[6];
        //последнее значение времени зарегистрированного параметра
        private double _timeNow;
        //для расчета реактивностей необходимы следующие параметры
        //предыдущее значение времени
        private double _timeOld;
        double[] _two = new double[6];
        //разность времени последнего и предпоследнего значений времени в формате UNIX
        public double dT;
        public double Ro; ////возвращает реактивность, рассчитанную в Беттах

        public double Tok1 { get; set; }
        public double Tok2 { get; set; }

        public double Reactivity1 { get; private set; }
        public double Reactivity2 { get; private set; }

        #endregion

        public void SearchCurrent(Ipt4 temp)
        {
            var step1 = Math.Pow(10, -temp.Power1);
            var step2 = Math.Pow(10, -temp.Power2);
            Tok1 = temp.FCurrent1 / 25000.0 * step1;
            Tok2 = temp.FCurrent2 / 25000.0 * step2;
        }

        //  List<double> MyPsi = new List<double>();

        //TODO: Александр. Старые значения времени и токов нужно хранить в полях класса. Извне брать только текущие значения.
        //эти методы должны расчитывать реактивности из Ток1 и Ток2
        public void SearchReactivity(double timeNow, double timeOld, double j1Now, double j1Old, double j2Now,
                                     double j2Old, double[] l, double[] a)
        {
            //так как предыдущего значения тока нет в самом начале регистрации, то приравиваем все 6 значений массива одному току
            //TODO:ВОЗМОЖНО ТУТ НУЖНО СТАВИТЬ УСЛОВИЕ, ЕСЛИ НАЧАЛО РЕГИСТРАЦИИ ТО НУЖНО ВЫПОЛНЯТЬ ЭТОТ ЦИКЛ 
            //TODO:ТАК КАК НЕТ ДВУХ ЗНАЧЕНИЙ ТОКОВ, А ЕСТЬ ТОЛЬКО ОДНО, А ЕСЛИ ЗАРЕГИСТРИРОВАЛОСЬ ВТОРОЕ ЗНАЧЕНИЕ 
            //TODO:ПРОПУСКАЕТСЯ ЭТОТ ЦИКЛ FOR
            for (int i = 0; i < 6; i++)
            {
                _psi01[i] = j1Old;
                _psi02[i] = j2Old;
            }


            Reactivity1 = 0;
            Reactivity2 = 0;

            dT = timeNow - timeOld;

            for (int i = 0; i < _one.Length; i++)
            {
                double constTRaspada = l[i] * dT;
                _one[i] = Math.Exp(-constTRaspada);
                _two[i] = (1 - _one[i]) / constTRaspada;
                _psi01[i] = _psi01[i] * _one[i] - (j1Now - j1Old) * _two[i] - j1Old * _one[i] + j1Now;

                Reactivity1 += a[i] * _psi01[i];
                Reactivity2 += a[i] * _psi02[i];
            }
            Reactivity1 = 1 - Reactivity1 / j1Now;
            Reactivity2 = 1 - Reactivity2 / j2Now;
        }

        class MyConst
        {
            #region Свойства

            //параметры запаздывающих нейтронов (постоянные распада - лямбда, взятые из методик физических испытаний)
            public static double[] LMetodiki = {0.0127, 0.0317, 0.1180, 0.3170, 1.4000, 3.9200};
            //параметры запаздывающих нейтронов (относительные групповые доли - альфа)
            public static double[] AApik = {0.0340, 0.2020, 0.1840, 0.4030, 0.1430, 0.0340};
            //коэффициент перевода из процентов в бетта эффективность
            private static double _beff = 0.74;

            #endregion
        }
    }
}