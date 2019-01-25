## Behaviour Tree

Behaviour tree implementation in C#

A Behavior Tree (BT) is a mathematical model of plan execution used in computer science, robotics, control systems and video games. 
They describe switchings between a finite set of tasks in a modular fashion. 
Their strength comes from their ability to create very complex tasks composed of simple tasks, without worrying how the simple tasks are implemented. 
BTs present some similarities to hierarchical state machines with the key difference that the main building block of a behavior is a task rather than a state.
Its ease of human understanding make BTs less error prone and very popular in the game developer community.

## What is it?

A behaviour tree consists of 4 types of nodes:
- Root Node
- Selector Node
- Sequence Node
- Action Node

Root node - an idle node that does nothing except from being the root of the tree
Selector node - iterates over child nodes and ticks them, until the first child fails
Sequence node - iterates over child nodes and ticks them, finds the first child that has not yet succeeded
Action node - actually does your work like moving the character to a nearest cover or shoot at the closest target

Action nodes are leaf nodes in the tree, meaning they cannot have any children... unless you add them :D 

## API

Currently the `SimpleBehaviourTree` works, somewhat. 
There are still some components missing like serialization, which I guess render's it as useless for now...

But wait!

I am going to work on this project in order to add things like serialization components, ticker engine component and Unity wrappers.

