using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CellStyle
{
    public class Data
    {
        public static List<Node> DataList
        {
            get
            {
                List<Node> list = new List<Node>();
                for (int i = 0; i < 10; i++)
                {
                    list.Add(new Node("key" + i, "line " + i));
                }
                return list;
            }
        }
    }

    public class Node
    {
        public string Key { get; set; }
        public string Text { get; set; }

        public Node(string key, string text)
        {
            Key = key;
            Text = text;
        }
    }
}
