using System;
using System.Collections.Generic;
using System.Text;

namespace App1.ViewModels
{
    public class MainViewModel
    {
        public List<Field> Fields { get; set; }
        public MainViewModel()
        {
            Fields = new List<Field>
            {
                 new Field{ Name = "First field" },
                 new Field{ Name = "Second field"},
                 new Field{ Name = "Third field"}
            };
        }
    }
    public class Field
    {
        public string Name { get; set; }
    }
}
