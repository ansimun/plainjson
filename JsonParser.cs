using System;
using System.Collections;
using System.Text;
using System.Globalization;

namespace Json
{
    public class JsonParser
    {
        private int currentIndex = 0;
        private char[] characters = null;

        private bool IsAtStringDelimiter
        {
            get
            {
                return this.CurrentCharacter.Equals('"');
            }
        }
        private bool IsAtNumberStart
        {
            get
            {
                return (Char.IsDigit(this.CurrentCharacter) ||
                    this.CurrentCharacter.Equals('-'));
            }
        }
        private bool IsAtObjectStart
        {
            get
            {
                return this.CurrentCharacter.Equals('{');
            }
        }
        private bool IsAtObjectEnd
        {
            get
            {
                return this.CurrentCharacter.Equals('}');
            }
        }
        private bool IsAtArrayStart
        {
            get
            {
                return this.CurrentCharacter.Equals('[');
            }
        }
        private bool IsAtArrayEnd
        {
            get
            {
                return this.CurrentCharacter.Equals(']');
            }
        }
        private bool IsAtBooleanStart
        {
            get
            {
                return (this.CurrentCharacter.Equals('t') ||
                    this.CurrentCharacter.Equals('f'));
            }
        }
        private bool IsAtNullStart
        {
            get
            {
                return this.CurrentCharacter.Equals('n');
            }
        }
        private bool IsAtCollectionSeparator
        {
            get
            {
                return this.CurrentCharacter.Equals(',');
            }
        }
        private bool IsAtEscapeSequence
        {
            get
            {
                return this.CurrentCharacter.Equals('\\');
            }
        }
        private bool IsAtUnicodeEscapeSequence
        {
            get
            {
                return (this.IsAtEscapeSequence &&
                    this.characters.Length - this.currentIndex >= 2 &&
                    this.characters[this.currentIndex + 1].Equals('u'));
            }
        }
        private char CurrentCharacter
        {
            get
            {
                if (this.currentIndex >= this.characters.Length)
                {
                    string message = string.Format("expected character at position {0} but is out of range",
                        this.currentIndex);
                    throw new FormatException(message);
                }
                return this.characters[this.currentIndex];
            }
        }

        public JsonParser()
        { }

        public object Parse(string json)
        {
            this.Initialize(json);
            return this.ParseValue();
        }

        private void Initialize(string json)
        {
            if (json == null)
                throw new ArgumentException("argument 'json' == null");

            this.characters = json.ToCharArray();
            this.currentIndex = 0;
            this.MoveToNonWhitespaceCharacter();
        }

        private object ParseValue()
        {
            object result = null;

            if (this.IsAtStringDelimiter)
                result = this.ParseString();
            else if (this.IsAtNumberStart)
                result = this.ParseNumber();
            else if (this.IsAtObjectStart)
                result = this.ParseObject();
            else if (this.IsAtArrayStart)
                result = this.ParseArray();
            else if (this.IsAtBooleanStart)
                result = this.ParseBoolean();
            else if (this.IsAtNullStart)
                result = this.ParseNull();
            else
                throw new FormatException(string.Format("invalid character at position {0}",
                    this.currentIndex));

