using Shared.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Dtos.References
{
    public class ReferenceString
    {
        public string Value { get; set; }

        private const string ATTRIBUTE_SEPARATOR = ":";
        private const string REFERENCE_SEPARATOR = ";";

        public ReferenceString()
        {
        }

        public ReferenceString(string value)
        {
            Value = value;
        }

        public ReferenceString(Guid id, string value)
        {
            Value = id.ToString() + ATTRIBUTE_SEPARATOR + value + REFERENCE_SEPARATOR;
        }

        public Guid GetId()
        {
            Dictionary<Guid, string> parsedReferenceString = Parse(Value);
            return parsedReferenceString.First().Key;
        }

        public List<Guid> GetIds()
        {
            if(Value == null)
            {
                return null;
            }
            Dictionary<Guid, string> parsedReferenceString = Parse(Value);
            return parsedReferenceString.Keys.ToList();
        }

        public string GetValue()
        {
            Dictionary<Guid, string> parsedReferenceString = Parse(Value);
            return parsedReferenceString.First().Value;
        }

        public string GetValues()
        {
            if (Value == null)
            {
                return string.Empty;
            }
            Dictionary<Guid, string> parsedReferenceString = Parse(Value);
            return StringUtils.SeparateString(parsedReferenceString.Values.ToList(), ", ");
        }

        public void Append(Guid id, string value)
        {
            Value += id.ToString() + ATTRIBUTE_SEPARATOR + value + REFERENCE_SEPARATOR;
        }

        public override string ToString()
        {
            return Value;
        }

        public Dictionary<Guid, string> GetReferencies()
        {
            return Parse(Value);
        }

        public static ReferenceString Create<T>(IList<T> dtos) where T : BaseDto
        {
            ReferenceString referenceString = new ReferenceString();
            dtos.ToList().ForEach(x => referenceString.Append(x.Id, x.ToString()));
            return referenceString;
        }

        private static Dictionary<Guid, string> Parse(string referenceString)
        {
            if(referenceString == null)
            {
                return null;
            }
            string[] values = referenceString.Split(new string[] { REFERENCE_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<Guid, string> parsedReferenceString = new Dictionary<Guid, string>();
            foreach (string value in values)
            {
                string[] pair = value.Split(new string[] { ATTRIBUTE_SEPARATOR }, StringSplitOptions.RemoveEmptyEntries);
                parsedReferenceString.Add(Guid.Parse(pair[0]), pair[1]);
            }
            return parsedReferenceString;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is ReferenceString))
            {
                return false;
            }
            ReferenceString rds = obj as ReferenceString;
            return Value.Equals(rds.Value);
        }
    }
}
