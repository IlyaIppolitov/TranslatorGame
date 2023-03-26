using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TranslatorGame.ValueObjects
{
    [Owned]
    public class Password
    {
        public string Value { get; private set; }

        protected Password() { }
        public Password(string value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (!IsPassword(value))
            {
                throw new ArgumentException("Пароль задан некорректно!");
            }
            Value = value;
        }

        public override string ToString() => Value;

        private static bool IsPassword(string value)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            return hasNumber.IsMatch(value) && hasUpperChar.IsMatch(value) && hasMinimum8Chars.IsMatch(value);
        }

        protected bool Equals(Password other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Password)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Password? left, Password? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Password? left, Password? right)
        {
            return !Equals(left, right);
        }
    }
}
