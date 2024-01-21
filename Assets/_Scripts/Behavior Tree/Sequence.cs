namespace _Scripts.Behavior_Tree
{
    public class Sequence : Node
    {
        public Sequence(string name)
        {
            base.name = name;
        }

        public override NodeStatus Process()
        {
            NodeStatus childStatus = children[currentChild].Process();
            if (childStatus == NodeStatus.Running)
            {
                return NodeStatus.Running;
            }

            if (childStatus == NodeStatus.Failure)
            {
                //currentChild = 0;
                return childStatus;
            }

            currentChild++;
            if (currentChild >= children.Count)
            {
                currentChild = 0;
                return NodeStatus.Success;
            }
            return NodeStatus.Running;
        }
    }
}