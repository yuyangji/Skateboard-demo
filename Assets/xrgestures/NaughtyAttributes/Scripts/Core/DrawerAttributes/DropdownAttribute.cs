using System.Collections;
using System;
using System.Collections.Generic;

namespace NaughtyAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class DropdownAttribute : DrawerAttribute
    {
        public string ValuesName { get; private set; }

        public DropdownAttribute(string valuesName)
        {
            ValuesName = valuesName;
        }
    }

    public interface IDropdownList : IEnumerable<KeyValuePair<string, object>>
    {
    }

    public class TypeDropdownList<T> : DropdownList<T> 
    {
        private Dictionary<string, Type> _dictionary;

        public TypeDropdownList() : base()
        {
            _dictionary = new Dictionary<string, Type>();
        }
        public void Add(Type value)
        {
            _dictionary.Add(value.Name, value);
            _values.Add(new KeyValuePair<string, object>(value.Name, value.Name));
        }

        public Type GetValueType(string name)
        => _dictionary.TryGetValue(name, out var type) ? type : null;

        public void Clear()
        {
            _values.Clear();
            _dictionary.Clear();
        }


    }

    public class DropdownList<T> : IDropdownList
    {
        protected List<KeyValuePair<string, object>> _values;
      
        public DropdownList()
        {
            _values = new List<KeyValuePair<string, object>>();
        }

       
        public void Add(string displayName, T value)
        {
            
            _values.Add(new KeyValuePair<string, object>(displayName, value));
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static explicit operator DropdownList<object>(DropdownList<T> target)
        {
            DropdownList<object> result = new DropdownList<object>();
            foreach (var kvp in target)
            {
                result.Add(kvp.Key, kvp.Value);
            }

            return result;
        }



    }
}
