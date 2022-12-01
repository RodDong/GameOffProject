// 房间调查：

// 色欲的床边的床头柜里：
// Evidence No.1 
-(choices)
Investigate the table
* [A Name List]
->bedside_table
* [A Menu]
->chair
* [A Phone]
->sims_phone
->END


-(bedside_table)

you find a list of Patients information: “XXX, 25 years old … healthy; XXX, 31 years old … healthy; XXX, 29 years old … healthy” 

“A list of patients information and healthy reports?”  #speaker:P #portrait:neutural #tachie:none

“Why does he collect this amount of information?”  #speaker:P #portrait:neutural #tachie:none
-> choices


// Evidence No.3 Feast menu

-(chair)
there is a menu from "Feast" sit there silently on the chair 

“the one that he told me before. But the menu looks not like a usual restaurant.” #speaker:P #portrait:neutural #tachie:none
-> choices
// 第一个抽屉里  Evidence No.4


// Evidence NO.5 色欲手机：里面含有三个情人的电话号码

-(sims_phone)
Sim's phone is there on the floor; #speaker:P 

luckily there are still some battery left, unluckily you do not know the password. 

the screen lit up and you see three phone numbers as notifications. 

"three phone numbers? The names sound weird. Are they buddies with benefits?" #speaker:P #portrait:neutural #tachie:none
-> choices

