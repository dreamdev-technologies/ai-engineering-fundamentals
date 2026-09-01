# Activities

A library of standalone activities, not a fixed agenda. Pull whichever ones fit the session you are running; each `instructions.md` is a self-contained step-by-step tutorial with its own time budget and done criterion. The numbering is an identifier and a sensible default order, not a schedule. Activity 01 is the opening talk, which the facilitator brings; the library starts at 02.

| # | Activity | Format | Time | Needs first |
|---|---|---|---|---|
| 02 | Value stream mapping | Groups of 3 or 4 | 40 min | Nothing |
| 03 | Config check-up | Pairs | 20 min | The exercise repo |
| 04 | The LLM wiki | Pairs | 30 min | The exercise repo |
| 05 | Acceptance tests first | Pairs | 45 min | 03; 04 helps |
| 06 | Goal-driven delegation | Pairs | 30 min | 05 (its verification) |
| 07 | Characterisation tests | Pairs | 40 min | The exercise repo |
| 08 | Agent creation | Pairs | 35 min | 05 and 07 (the work it bottles) |
| 09 | Strangler extraction | Pairs | 40 min | 07 (its tests); 08 if it ran |
| 10 | Close: debrief and 30-60-90 | Whole room | 30 min | Any of the above; 02's maps help |

Hard dependencies are few: 06 builds on the tests written in 05, 08 bottles 05 and 07 into reusable agents, and 09 is protected by the tests written in 07 while putting 08's agents to work. Everything else combines freely.

08 sits where it does for two reasons. It asks pairs to specify an agent for a job, which only goes well once they have done that job by hand in 05 and 07 and know what the agent has to be forbidden from doing. And it is followed immediately by 09, which puts the agents on new work, so a loose specification shows up as a problem the same morning rather than never.

Some workable combinations: **the argument** (02, 10) needs no laptops and no repo; **the delegation loop** (03, 04, 05, 06) is the hands-on core; **build your own tooling** (03, 05, 07, 08) ends with three agents in version control; **legacy safely** (07, 09) stands alone for a team mid-migration.

## Fitting a session

The whole library runs to about five hours of activities plus a forty-minute opening, which does not fit a half-day. A four-and-a-half-hour session holds the opening, the value stream map, three or four hands-on activities, the close, and two short breaks. Three tracks that fit, chosen at the end of the value stream debrief once you know what the room needs:

| Track | Activities | Activity time | For a room that |
|---|---|---|---|
| The delegation loop | 02, 03, 04, 05, 06, 10 | 195 min | wants to learn to hand work over safely, on new code |
| Context, tests, legacy | 02, 03, 05, 07, 10 (add 04 if ahead) | 175 to 205 min | is split between new build and migration |
| Legacy safely | 02, 03, 07, 08, 09, 10 | 205 min | is mostly living inside the legacy system |

Swap activities between tracks freely; the dependencies in the table above are the only constraint.

Activities 03, 05, 07, 08 and 09 use the `dotnet-test` plugin from the .NET team's skills repository, `dotnet/skills`. Activity 03 installs it and uses the install as a worked example of on-demand loading. If you run 07 and 09 on their own, install it first, or use the offline copy under `vendor/dotnet-skills`, which activity 03 also explains. The skills and agents are markdown files, reviewed and versioned like code. Prompts and repository contents go to the AI provider, exactly as they do with any agentic tool; nothing from any other codebase is involved.

Activities 03 to 09 use the exercise repository. Nothing in them touches any other codebase. Pairs work in Claude Code or GitHub Copilot; the instructions call out any difference.
