using UnityEngine;

namespace _Scripts.Behavior_Tree
{
    public class Leaf : Node
    {
        public delegate NodeStatus Tick();

        public Tick ProcessMethod;


        public Leaf() { }

        public Leaf(string name, Tick method)
        {
            base.name = name;
            ProcessMethod = method;
        }

        public override NodeStatus Process()
        {
            NodeStatus s;
            if (ProcessMethod != null)
                s = ProcessMethod();
            else
                s = NodeStatus.Failure;

            Debug.Log(name +" "+ s);
            return s;
        }
    }
}