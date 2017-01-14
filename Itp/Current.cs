using System;

namespace Ipt
{
    public class Current
    {
        #region Свойства

        //6 групп запаздывающих нейтронов
        //два массива для упрощения вычислений реактивности в дальнейшем
        double[] _one = new double[6];
        double[] _two = new double[6];

        //для поиска реактивности по формуле обращенного уравнения кинетики
        //это массив первых 6-ти значений, они все равны первому вычисленному току
        //так как нет предыдущего времени, а есть только первое значение
        double[] _psi01 = new double[6];
        double[] _psi02 = new double[6];
    
        public double Ro; ////возвращает реактивность, рассчитанную в Беттах
        private double _tok1Old = double.NaN;
        private double _tok2Old = double.NaN;

        public Current()
        {
            TimeOld= DateTime.MinValue;
        }

        public double Tok1New;
        public double Tok2New;

        public double Reactivity1;
        public double Reactivity2;
        public double ReactivityAverage;

        public DateTime TimeNow;
        public DateTime TimeOld;

        #endregion

        private void SearchCurrent(Ipt4 temp)
        {
            var step1 = Math.Pow(10, -temp.Power1);
            Tok1New = temp.FCurrent1 / 25000.0 * step1;
            var step2 = Math.Pow(10, -temp.Power2);
            Tok2New = temp.FCurrent2 / 25000.0 * step2;
        }

        //TODO: Александр. Старые значения времени и токов нужно хранить в полях класса. Извне брать только текущие значения.
        //эти методы должны расчитывать реактивности из Ток1 и Ток2
        public void SearchReactivity(double[] l, double[] a, Buffer time, Ipt4 temp)
        {
            //TODO:ВОЗМОЖНО ТУТ НУЖНО СТАВИТЬ УСЛОВИЕ, ЕСЛИ НАЧАЛО РЕГИСТРАЦИИ ТО НУЖНО ВЫПОЛНЯТЬ ЭТОТ ЦИКЛ 
            //TODO:ТАК КАК НЕТ ДВУХ ЗНАЧЕНИЙ ТОКОВ, А ЕСТЬ ТОЛЬКО ОДНО, А ЕСЛИ ЗАРЕГИСТРИРОВАЛОСЬ ВТОРОЕ ЗНАЧЕНИЕ 
            //TODO:ПРОПУСКАЕТСЯ ЭТОТ ЦИКЛ FOR
             SearchCurrent(temp);

             if (TimeOld.Equals(DateTime.MinValue) && _tok1Old.Equals(double.NaN) && _tok2Old.Equals(double.NaN))
            {
                for (int i = 0; i < 6; i++)
                {
                    _psi01[i] = Tok1New;
                    _psi02[i] = Tok2New;
                }
                TimeOld = DateTime.Now;
                _tok1Old = Tok1New;
                _tok2Old = Tok2New;
            }

            var timeNow = DateTime.Now;
            var dt = timeNow - TimeOld;

            for (int i = 0; i < _one.Length; i++)
            {
                double constTRaspada = l[i] * dt.TotalSeconds;
                _one[i] = Math.Exp(-constTRaspada);
                _two[i] = (1 - _one[i]) / constTRaspada;
                _psi01[i] = _psi01[i] * _one[i] - (Tok1New - _tok1Old) * _two[i] - _tok1Old * _one[i] + Tok1New;
                _psi02[i] = _psi02[i] * _one[i] - (Tok2New - _tok2Old) * _two[i] - _tok2Old * _one[i] + Tok2New;
                Reactivity1 += a[i] * _psi01[i];
                Reactivity2 += a[i] * _psi02[i];
            }
    
            //Зачем потребовалось создать _timeNow ?? Да просто иначе если бы в этой строке стояло бы DateTime.Now то это было бы уже другое время, нежели участвующее в формуле выше!!!
            TimeOld = timeNow;
            _tok1Old = Tok1New;
            _tok2Old = Tok2New;

            Reactivity1 = 1 - Reactivity1 / Tok1New;
            Reactivity2 = 1 - Reactivity2 / Tok2New;
            ReactivityAverage = (Reactivity1 + Reactivity2)/2;
        }

        class MyConst
        {
            #region Свойства

            //параметры запаздывающих нейтронов (постоянные распада - лямбда, взятые из методик физических испытаний)
            public static double[] LMetodiki = { 0.0127, 0.0317, 0.1180, 0.3170, 1.4000, 3.9200 };
            //параметры запаздывающих нейтронов (относительные групповые доли - альфа)
            public static double[] AApik = { 0.0340, 0.2020, 0.1840, 0.4030, 0.1430, 0.0340 };
            //коэффициент перевода из процентов в бетта эффективность
            private static double _beff = 0.74;

            #endregion
        }
    }
}