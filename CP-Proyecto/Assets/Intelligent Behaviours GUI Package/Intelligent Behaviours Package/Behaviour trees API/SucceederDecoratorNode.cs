﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SucceederDecoratorNode : TreeNode {

    public SucceederDecoratorNode(string name, TreeNode child, BehaviourTreeEngine behaviourTree)
    {
        base.Child = child;
        Child.ParentNode = this;
        base.StateNode = new State(name, () => { }, behaviourTree); // Empty action to prevent going to child too early
        base.behaviourTree = behaviourTree;
    }

    private void ToChild()
    {

        if(Child.ReturnValue != ReturnValues.Running)
            return;

        ReturnValue = ReturnValues.Running;
        Child.ReturnValue = ReturnValues.Running;
        Debug.Log("ToChild"+Child.StateNode.Name + StateNode.Name);
        new Transition("to child", StateNode, new PushPerception(behaviourTree), Child.StateNode, behaviourTree)
            .FireTransition();

        behaviourTree.ActiveNode = Child;
    }

    public override void Update()
    {
        Debug.Log(firstExecution);
        if (!firstExecution) { ToChild(); firstExecution = true;}; // First loop goes to child
        if (Child.ReturnValue != ReturnValues.Running) {
            if(ReturnNodeValue() != ReturnValues.Running) {
                ReturnToParent();
                Child.Reset();
                firstExecution = false; //ADDED 
            }
        }
        
    }

    public override ReturnValues ReturnNodeValue()
    {
        Child.ReturnValue = ReturnValues.Succeed;
        ReturnValue = Child.ReturnValue;
       
        return ReturnValue;
    }
}