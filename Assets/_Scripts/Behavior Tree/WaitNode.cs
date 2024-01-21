using UnityEngine;

namespace _Scripts.Behavior_Tree
{
    public class WaitNode : Node
    {
        private float waitTime;
        private float startTime;
        public WaitNode(float timeToWait)
        {
            base.name = "Wait";
            waitTime = timeToWait;
            startTime = waitTime;
        }

        public override NodeStatus Process()
        {
            if (Status == NodeStatus.Running)
            {
                startTime -= Time.deltaTime;
                if (startTime < 0)
                {
                    startTime = waitTime;
                    Status = NodeStatus.Success;
                }
                //Debug.Log("Wait == " + startTime);
            }else if (Status == NodeStatus.Failure)
            {
                Status = NodeStatus.Running;
            }
            else
            {
                Status = NodeStatus.Running;
            }
            return Status;
        }
    }
}