using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Model
{
    internal class Directory
    {
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int chapter;
        public int Chapter
        {
            get { return chapter; }
            set { chapter = value; }
        }

        private int valueDir;
        public int ValueDir
        {
            get { return valueDir; }
            set { valueDir = value; }
        }

        public Directory() { }

        public Directory(string name, int chapter, int value)
        {
            this.name = name;
            this.chapter = chapter;
          //  this.Value = value;
        }
    }
}
