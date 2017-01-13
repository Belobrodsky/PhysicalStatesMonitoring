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
            _timeOld= DateTime.MinValue;
        }

        public double _tok1New;
        public double _tok2New;

        public double _reactivity1;
        public double _reactivity2;

        public DateTime _timeNow;
        public DateTime _timeOld;

        #endregion

        private void SearchCurrent(Ipt4 temp)
        {
            var step1 = Math.Pow(10, -temp.Power1);
            _tok1New = temp.FCurrent1 / 25000.0 * step1;
            var step2 = Math.Pow(10, -temp.Power2);
            _tok2New = temp.FCurrent2 / 25000.0 * step2;
        }

        //TODO: Александр. Старые значения времени и токов нужно хранить в полях класса. Извне брать только текущие значения.
        //эти методы должны расчитывать реактивности из Ток1 и Ток2
        public void SearchReactivity(double[] l, double[] a, Buffer time, Ipt4 temp)
        {
            //TODO:ВОЗМОЖНО ТУТ НУЖНО СТАВИТЬ УСЛОВИЕ, ЕСЛИ НАЧАЛО РЕГИСТРАЦИИ ТО НУЖНО ВЫПОЛНЯТЬ ЭТОТ ЦИКЛ 
            //TODO:ТАК КАК НЕТ ДВУХ ЗНАЧЕНИЙ ТОКОВ, А ЕСТЬ ТОЛЬКО ОДНО, А ЕСЛИ ЗАРЕГИСТРИРОВАЛОСЬ ВТОРОЕ ЗНАЧЕНИЕ 
            //TODO:ПРОПУСКАЕТСЯ ЭТОТ ЦИКЛ FOR
             SearchCurrent(temp);

             if (_timeOld.Equals(DateTime.MinValue) && _tok1Old.Equals(double.NaN) && _tok2Old.Equals(double.NaN))
            {
                for (int i = 0; i < 6; i++)
                {
                    _psi01[i] = _tok1New;
                    _psi02[i] = _tok2New;
                }
                _timeOld = DateTime.Now;
                _tok1Old = _tok1New;
                _tok2Old = _tok2New;
            }

            var timeNow = DateTime.Now;
            var dt = timeNow - _timeOld;

            for (int i = 0; i < _one.Length; i++)
            {
                double constTRaspada = l[i] * dt.TotalSeconds;
                _one[i] = Math.Exp(-constTRaspada);
                _two[i] = (1 - _one[i]) / constTRaspada;
                _psi01[i] = _psi01[i] * _one[i] - (_tok1New - _tok1Old) * _two[i] - _tok1Old * _one[i] + _tok1New;
                _psi02[i] = _psi02[i] * _one[i] - (_tok2New - _tok2Old) * _two[i] - _tok2Old * _one[i] + _tok2New;
                _reactivity1 += a[i] * _psi01[i];
                _reactivity2 += a[i] * _psi02[i];
            }
    
            //Зачем потребовалось создать _timeNow ?? Да просто иначе если бы в этой строке стояло бы DateTime.Now то это было бы уже другое время, нежели участвующее в формуле выше!!!
            _timeOld = timeNow;
            _tok1Old = _tok1New;
            _tok2Old = _tok2New;

            _reactivity1 = 1 - _reactivity1 / _tok1New;
            _reactivity2 = 1 - _reactivity2 / _tok2New;
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