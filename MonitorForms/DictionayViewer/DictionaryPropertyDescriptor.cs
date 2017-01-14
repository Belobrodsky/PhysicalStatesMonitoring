using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MonitorForms
{
    /// <summary>
    /// Класс для описания значений словаря как свойств.
    /// </summary>
    /// <typeparam name="TKey">Тип данных словарного ключа.</typeparam>
    /// <typeparam name="TValue">Тип данных словарного значения.</typeparam>
    internal class DictionaryPropertyDescriptor<TKey, TValue> : PropertyDescriptor

    {
        private readonly TKey _key;
        private readonly IDictionary<TKey, TValue> _dictionary;

        public DictionaryPropertyDescriptor(IDictionary<TKey, TValue> dictionary, TKey key)
            : base(((TKey)key).ToString(), null)
        {
            _dictionary = dictionary;
            _key = key;
        }

        #region Overrides of PropertyDescriptor

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override object GetValue(object component)
        {
            return _dictionary[(TKey) _key];
        }

        public override void ResetValue(object component)
        {
            
        }

        public override void SetValue(object component, object value)
        {
            _dictionary[(TKey)_key] = (TValue)value;
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get
            {
                return null;
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public override Type PropertyType
        {
            get
            {
                return _dictionary[(TKey)_key].GetType();
            }
        }

        public override TypeConverter Converter
        {
            get
            {
                return ScudIptConverter.GetInstance();
            }
        }
        #endregion
    }
}