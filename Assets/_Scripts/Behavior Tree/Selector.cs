using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Behavior_Tree
{
    public class Selector : Node
    {
        public Selector(string name)
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

            if (childStatus == NodeStatus.Success)
            {
                currentChild = 0;
                return NodeStatus.Success;
            }

            currentChild++;
            if (currentChild >= children.Count)
            {
                currentChild = 0;
                return NodeStatus.Failure;
            }
            return NodeStatus.Running;
        }
    }
}

