
# Air BnB Data visualisation

### Results: Top in year - 95%
### The task:
 - Using the provided data, read the data in using a proper datastructure. 
The user needs to be able to search, update, create new and delete a property / neighbourhood / district.
 - Any changes to the data needs to be automatically saved.
 - Data needs to be visualised graphically without using any in-built C# libraries or tools.

### Problems faced / solutions:
 - On form load, the program was unresponsive for a few seconds whilst everything was loaded and calculated. This was because the whole program ran on a sinlge thread. The first solution to this was to limit the calculations made, so calculations like the average price weren't run until after all the data was loaded and the form was running. The second solution was to run the graph calculations on different threads. This is why on start-up, the map and graphs turn visable after a few seconds.
 
 - When creating bar graphs, the point ( 0 , 0 ) is the top left, rather than the bottom right in traditional mathematics. The original solution I had was to do all the calculations and draw the graph, then rotate it 180° to display it properly. On reflection, I could have used the point (this.Bottom , this.Left ) as the origin, this would have stopped the need to rotate it.

### Further improvments
- To speed up the execution time of the program, I could have created some form of cache to keep each graph. This would stop the need to re-calculate each graph every time a user changes district. 

- (Update 5 months after submission) : Now that I have learnt asyncronus C#, I realised that alot of the processes were ran on the main thread, locking-up the UI. If I were to do this again, I would have used Tasks as to not lock the main thread and make the program feel much more responsive

- The graphs would have been easier to read if the width was adjusted so that more could have been seen on one panel, possibly having another window to view it better?

- The graphs could've been user-sortable i.e. high => low 

- Graph sorting by distance would've also been nice, but WAY out of the specification

- Tooltips when mousing over graph to show the value of the item you've moused over

 ## The final solution
 
 ![application image](https://i.imgur.com/uuvDW4e.jpg)
 
