# 09. Strangler extraction

Pairs. 40 minutes. The exercise repository: `src/Legacy`, the tests and decisions from activity 07, and the agents from activity 08 if you ran it. How to talk to the tools is in [tools.md](../tools.md).

## Goal

Extract one behaviour out of the legacy service into clean, tested code, protected the whole way by the characterisation tests. This is the pattern for retiring a legacy system a slice at a time while it keeps running.

## Steps

1. Choose one behaviour to extract. Let the numbers pick rather than taste: take the highest CRAP score from activity 07's run that your tests also cover well (the freight allocation, the price list lookup and the rounding are the natural slices), and check it is small enough for the time box. Write the score down, you will want it at the end. `Run the full test suite` and confirm it is green before you begin.
2. Write acceptance tests for the new implementation, tests first, in the style of activity 05. If you did not run activity 08, type `Use the acceptance-tests skill to write acceptance tests for a new <Behaviour> class in src/Calculator that replaces <the method> in src/Legacy/HarbourPricingService.cs, keeping every quirk marked KEEP in src/Legacy.Tests and fixing every quirk marked BUG`, read them together, and go to step 3. If you did run 08, type the same request with `Use the atdd agent` at the front. It is the first real job that agent has had, on a behaviour nobody has written tests for yet, so how it does is a verdict on your specification rather than on the model: if it quietly guesses at something the extraction leaves open instead of stopping to ask, that is a rule to tighten in the agent file rather than a prompt to reword. If it is not good enough yet, write the tests by hand and keep going; knowing that is worth as much as the agent working first time. The intended behaviour differs from the current one only where activity 07 marked a quirk as BUG: those you fix, deliberately. Every quirk marked KEEP is preserved exactly.
3. Hand the extraction to the agent: `Implement <Behaviour> in src/Calculator so the new acceptance tests pass. Do not modify anything under src/Legacy or any existing test. Run the tests and show me the output.`
4. When the new tests are green, make the swap at a seam: one place where callers reach the behaviour, routed to the new implementation behind the existing interface. Callers should not know anything changed. The old code path stays in place, unreferenced, until confidence is earned; deleting it is a later, separate, boring change.
5. `Run the full test suite and list every failing test with its message.` Expected result: new tests green; characterisation tests green except any that pin a quirk you deliberately fixed. Each of those failures must map one-to-one to a BUG decision. An unexpected failure means the net caught you: stop and look before touching anything.
6. Retire the characterisation tests that the accepted BUG fixes have made obsolete, citing the decision, and leave the rest guarding the remaining legacy behaviour.
7. Type `/crap-score src/Calculator/<Behaviour>.cs` and compare it with the number from step 1. The difference is what this slice bought, in a figure you can put in front of someone who was not in the room.

## Done when

The behaviour runs in the new service, every test failure was predicted by a BUG decision from activity 07, nothing else moved, and you can say what the extraction did to the CRAP score.

## Notes

The discipline is that behaviour changes only ever happen on purpose, visibly, in a test diff. If you finish early, extract a second behaviour; the second slice is where the pattern starts to feel routine.
