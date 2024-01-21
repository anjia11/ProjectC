namespace _Scripts.Behavior_Tree
{
    public class Inverter : Node
    {
        public Inverter(string name)
        {
            base.name = name;
        }

        public override NodeStatus Process()
        {
            NodeStatus childStatus = children[0].Process();
            if (childStatus == NodeStatus.Running)
            {
                return NodeStatus.Running;
            }

            if (childStatus == NodeStatus.Failure)
            {
                return NodeStatus.Success;
            }
            else
            {
                return NodeStatus.Failure;
            }
        }
    }
}