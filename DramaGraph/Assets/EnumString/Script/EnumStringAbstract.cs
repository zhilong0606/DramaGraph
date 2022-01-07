using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnumString
{
    [Serializable]
    public abstract class EnumStringAbstract<T> : EnumStringAbstract
        where T : struct, Enum
    {
        public T value;

        [SerializeField]
        private string m_enumName;

        public EnumStringAbstract(T value)
        {
            this.value = value;
        }

        public abstract bool CheckValueEquals(T other);

        public override void OnBeforeSerialize()
        {
            m_enumName = value.ToString();
        }

        public override void OnAfterDeserialize()
        {
            T enumValue;
            if (Enum.TryParse(m_enumName, out enumValue))
            {
                value = enumValue;
            }
        }

        public static bool operator ==(EnumStringAbstract<T> p1, EnumStringAbstract<T> p2)
        {
            return p1.Equals(p2);
        }


        public static bool operator !=(EnumStringAbstract<T> p1, EnumStringAbstract<T> p2)
        {
            return !p1.Equals(p2);
        }

        public static bool operator ==(EnumStringAbstract<T> p1, T p2)
        {
            return p1.CheckValueEquals(p2);
        }


        public static bool operator !=(EnumStringAbstract<T> p1, T p2)
        {
            return !p1.CheckValueEquals(p2);
        }

        public static implicit operator T(EnumStringAbstract<T> enumString)
        {
            return enumString.value;
        }

        public override bool Equals(object obj)
        {
            EnumStringAbstract<T> enumString = obj as EnumStringAbstract<T>;
            if(enumString != null)
            {
                return CheckValueEquals(obj as EnumStringAbstract<T>);
            }
            return base.Equals(obj);
        }

        protected bool Equals(EnumStringAbstract<T> other)
        {
            return CheckValueEquals(other.value);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return value.GetHashCode() * 397;
            }
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }

    public abstract class EnumStringAbstract : ISerializationCallbackReceiver
    {
        public abstract void OnBeforeSerialize();
        public abstract void OnAfterDeserialize();
    }
}