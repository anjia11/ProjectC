namespace _Scripts.Behavior_Tree
{
    public class Repeater : Node
    {
        private BehaviorTree _dependency;
        public Repeater(string name, BehaviorTree dependency)
        {
            base.name = name;
            _dependency = dependency;
        }

        public override NodeStatus Process()
        {
            if (_dependency.Process() == NodeStatus.Failure)
            {
                return NodeStatus.Success;
            }
            NodeStatus childStatus = children[currentChild].Process();
            if (childStatus == NodeStatus.Running)
            {
                return NodeStatus.Running;
            }

            if (childStatus == NodeStatus.Failure)
            {
                return childStatus;
            }

            currentChild++;
            if (currentChild >= children.Count)
            {
                currentChild = 0;
            }
            return NodeStatus.Running;
        }
    }
}