using System;
using System.Diagnostics;

namespace GeneticAlgorithm.Models
{

    [DebuggerDisplay("{Value}")]
    [Serializable]
    public struct Gene : IEquatable<Gene>
    {
        #region Fields
        private object m_value;
        #endregion

        public Gene(object value)
        {
            m_value = value;
        }

        public Object Value => m_value;
        
        
        #region Methods

        public static bool operator ==(Gene first, Gene second)
        {
            return first.Equals(second);
        }
        
        public static bool operator !=(Gene first, Gene second)
        {
            return !(first == second);
        }

        public override string ToString()
        {
            return Value != null ? Value.ToString() : String.Empty;
        }
        
        public bool Equals(Gene other)
        {
            if (Value == null)
                return other.Value == null;

            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is Gene other)
            {
                return this.Equals(other);
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (Value == null)
            {
                return 0;
            }

            return Value.GetHashCode();
        }
        #endregion
    }
}
