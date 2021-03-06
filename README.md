Moviepicker
===========

This app will suggest movies for you to watch. It takes into account your history to determine what movie you might like based on certain metrics. 

General concept mockups:

![UI Mockup](http://i.imgur.com/QWsAFsX.png)

### How far along is the project?

At the time of writing, I'm deciding on the exact approach of how I determine the metrics. Once that is done I will implement the rest of the REST Api interface after which a proof of concept of the Android app will be started. You can follow the exact progress through the YouTRACK issue tracker. If you would like access, send me an email at `jer_vannevel<at>outlook.com`.

### How do I contribute?

I am not actively looking for help but I will review every code you send in. Do note that I would like to do the initial work on the Android app and as such I will be hesitant with pull requests towards this aspect. If you want to improve another aspect of the code, shoot me an email or open a pull request so we can discuss some specifics.

### How does it work?

The database holds user-specific values for each year, language and genre. This value will be increased or decreased based on whether you like or dislike a certain show or movie. Based on these values it will select a movie that you might like. It is evident that at first you will receive seemingly random movies and that the more you "train" the program, the more accurate its predictions will become.

!!However!!

In an initial phase this selection algorithm will not be implemented. Instead, I will simply use the TMDb provided API call to retrieve similar movies. The reason for this is that I want to get started with actually implementing the Android app since I'm using that to learn Android programming for an exam I have in the summer. Once all that is done I can replace it with my custom algorithm.

### Is this scalable?

No clue. We'll see this when we get there!

## Links

YouTRACK: https://moviepicker.myjetbrains.com/youtrack/
