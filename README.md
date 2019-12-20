# ML-vs-GOAP-Unity-Demo

This project was created as part of my graduation work. 
The goal was to create an AI based on Machine Learning Algorithms and compare it to a more traditional approach like Goal-Oriented Action Planning (GOAP).

Reinforcement Learning in the Unity Machine Learning environment was used to train the Agents.
The GOAP-AI was implemented by using Sploregs GOAP implementation (see link below) and fitting it to my project.

A playing field resembling an arena was chosen to test both AIs against each other.
The goal of each team is to activate both platforms in the middle at the same time and transport the spawning bomb into the opposing teams base.
Agents can remove opposing agents from the playing field (for a few seconds) by colliding with their side/back.

The Machine Learning environment was designed to train agents in multiple steps and teach them to reach goals and account for opposing agents.

Below you can see the result:

![](Doc/unityMLvsGOAPdoc.gif)

**See Learning Process:**
https://youtu.be/qp7AaqJI2oY

See Results:
https://youtu.be/KHZ0Fa8jcZ8

**How to get it to run?**

Download the ML-Agents v0.5 Github Project and copy its Assets/ML-Agents folder to Monstertrainer/Assets.

#### DISCLAIMER - This project uses two other GitHub Projects:

ML-Agents from Unity-Technologies in Version v0.5
https://github.com/Unity-Technologies/ml-agents

Sploregs GOAP Implementation
https://github.com/sploreg/goap

Developed in Unity 2018.2.5f1.