            return result;
        }
        private Hashtable ParseObject()
        {
            Hashtable result = new Hashtable();

            this.ParseObjectStart();

            while (!this.IsAtObjectEnd)
            {
                if (result.Count > 0)
                {
                    this.ParseCollectionSeparator();
                }

                string key = this.ParseString();
                this.ParseKeyValuePairSeparator();
                object value = this.ParseValue();
                result.Add(key, value);
            }

            this.ParseObjectEnd();

            return result;
        }
        private ArrayList ParseArray()
        {
            ArrayList result = new ArrayList();

            this.ParseArrayStart();

            while (!this.IsAtArrayEnd)
            {
                if (result.Count > 0)
                {
                    this.ParseCollectionSeparator();
                }
                result.Add(this.ParseValue());
            }

            this.ParseArrayEnd();

            return result;
        }
        private string ParseString()
        {
            StringBuilder result = new StringBuilder();

            this.ParseStringDelimiter();

            while (!this.IsAtStringDelimiter)
            {
                if (this.IsAtUnicodeEscapeSequence)
                {
                    result.Append(this.ParseUnicodeEscapeSequence());
                }
                else if (this.IsAtEscapeSequence)
                {
                    result.Append(this.ParseSingleCharacterEscapeSequence());
                }
                else
                {
                    result.Append(this.ParseSingleCharacter());
                }
            }

            this.ParseStringDelimiter();

            return result.ToString();
        }
        private double ParseNumber()
        {
            double result;

            string numberText = this.ParseNumberText();

            if (!Double.TryParse(numberText, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                throw new Exception("invalid json string");
            }

            return result;
        }
        private bool ParseBoolean()
        {
            bool result = false;

            if (this.CurrentCharacter.Equals('t'))
            {
                result = true;
                this.currentIndex += 4;
            }
            else
            {
                result = false;
                this.currentIndex += 5;
            }

            this.MoveToNonWhitespaceCharacter();
            return result;
        }
        private object ParseNull()
        {
            this.currentIndex += 4;
            this.MoveToNonWhitespaceCharacter();
            return null;
        }
        private string ParseNumberText()
        {
            StringBuilder result = new StringBuilder();

            int nextNonNumberCharacter = this.FindNextNonNumberCharacter();
            if (nextNonNumberCharacter < 0)
            {
                result.Append(this.characters, this.currentIndex, this.characters.Length - this.currentIndex);
            }
            else
            {
                result.Append(this.characters, this.currentIndex, nextNonNumberCharacter - this.currentIndex);
            }

            this.currentIndex += result.Length;
            this.MoveToNonWhitespaceCharacter();

            return result.ToString();
        }
        private void ParseObjectStart()
        {
            this.ParseCharacter('{');
        }
        private void ParseObjectEnd()
        { 
            this.ParseCharacter('}');
        }
        private void ParseArrayStart()
        {
            this.ParseCharacter('[');
        }
        private void ParseArrayEnd()
        {
            this.ParseCharacter(']');
        }
        private void ParseCollectionSeparator()
        {
            this.ParseCharacter(',');
        }
        private void ParseKeyValuePairSeparator()
        {
            this.ParseCharacter(':');
        }
        private void ParseStringDelimiter()
        {
            this.MoveToNonWhitespaceCharacter();

            if (!this.IsAtStringDelimiter)
            {
                string message = string.Format("expected '\"' at position {0} but is '{1}'",
                    this.currentIndex, this.CurrentCharacter);
                throw new FormatException(message);
            }

            this.currentIndex += 1;
        }
        private char ParseSingleCharacter()
        {
            char result = this.CurrentCharacter;
            this.currentIndex += 1;
            return result;
        }
        private char ParseSingleCharacterEscapeSequence()
        {
            char result = Char.MinValue;

            this.currentIndex += 1;

            if (this.CurrentCharacter.Equals('"'))
            {
                result = '"';
            }
            else if (this.CurrentCharacter.Equals('\\'))
            {
                result = '\\';
            }
            else if (this.CurrentCharacter.Equals('/'))
            {
                result = '/';
            }
            else if (this.CurrentCharacter.Equals('b'))
            {
                result = '\b';
            }
            else if (this.CurrentCharacter.Equals('f'))
            {
                result = '\f';
            }
            else if (this.CurrentCharacter.Equals('n'))
            {
                result = '\n';
            }
            else if (this.CurrentCharacter.Equals('r'))
            {
                result = '\r';
            }
            else if (this.CurrentCharacter.Equals('t'))
            {
                result = '\t';
            }
            else
            {
                string message = string.Format("invalide esquape sequence at position {0}",
                    this.currentIndex);
                throw new FormatException(message);
            }

            this.currentIndex += 1;

            return result;
        }
        private char ParseUnicodeEscapeSequence()
        { 
            char result = Char.MinValue;

            this.currentIndex += 2;

            if (this.characters.Length - this.currentIndex < 4)
            {
                string message = string.Format("expected unicode character sequence of length 4 but only {0} characters are left",
                    this.characters.Length - this.currentIndex);
                throw new FormatException(message);
            }

            string unicodeSequence = new string(this.characters, this.currentIndex, 4);
            int unicodeNumber;
            if (!Int32.TryParse(unicodeSequence, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out unicodeNumber))
            {
                string message = string.Format("expected valid unicode sequence at position {0}",
                    this.currentIndex);
                throw new FormatException(message);
            }
            result = (char)unicodeNumber;

            this.currentIndex += 4;

            return result;
        }
        private void ParseCharacter(char c)
        {
            this.MoveToNonWhitespaceCharacter();

            if (!this.CurrentCharacter.Equals(c))
            {
                string message = string.Format("expected '{0}' at position {1} but is '{2}'");
                throw new FormatException(message);
            }
            this.currentIndex += 1;

            this.MoveToNonWhitespaceCharacter();
        }

        private void MoveToNonWhitespaceCharacter()
        {
            int nonWhitespaceIndex = this.FindNonWhitespaceCharacter();
            this.currentIndex = nonWhitespaceIndex;
        }
        private int FindNonWhitespaceCharacter()
        {
            int result = this.currentIndex;
            for (int i = this.currentIndex; i < this.characters.Length; i++)
            {
                if (!Char.IsWhiteSpace(this.characters[i]))
                {
                    result = i;
                    break;
                }
            }
            return result;
        }
        private int FindNextNonNumberCharacter()
        {
            int result = -1;

            for (int i = this.currentIndex + 1; i < this.characters.Length; i++)
            {
                if (!this.IsNumberCharacter(this.characters[i]))
                {
                    result = i;
                    break;
                }
            }

            return result;
        }
        private bool IsNumberCharacter(char c)
        {
            return (Char.IsDigit(c) ||
                    c.Equals('e') || c.Equals('E') ||
                    c.Equals('+') || c.Equals('-') ||
                    c.Equals('.'));
        }
    }
}
