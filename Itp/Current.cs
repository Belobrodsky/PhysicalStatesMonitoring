﻿using System;

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
        //последнее значение времени зарегистрированного параметра
        //   private double _timeNow;
        //для расчета реактивностей необходимы следующие параметры
        //предыдущее значение времени
        private DateTime _time1Old = DateTime.MinValue;
    
        public DateTime Time1Old 

        {
            get
            {
                return _time1Old;
            }
            set
            {
                _time1Old = value;
            }
        }
        
        private DateTime _time2Old = DateTime.MinValue;
        public DateTime Time2Old
        {
            get
            {
                return _time2Old;
            }
            set
            {
                _time2Old = value;
            }
        }






        public double Ro; ////возвращает реактивность, рассчитанную в Беттах
        private double _tok1Old = double.NaN;
        private double _tok2Old = double.NaN;

        public double Tok1New { get; set; }
        public double Tok2New { get; set; }

        public double Reactivity1 { get; set; }
        public double Reactivity2 { get; set; }
        public DateTime timenow { get; private set; }

        #endregion

        public double SearchCurrent1(Ipt4 temp)
        {
            var step1 = Math.Pow(10, -temp.Power1);
            return Tok1New = temp.FCurrent1 / 25000.0 * step1;
        }
        public double SearchCurrent2(Ipt4 temp)
        {
            var step2 = Math.Pow(10, -temp.Power2);
            return Tok2New = temp.FCurrent2 / 25000.0 * step2;
        }


        //TODO: Александр. Старые значения времени и токов нужно хранить в полях класса. Извне брать только текущие значения.
        //эти методы должны расчитывать реактивности из Ток1 и Ток2
        public double SearchReactivity1(double[] l, double[] a, Buffer time, Ipt4 temp)
        {
            //так как предыдущего значения тока нет в самом начале регистрации, то приравиваем все 6 значений массива одному току
            //TODO:ВОЗМОЖНО ТУТ НУЖНО СТАВИТЬ УСЛОВИЕ, ЕСЛИ НАЧАЛО РЕГИСТРАЦИИ ТО НУЖНО ВЫПОЛНЯТЬ ЭТОТ ЦИКЛ 
            //TODO:ТАК КАК НЕТ ДВУХ ЗНАЧЕНИЙ ТОКОВ, А ЕСТЬ ТОЛЬКО ОДНО, А ЕСЛИ ЗАРЕГИСТРИРОВАЛОСЬ ВТОРОЕ ЗНАЧЕНИЕ 
            //TODO:ПРОПУСКАЕТСЯ ЭТОТ ЦИКЛ FOR
            Tok1New = SearchCurrent1(temp);
            
            //вспомогательная переменная для устранения косяка неодинаковости времени
            DateTime _timeNow;

            if (_time1Old.Equals(DateTime.MinValue) && _tok1Old.Equals(double.NaN))
            {
                for (int i = 0; i < 6; i++)
                {
                    _psi01[i] = Tok1New;
                }
                _time1Old = DateTime.Now;
                _tok1Old = Tok1New;

            }


            _timeNow = DateTime.Now;

            var dt =  _timeNow- _time1Old;

            for (int i = 0; i < _one.Length; i++)
            {
                double constTRaspada = l[i] * dt.TotalSeconds;
                _one[i] = Math.Exp(-constTRaspada);
                _two[i] = (1 - _one[i]) / constTRaspada;
                _psi01[i] = _psi01[i] * _one[i] - (Tok1New - _tok1Old) * _two[i] - _tok1Old * _one[i] + Tok1New;

                Reactivity1 += a[i] * _psi01[i];
            }

            //Зачем потребовалось создать _timeNow ?? Да просто иначе если бы в этой строке стояло бы DateTime.Now то это было бы уже другое время, нежели участвующее в формуле выше!!!
            _time1Old = _timeNow;
            _tok1Old = Tok1New;

            return Reactivity1 = 1 - Reactivity1 / Tok1New;
        }

        public double SearchReactivity2(double[] l, double[] a, Buffer time, Ipt4 temp)
        {
            //так как предыдущего значения тока нет в самом начале регистрации, то приравиваем все 6 значений массива одному току
            //TODO:ВОЗМОЖНО ТУТ НУЖНО СТАВИТЬ УСЛОВИЕ, ЕСЛИ НАЧАЛО РЕГИСТРАЦИИ ТО НУЖНО ВЫПОЛНЯТЬ ЭТОТ ЦИКЛ 
            //TODO:ТАК КАК НЕТ ДВУХ ЗНАЧЕНИЙ ТОКОВ, А ЕСТЬ ТОЛЬКО ОДНО, А ЕСЛИ ЗАРЕГИСТРИРОВАЛОСЬ ВТОРОЕ ЗНАЧЕНИЕ 
            //TODO:ПРОПУСКАЕТСЯ ЭТОТ ЦИКЛ FOR
            Tok2New = SearchCurrent2(temp);
            
            //вспомогательная переменная для устранения косяка неодинаковости времени
            DateTime _timeNow;


            if (_time2Old.Equals(DateTime.MinValue) && _tok2Old.Equals(double.NaN))
            {
                for (int i = 0; i < 6; i++)
                {
                    _psi02[i] = Tok2New;
                }
                _time2Old = DateTime.Now;
                _tok2Old = Tok2New;
            }

            _timeNow = DateTime.Now;

            var dt = _timeNow - _time2Old;


            for (int i = 0; i < _one.Length; i++)
            {
                double constTRaspada = l[i] * dt.TotalSeconds;
                _one[i] = Math.Exp(-constTRaspada);
                _two[i] = (1 - _one[i]) / constTRaspada;
                _psi02[i] = _psi02[i] * _one[i] - (Tok2New - _tok2Old) * _two[i] - _tok2Old * _one[i] + Tok2New;

                Reactivity2 += a[i] * _psi02[i];
            }
            _time2Old = _timeNow;
            _tok1Old = Tok2New;

            return Reactivity2 = 1 - Reactivity2 / Tok2New;
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