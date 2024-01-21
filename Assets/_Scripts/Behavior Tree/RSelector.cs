using _Scripts.Behavior_Tree.Utils;

namespace _Scripts.Behavior_Tree
{
    public class RSelector : Node
    {
        private bool shuffled = false;
        public RSelector(string name)
        {
            base.name = name;
        }

        public override NodeStatus Process()
        {
            if (!shuffled)
            {
                children.Shuffle();
                shuffled = true;
            }
            NodeStatus childStatus = children[currentChild].Process();
            if (childStatus == NodeStatus.Running)
            {
                return NodeStatus.Running;
            }

            if (childStatus == NodeStatus.Success)
            {
                currentChild = 0;
                shuffled = false;
                return NodeStatus.Success;
            }

            currentChild++;
            if (currentChild >= children.Count)
            {
                currentChild = 0;
                shuffled = false;
                return NodeStatus.Failure;
            }
            return NodeStatus.Running;
        }
    }
}