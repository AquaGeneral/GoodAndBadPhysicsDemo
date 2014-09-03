Good and Bad Physics Demo
=====================
Accompanying demo for the article hosted on Tuts+ Game Development: [How To Fix Common Physics Problems in Your Game](http://gamedevelopment.tutsplus.com/articles/how-to-fix-common-physics-problems-in-your-game--cms-21418).

###Description
This demo shows most mistakes mentioned in this article in both a incorrect or broken state, and in a fixed state.


###Scenarios
####Normal Scale
This is a simple scene with a ball that hits into a stack of barrels. The alternate version Incorrect Scale (10x) shows how the scene changes at 10x scale (see the scale markings on the floor of the demo). Notice how it appears to be in slow-motion, but this effect is simply caused by the scale of the scene.

####Character Controller
This shows a simple side-scroller game working as intended. The "bad" alternative; Rigidbody & Character Controller Together shows the same game, but with a Rigidbody component attached to the character. Notice how the Rigidbody breaks the behavior of the Character Controller.

####Objects With Bounciness
Barrels are shot from the side of the scene and are hurled into the air. As they come crashing down, they bounce off the ground and move in simple but majestic ways. The broken version shows the same scene without bounciness, and it looks so much more boring in comparison.

####Not Directly Modifying a Rigidbody's Transform
In this scenario, a heavy ball is pushed up a ramp using Rigidbody.AddForce(). In the second scenario, instead of using Rigidbody.AddForce() to move the ball up the ramp, Transform.position is used. The result of using Transform.position is that the ball rolls back down, due to the fact that the rigidbody isn't properly taking into account the rigidbody's velocity change, but then the position modification causes the ball to jitter up and down the ramp.
