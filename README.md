<div align="center">

[![C#](https://img.shields.io/badge/Language-C%23-%23239120.svg?style=plastic)](https://en.wikipedia.org/wiki/C%2B%2B)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=plastic)
[![Windows](https://img.shields.io/badge/Platform-Windows-0078d7.svg?style=plastic)](https://en.wikipedia.org/wiki/Microsoft_Windows)
[![x86](https://img.shields.io/badge/Arch-x86-red.svg?style=plastic)](https://en.wikipedia.org/wiki/X86)
[![License](https://img.shields.io/github/license/R3nzTheCodeGOD/R3nzSkin.svg?style=plastic)](LICENSE)
</div>

<h1 style="text-align:center">A* Pathfinding Visualization</h1>

This is my first attempt at creating an efficient A* pathfinding algorithm. **This was made from scratch without any UI/algorithmic library**.
If there is a path, it will **always** find the shortest way to reach it and then renders it.

## Basic Controls
You must create a map to start the pathfinding. The start node is blue, end node is red and the walls are black. 

To create nodes:
  - Start: press 'S'
  - End: press 'E'
  - Wall: left click
  
To delete nodes:
  - Right click on top of node
  
![basic-controls](https://raw.githubusercontent.com/lipeeeee/astar_pathfinding/master/docs/Sample.png)

## Diagonal
My algorithm supports both diagonal and non diagonal pathfinding. 

Simply check the "diagonal" box at the bottom left of the screen.

![diagonal-vs-non-diagonal](https://github.com/lipeeeee/astar_pathfinding/blob/master/docs/SampleNonDiag.png?raw=true)


## Timed Efficiency
The algorithm after a successful attempt at pathfinding will show the amount of time it took to compute the fastest path along with the amout of open and closed nodes.

![time-efficiency](https://github.com/lipeeeee/astar_pathfinding/blob/master/docs/TimedEfficiency.png?raw=true)

## Generated Walls
### Random Maze
I've also developed a random maze generator(**it only generates walls so you have to choose where you want the open and end nodes**).

![random-maze](https://github.com/lipeeeee/astar_pathfinding/blob/master/docs/RandomMaze.png?raw=true)

## Scenes
### Exporting/Importing Scene
You can export your current scene or import someone else's scene in the menu strip "*Import/Export*".

![import-export](https://github.com/lipeeeee/astar_pathfinding/blob/master/docs/ImportExport.png?raw=true)

### Presets
There are custom made scene presets that you can try and edit on your own. You can find them in the presets folder, download them and import whichever one you want.

![preset-example-one](https://github.com/lipeeeee/astar_pathfinding/blob/master/docs/presetExample1.png?raw=true)

![preset-example-two](https://github.com/lipeeeee/astar_pathfinding/blob/master/docs/presetExample2.png?raw=true)

## Complicated Stuff
Euclidean Heuristic Function for Diagonal Movement:
```js
function heuristic(node) =
    dx = abs(node.x - goal.x)
    dy = abs(node.y - goal.y)
    return D * sqrt(dx * dx + dy * dy)
```

4-Way Heuristic Function(D=10, D2=14):
```js
function heuristic(node) =
    dx = abs(node.x - goal.x)
    dy = abs(node.y - goal.y)
    return D * (dx + dy) + (D2 - 2 * D) * min(dx, dy)
```

Get Path Pseudo-Code:
```cs
int newG, dx, dy;
bool newPath;
Node lowestCost;
List<Node> neighbours, open = new()
{
    new Node(globals.start_ij, null)
}, close = n

// While there are nodes to explore
while (open.Count > 0)
{
    // Get lowest f cost in open list
    lowestCost = getLowestFCost(open);

    open.Remove(lowestCost);
    close.Add(lowestCost);

    // Found end
    if (lowestCost.ij[0] == globals.end_ij[lowestCost.ij[1] == globals.end_ij[1])
    {
        pathCount = retracePath(lowestCost, close);
        openCount = open.Count;
        closeCount = close.Count;
        return true;
    }

    // Get neighbours
    neighbours = getNormalizedNeighbours(lowestCost);
    for (int i = 0; i < neighbours.Count; i++)
    {
        if (close.Contains(neighbours[i]))
        {
            continue;
        }

        // Recalculate node values in runtime
        newPath = false;
        dx = lowestCost.ij[0] - neighbours[i].ij[0];
        dy = lowestCost.ij[1] - neighbours[i].ij[1];
        newG = (dx != 0 && dy != 0) ? lowestCos14 : lowestCost.g + 10;
        if (open.Contains(neighbours[i]))
        {
            if (newG < neighbours[i].g)
            {
                neighbours[i].g = newG;
                newPath = true;
            }
        }
        else
        {
            neighbours[i].g = newG;
            newPath = true;
            open.Add(neighbours[i]);

        if (newPath)
        {
            neighbours[i].updateHeuristic();
            neighbours[i].parent = lowestCost;
        }
    }
}

return false;
```

*a project by lipeeeee.*
