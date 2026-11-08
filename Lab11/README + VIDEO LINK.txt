VIDEO LINK:

https://youtu.be/XEyk9scqtLk

README:

How does each pathfinding algorithms calculate and prioritize paths?

Breadth-First Search (BFS): By calculating all of the closest nodes and slowly fanning out in order.
Depth-First Search (DFS): By going as far as possible on a branch, then continuing to the next.
Dijkstra’s Algorithm: By calculating all possible paths
Greedy Best-First (GBF): By trying to get closer to the target with each step until it no longer can, then changes directions and tries again.
A* Algorithm: Calculates path cost for each node and draws a path based on the lowest estimated cost.


What challenges arise when dynamically updating obstacles in real-time?

Potentially closing off the path entirely, performance drops, or desyncing the pathfinder from the current obstacles.


Which algorithm should you choose and how should you adapt it for larger grids or open-world settings?

Larger grids I think I would use A* pathfinding because it calculates a more direct path searching less area. You could adapt it by
using navmesh and cutting the grid into chunks that are relevant to where you are in the open world or large grid.


What would your approach be if you were to add weighted cells (e.g., "difficult terrain" areas)?

My approach would be to give those areas a higher cost in the pathfinding logic making them less appealing to path through, but not too high as to where they would mathematically never be used. For example if the terrain makes you 50% slower, I would probably double the cost of that cell, rather than say quadruple.