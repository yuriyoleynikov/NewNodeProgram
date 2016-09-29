using System;

namespace NodeProg
{
    class NodeProgram
    {
        static void Main(string[] args)
        {
            var ln = Node.ParseNodes("test,(test2)");
            foreach (var node in ln)
            {
                Console.Write(node);
            }
        }
    }
}