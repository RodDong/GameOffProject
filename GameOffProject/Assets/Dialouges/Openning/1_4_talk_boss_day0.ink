"Off from work?" #speaker:Boss #portrait:neutural #tachie:none

"Kind of..." #speaker:P #portrait:neutural #tachie:none
"You know what? You've been doing good this month."#speaker:Boss #portrait:neutural #tachie:none
"I think you did outperform 80% people in the department. Very impressive. I'll consider promoting you by the end of the year."#speaker:Boss #portrait:neutural #tachie:none

"T-Thanks, sir. Good to know that."#speaker:P #portrait:neutural #tachie:none

Am I doing well? Relatively speaking, probably, yes. Am I doing THAT well? May be too much exaggregation.
"I was thinking -- maybe you are interested in going to dinner with me?"#speaker:Boss #portrait:neutural #tachie:none
"Some of the company leaderships are gonna meet at a great dining place in town. I'm sure they would be happy to see an employee making this kind of contribution like you."#speaker:Boss #portrait:neutural #tachie:none

-(begin_choice)
    * "I'm good with that."
    -> dinner
    * "Actually I have a doctor's appointment."
    -> clinic
    * "An old friend invites me to drink."
    -> bar
    * "Sorry, but I feel too sick today."
    -> home
    
-(dinner)
foo
-> placeholder

-(clinic)
foo
-> placeholder

-(bar)
foo
-> placeholder

-(home)
“There is a family emergency that I need to attend to, see, my sister’s daughter needs to be picked up and my sister’s out of town... I do really appreciate the offer, though, that must be a wonderful place to dine. Please do enjoy it.” #speaker:P #portrait:neutural #tachie:none
You smiled a little, trying to be as sorry as one possibility could. You bowed down your head to not look at glare that almost broke free of his eyes.
You picked up your laptop bag. No restaurant’s taking you away from that sweet bed of yours now that you are off of work.
Or, you are very mistaken.
-> placeholder

-(placeholder)
//foo
