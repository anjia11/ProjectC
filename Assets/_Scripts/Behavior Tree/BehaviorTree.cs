using UnityEngine;

namespace _Scripts.Behavior_Tree
{
    public class BehaviorTree : Node
    {
        public BehaviorTree()
        {
            name = "Root";
        }

        public BehaviorTree(string name)
        {
            base.name = name;
        }

        public override NodeStatus Process()
        {
            if (children.Count == 0) return NodeStatus.Success;
            return children[currentChild].Process();
        }

        private string PrintTree(Node node, int level = 0)
        {
            string str = $"\n{new string('-', 4 * level)}{node.name}";
            foreach (var child in node.children) str += PrintTree(child, level + 1);
            return str;
        }
        
        public void PrintTree()
        {
            string str = PrintTree(this);
            Debug.Log(str + '\n');
        }
    }
}