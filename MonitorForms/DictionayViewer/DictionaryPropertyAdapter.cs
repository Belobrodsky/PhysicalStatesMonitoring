using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace MonitorForms
{
    /// <summary>
    /// Класс-адаптер для отображения словарей в <see cref="PropertyGrid"/>
    /// </summary>
    /// <typeparam name="TKey">Тип данных словарного ключа.</typeparam>
    /// <typeparam name="TValue">Тип данных словарного значения.</typeparam>
    class DictionaryPropertyAdapter<TKey, TValue> : ICustomTypeDescriptor
    {
        private readonly IDictionary<TKey, TValue> _dictionary;

        public void Add(TKey key, TValue value)
        {
            if (_dictionary.ContainsKey(key))
            {
                return;
            }
            _dictionary.Add(key, value);
        }

        public TValue this[TKey key]
        {
            get
            {
                return _dictionary[key];
            }
            set
            {
                if (!_dictionary.ContainsKey(key))
                {
                    return;
                }
                _dictionary[key] = value;
            }
        }

        public DictionaryPropertyAdapter(IDictionary<TKey, TValue> dictionary)
        {
            _dictionary = dictionary;
        }

        public DictionaryPropertyAdapter()
        {
            _dictionary = new ConcurrentDictionary<TKey, TValue>();
        }

        #region Implementation of ICustomTypeDescriptor

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return null;
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return ((ICustomTypeDescriptor)this).GetProperties(new Attribute[0]);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            var props = new ArrayList();
            foreach (KeyValuePair<TKey, TValue> entry in _dictionary)
            {
                props.Add(new DictionaryPropertyDescriptor<TKey, TValue>(_dictionary, entry.Key));
            }
            var pd = (PropertyDescriptor[])props.ToArray(typeof(PropertyDescriptor));
            return new PropertyDescriptorCollection(pd);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return _dictionary;
        }

        #endregion

        public void Clear()
        {
            _dictionary.Clear();
        }
    }
}
