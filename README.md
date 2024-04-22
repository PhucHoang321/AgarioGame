# Agario
```
	Author:     Phuc Hoang
	Partner:    Chanphone Visathip
	Start Date: 7-April-2024
	Course:     CS 3500, University of Utah, School of Computing
	GitHub ID:  PhucHoang123
	Repo:		https://github.com/uofu-cs3500-spring24/assignment-eight-agario-v_tekkkkk
	Commit Date:21-April-2024 11:25pm
	Solution:	Agario
	Copyright:  CS 3500 and Phuc Hoang and Chanphone Visathip - This work may not be copied for use in Academic Coursework.
```

# User Interface and Game Design Decisions
	We aim for simple minimal design that is easy to read and play.
	The welcome page GUI is looking very good.
# Overview of the Agario functionality
	Let the player eat foods, other players, also spit when reach the requirement mass. 
# Consulted Peers:
	We talked with Trenton,Ted and Shadrach.
# Comments to Agario:
	-N/A-
# Assignment Specific Topics
	Drawing the scence: this were a bottleneck for us. Figuring out how to display the blob at the center, choosing what to draw
	around it was difficult. We solved it by calculating a bound base on our player coordinate as the center. This allows us to tell
	what should be draw and keep the player at the center of the view portal.
# Testing
	We test all these functions by designing it first, write the code, run the program to test the newly added feature. Then, we
	dedicate sometimes at the end each coding session to test all the new function at once. By doing this, we can make sure the 
	new stuffs work and does not break the current code.
# Time Expenditures:
	Assignment Eight: Predicted Hour:35   Actual Hour:27 (not included the 3 steps tutorial)
	We think our time predicted is getting very good. I think because of the tutorials that give us the better time prediction.
# Partnerships
	Phuc Hoang: 3
	Chanphone Visathip: 3 
	Pair programming hour: 21
	Debug hour: 3
	We was struggling on try to get the view portal to show correctly. 

# Good software practice (GSP)	
	1.Well-named, commented and short methods : 
		We use lots of small comment on some methods. That tell the reader know what that actual do.
	2.Single Responsibility Principle:
		For example, in MainPage.xaml.cs we have it do all the GUI work,
		and in World Class we have it handle all the incoming data such as, add player, add food,e.t.c..	
# References
	1.Piazza post
	2.ChatGPT - https://chat.openai.com/
	3.Lab 12
	4.Color.FromArgb Method - https://learn.microsoft.com/en-us/dotnet/api/system.drawing.color.fromargb?view=net-8.0 // decide to use Color.FromInt later on
	5.GraphicsView - https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/graphicsview?view=net-maui-8.0
	6.DrawString - https://learn.microsoft.com/en-us/dotnet/api/system.drawing.graphics.drawstring?view=dotnet-plat-ext-8.0
	
	
	
	
