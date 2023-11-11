# ForceInspiredNavigation

The hypothesis for this experiment was, if an agent treats obstacles like forces that grow as it gets closer to them the agent will be able to move around them in a superficially natural way. This turned out to be somewhat true. The agent under the right circumstances can move smoothly around obstacles. There are less optimal circumstances where the agent appears almost drunk, bumping into an obstacle before moving around it. Under the worst circumstances the agent will get stuck moving forward and backward towards its destination stuck on an obstacle directly in its path.

## Method:

The concept is based on the addition of forces:

$$ F = F_1 * \< cos(\theta_1), sin(\theta_1) \> + ... + F_n * \< cos(\theta_n), sin(\theta_n) \> $$

We have one force $F_{dest}$ which is a constant and represents the pull of our destination.

Then we have $F_{obst_i} = {-F_{dest} \over {dist^2}}$ which is the force pushing the agent away from an object the closer it gets. 
$dist$ there is the distance from the nearest point on the surface of an obstacle and the same of the agent.

Then $\theta_i$ is the angle from the center of our agent to the nearest point on the surface of the i-th obstacle.

Then we add up $F$ like the equation above, normalize it, and use it as the direction vector of our agent.

## Demov Video

https://github.com/tzcrowdis/ForceInspiredNavigation/assets/100492586/7eb6301f-f711-45cb-a3b1-56e37a7eba3a

## Potential Future Improvements:
- add line of sight force and make destination pull weaker so that the agent moves toward open areas
- or add random forces occasionally to free up stuck agents (and lean into the drunk aspect)

### Misc:
Unity version: 2021.3.15f1
