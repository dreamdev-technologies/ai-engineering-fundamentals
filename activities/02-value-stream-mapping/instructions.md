# 02. Value stream mapping

Groups of 3 or 4. 40 minutes. Whiteboard or the printed template.

## Goal

Map how long a change really takes from idea to production here, work out what share of that is development, and price what a faster coding stage is actually worth.

## What you need

The blank template: a row of stages, with three numbers under each stage:

- **Process time**: someone is actively working on it.
- **Wait time**: it is sitting in a queue.
- **%C/A** (percent complete and accurate): how often the next stage can use what this stage hands over without sending it back. Rework loops hide here, and this is where quality problems show up as time.

## Steps

1. Pick one recent, completed change your group knows well. Typical, not heroic: a normal feature that made it to production.
2. List its stages left to right, from the idea first being raised to running in production. Use the stages it really went through, not the official process. Five to eight stages is usual.
3. For each stage, agree the three numbers out loud: process time, wait time, %C/A. Estimates are fine; where the group cannot agree, write the range. Be honest about %C/A: how often does test hand back to dev, or UAT hand back to test?
4. Add up total lead time and total process time. Compute the activity ratio: process time divided by lead time. Anything under 25% means the work spends most of its life waiting.
5. Work out development's share: development process time divided by total lead time, as a percentage.
6. Now the arithmetic. Assume an AI coding tool makes the development stage 26% faster and changes nothing else. Apply that to the development stage only and recalculate total lead time. Write the percentage improvement next to the map.
7. Now the second arithmetic. Look at your lowest %C/A. Ask: if faster coding produces more changes of the same quality, what happens to the rework loop at that point? This is how a local speed-up makes a stream slower.
8. Circle the single longest wait and the lowest %C/A on the map. Discuss for five minutes: what would it take to halve the wait, and to raise the %C/A? Note the answers.

## Done when

Your map shows the stages with all three numbers per stage, the activity ratio, development's share of lead time, the recalculated lead time with a 26% faster coding stage, and a circled wait and %C/A with notes on improving each.

## The point, for after

Field benchmarks put development at roughly 10% to 22% of lead time, so a 26% coding gain moves total lead time by a few percent. The 2024 DORA finding that AI adoption reduced delivery stability is your %C/A column moving the wrong way. Whatever you circled in step 8 is probably worth more than faster typing. Keep the map: the debrief (activity 10) comes back to it, if you run one.
