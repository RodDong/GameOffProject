What should I do now?
    * [Investigate the boss]
    -> boss
    * [Investigate the doctor]
    -> doctor
    * [Investigate the chef]
    -> chef
    * [Investigate the bar maiden]
    -> barmaiden
    * [I'm Tired. Going Home]
    ...#speaker:None #progress:0

-(boss)
Better Go Home First #speaker:None #progress:0 #home:yes
->END

-(doctor)
Better Go Home First #speaker:None #progress:1 #home:yes
->END

-(chef)
Better Go Home First. #speaker:None #progress:2 #home:yes
->END

-(barmaiden)
Better Go Home First #speaker:None #progress:3 #home:yes
->END