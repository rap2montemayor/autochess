=== 000_testing_1 ===
If you are reading this, why are you reading this?
->DONE

=== 000_richtext_test ===
This is the magic sign.
It says: <color=blue>BLUE.</color>
It also says: <b>BOLD.</b>
->DONE

===000_annoyance_testing===
*{not 000_annoyance_testing.first}->first
*{not 000_annoyance_testing.next}->next
*{not 000_annoyance_testing.then}->then
*{not 000_annoyance_testing.last}->last
+->goaway


=first
1... 2... 3... 4... 5... 6... 7... 8... 9... 10... 11... 12... 13... 14... 15... 16... 17... 18... 19... 20... 21... 22... 23... 24... 25... 26... 27... 28... 29... 30... 31... 32... 33... 34... 35... 36... 37... 38... 39... 40.#NOSKIP
->DONE

=next
You came to talk to me again. You're so nice.
So let's have a real conversation this time! 
1... 2... 3... 4... 5... 6... 7... 8... 9... 10... 11... 12... 13... 14... 15... 16... 17... 18... 19... 20... 21... 22... 23... 24... 25... 26... 27... 28... 29... 30... 31... 32... 33... 34... 35... 36... 37... 38... 39... 40.#NOSKIP
->DONE

=then
31... 32... 33... 34... 35... 36... 37... 38... 39... 40... 
41... 42... 43... 44... 45... 46... 47... 48... 49... 50... 
51... 52... 53... 54... 55... 56... 57... 58... 59... 60... 
->DONE

=last
congrats
you are so bored that you made it to the end of my dialogue
the secret to life, the universe, and everything, is...
-> DONE

=goaway
nothing
->DONE


===000_branch_testing===
*{not 000_branch_testing.first}->first
*{not 000_branch_testing.second}->second
+->third

=first
I have a question for you.
What is 1 + 1?
*   Two
You are boring
->DONE
*   Eleven
You are old
->DONE
*   Seven
You want to be special, but you're not
->DONE

=second
That was the only math question I know!
->DONE

=third
Go away.
->DONE