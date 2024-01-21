using System.Collections.Generic;

namespace _Scripts.Behavior_Tree
{
    public class Node
    {
        public NodeStatus Status = NodeStatus.Running;
        public List<Node> children = new List<Node>();
        public int currentChild = 0;
        public string name;

        public Node(){}

        public Node(string name)
        {
            this.name = name;
        }

        public void AddChild(Node node)
        {
            children.Add(node);
        }

        public virtual void OnStart(){ }

        public virtual NodeStatus Process()
        {
            return children[currentChild].Process();
        }
    }
}