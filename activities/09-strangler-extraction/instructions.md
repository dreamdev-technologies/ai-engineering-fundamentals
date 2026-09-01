# 09. Strangler extraction

Pairs. 40 minutes. The exercise repository: `src/Legacy`, the tests and decisions from activity 07, and the agents from activity 08.

## Goal

Extract one behaviour out of the legacy service into clean, tested code, protected the whole way by the characterisation tests. This is the pattern for retiring a legacy system a slice at a time while it keeps running.

## Steps

1. Choose one behaviour to extract. Let the numbers pick rather than taste: take the highest CRAP score from activity 07's coverage run that your tests also cover well, and check it is small enough for the time box. Write the score down, you will want it at the end. Confirm the suite is green before you begin.
2. Write acceptance tests for the new implementation, tests first, in the style of activity 05. Hand this to the ATDD agent you built in activity 08. It is the first real job that agent has had, on a behaviour nobody has written tests for yet, so how it does is a verdict on your specification rather than on the model: if it quietly guesses at something the extraction leaves open instead of stopping to ask, that is a rule to tighten in the agent file rather than a prompt to reword. If it is not good enough yet, write the tests by hand and keep going; knowing that is worth as much as the agent working first time. The intended behaviour differs from the current one only where activity 07 marked a quirk as BUG: those you fix, deliberately. Every quirk marked KEEP is preserved exactly.
3. Hand the extraction to the agent: implement a new service that passes the new acceptance tests, without modifying the legacy service or any existing test.
4. When the new tests are green, make the swap at a seam: one place where callers reach the behaviour, routed to the new implementation behind the existing interface. Callers should not know anything changed. The old code path stays in place, unreferenced, until confidence is earned; deleting it is a later, separate, boring change.
5. Run everything: the characterisation suite and the new acceptance tests. Expected result: new tests green; characterisation tests green except any that pin a quirk you deliberately fixed. Each of those failures must map one-to-one to a BUG decision. An unexpected failure means the net caught you: stop and look before touching anything.
6. Retire the characterisation tests that the accepted BUG fixes have made obsolete, citing the decision, and leave the rest guarding the remaining legacy behaviour.
7. Run `crap-score` on the behaviour in its new home and compare it with the number from step 1. The difference is what this slice bought, in a figure you can put in front of someone who was not in the room.

## Done when

The behaviour runs in the new service, every test failure was predicted by a BUG decision from activity 07, nothing else moved, and you can say what the extraction did to the CRAP score.

## Notes

The discipline is that behaviour changes only ever happen on purpose, visibly, in a test diff. If you finish early, extract a second behaviour; the second slice is where the pattern starts to feel routine.
