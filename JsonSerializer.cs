using System;
using System.Text;
using System.Collections;

namespace Json
{
    public class JsonSerializer
    {
        private StringBuilder content = null;
        private int currentIndentation = 0;
		private bool prettify = false;
		private int indentation = 0;

        public bool Prettify 
		{ 
			get
			{
				return this.prettify;
			}
			set
			{ 
				this.prettify = value;
			}
		}
        public int Indentation
		{
			get
			{
				return this.indentation;
			}
			set
			{ 
				if (value < 0)
					throw new ArgumentException("value < 0");
				
				this.indentation = value;
			}
		}

        public JsonSerializer()
        {
            this.Indentation = 0;
            this.Prettify = false;
        }

        public string Serialize(object value)
        {
            this.Initialize();
            this.SerializeValue(value);

            return this.content.ToString();
        }

        private void Initialize()
        {
            this.content = new StringBuilder();
            this.currentIndentation = 0;
        }

        private void SerializeValue(object value)
		{
            if (value == null)
            {
                this.SerializeNullValue();
            }
            else if (value is Char)
            {
                this.SerializeString(Char.ToString((char)value));
            }
			else if (value is string) 
            {
				this.SerializeString((string)value);
            }
			else if (value is IDictionary) 
            {
				this.SerializeDictionary(value as IDictionary);
			} 
            else if (value is IList) 
            {
				this.SerializeList(value as IList);
			} 
            else if (value is Boolean) 
            {
                this.SerializeBoolean(Convert.ToBoolean(value));
			} 
            else if (value is ValueType) 
            {
				this.SerializeNumber(Convert.ToDouble(value));
			}
            else
            {
                throw new InvalidOperationException("type not serializable");
            }
		}
        private void SerializeDictionary(IDictionary dict)
		{
            this.AddObjectStartTokens();
            int i = 0;
            foreach (DictionaryEntry entry in dict)
            {
                this.SerializeKeyValuePair(entry.Key as string, entry.Value);
                if (i < dict.Count - 1)
                {
                    this.AddEnumerationSeparatorTokens();
                }

                i++;
            }
            this.AddObjectEndTokens();
		}
        private void SerializeList(IList list)
		{
            this.AddArrayStartTokens();

			for (int i = 0; i < list.Count; i++) 
            {
                this.SerializeValue(list[i]);
                if (i < list.Count - 1)
                {
                    this.AddEnumerationSeparatorTokens();
                }
			}

            this.AddArrayEndTokens();
		}
        private void SerializeKeyValuePair(string key, object value)
        {
            this.SerializeString(key);
            this.AddKeyValuePairSeparatorTokens();
            this.SerializeValue(value);
        }
        private void SerializeString(string value)
		{
			this.content.Append("\"");

			char[] charArray = value.ToCharArray();
			for (int i = 0; i < charArray.Length; i++) {
				char c = charArray[i];
				if (c == '"') {
					this.content.Append("\\\"");
				} else if (c == '\\') {
					this.content.Append("\\\\");
                } else if (c == '/') {
                    this.content.Append("\\/");
				} else if (c == '\b') {
					this.content.Append("\\b");
				} else if (c == '\f') {
					this.content.Append("\\f");
				} else if (c == '\n') {
					this.content.Append("\\n");
				} else if (c == '\r') {
					this.content.Append("\\r");
				} else if (c == '\t') {
					this.content.Append("\\t");
				} else {
					int codepoint = Convert.ToInt32(c);
					if ((codepoint >= 32) && (codepoint <= 126)) {
						this.content.Append(c);
					} else {
						this.content.Append("\\u" + Convert.ToString(codepoint, 16).PadLeft(4, '0'));
					}
				}
			}

			this.content.Append("\"");
		}
        private void SerializeNumber(double value)
		{
            if (double.IsNaN(value) || double.IsInfinity(value))
            {
                string message = "Unable to serialize NaN, Infinity, -Infinity numbers";
                throw new InvalidOperationException(message);
            }
			this.content.Append(value.ToString("r",System.Globalization.CultureInfo.InvariantCulture));
		}
        private void SerializeBoolean(bool value)
        {
            if (value)
            {
                this.content.Append("true");
            }
            else
            {
                this.content.Append("false");
            }
        }
        private void SerializeNullValue()
        {
            this.content.Append("null");
        }
        private void AddObjectStartTokens()
        {
            this.content.Append("{");

            if (this.Prettify)
            {
                this.content.Append("\n");
                this.currentIndentation += this.Indentation;
                this.content.Append(' ', this.currentIndentation);
            }
        }
        private void AddObjectEndTokens()
        {
            if (this.Prettify)
            {
                this.content.Append("\n");
                this.currentIndentation -= this.Indentation;
                this.content.Append(' ', this.currentIndentation);
            }

            this.content.Append("}");
        }
        private void AddArrayStartTokens()
        {
            this.content.Append("[");

            if (this.Prettify)
            {
                this.content.Append("\n");
                this.currentIndentation += this.Indentation;
                this.content.Append(' ', this.currentIndentation);
            }
        }
        private void AddArrayEndTokens()
        {
            if (this.Prettify)
            {
                this.content.Append("\n");
                this.currentIndentation -= this.Indentation;
                this.content.Append(' ', this.currentIndentation);
            }

            this.content.Append("]");
        }
        private void AddKeyValuePairSeparatorTokens()
        {
            if (this.Prettify)
                this.content.Append(": ");
            else
                this.content.Append(":");
        }
        private void AddEnumerationSeparatorTokens()
        {
            this.content.Append(",");

            if (this.Prettify)
            {
                this.content.Append("\n");
                this.content.Append(' ', this.currentIndentation);
            }
        }
    }
}
