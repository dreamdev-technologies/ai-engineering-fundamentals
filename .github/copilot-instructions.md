# Exercise repository for the AI Engineering Fundamentals session

A fictional produce importer, Meridian Fresh. Nothing here is real client code.

## Before changing anything

- Read `wiki/index.md`. The wiki is the source of truth for how the business works; the code is the source of truth for values (rates, margins, dates).
- `src/Legacy` is legacy on purpose. Do not tidy, reformat or fix it unless the task says so. Its behaviour gets pinned by tests before anything changes.

## Gotchas

- Never copy a rate, margin or exchange rate into a document or a test as a fact. Point at `DutyRates`, `ExchangeRates` and `PricingPolicy` instead; the values there change.
- Tests are the contract. Do not edit an existing test to make it pass. Say why you think it is wrong and stop.
- `Money` carries a currency. Never add amounts in different currencies.
- When a story leaves something open, list the questions rather than choosing an answer silently.

## Skills

Skills live in `.claude/skills/`, one folder per skill, and load on demand:

- `acceptance-tests`: how to turn a story into acceptance tests in this repository's style.
- `verify`: what to run, and what evidence to show, before claiming work is done.
