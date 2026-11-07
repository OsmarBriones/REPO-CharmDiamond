# CharmDiamond Architecture

A Charm mod (layer 3 of the PocketCharms family): one valuable, one power. It depends on **Pockets** (storage) and **CharmCore** (registry, wear, cooldown display, host settings, debug mode).

## The power: Unbreakable

While a player carries the Diamond in a pocket, valuables that player is holding in their hands take no damage (no value loss).

- `DiamondCharm : Charm` registers the prefab name `Valuable Wizard Diamond`, wear 8% per prevented hit, no cooldown, and one extra host-synced setting.
- `Patches/PhysGrabObjectImpactDetector_Break_Patch` is a Harmony prefix on `PhysGrabObjectImpactDetector.Break(valueLost, contactPoint, breakLevel, _forceBreak)`. All valuable value loss from physics goes through it on the master (it then sends `BreakRPC` to everybody), so returning false on the master prevents the loss on every machine. It runs only when `SemiFunc.IsMasterClientOrSingleplayer()`.
- `Unbreakable.ShouldBlock` decides. It blocks only when ALL hold: the break is not forced; the detector `isValuable`; the object is not `dead`; some carrier from `CharmsApi.ForEachCarrier` has its `PlayerAvatar.physGrabber` in `PhysGrabObject.playerGrabbing`; and the valuable is not pocketed (`PocketsApi.IsPocketed`).
- Wear: each blocked hit calls `charm.WearValuable(diamond, charm.WearPercent)` on the host, unless the same held valuable was charged less than `WearCooldownSeconds` ago (`Domain/WearThrottle`, keyed by the valuable's instance id and `Time.time`). The hit is prevented either way.

### Config keys (section `Charm.Diamond`)

| Key | Type / unit | Range | Default | Notes |
|-----|-------------|-------|---------|-------|
| `Enabled` | bool | | true | provided by CharmCore |
| `WearPercent` | float, percent of original value per prevented hit | 0 to 100 | 8 | provided by CharmCore; 0 means free protection |
| `WearCooldownSeconds` | float, seconds | 0 to 10 | 0.5 | minimum time between wear charges per held valuable; 0 charges every prevented hit; cannot be negative |
| `Debug.DebugMode` | bool | | false | provided by CharmCore |

## Game members used

- `PhysGrabObjectImpactDetector.Break` (4 parameters, internal; patched, parameter name `_forceBreak` is matched by Harmony) and `.isValuable`.
- `PhysGrabObject.playerGrabbing` (list of `PhysGrabber`) and `.dead`.
- `PlayerAvatar.physGrabber`.
- `ValuableObject.dollarValueCurrent` (debug log only).
- `SemiFunc.IsMasterClientOrSingleplayer()`.

All are checked by `CharmDiamond.Tests/GameContractTests.cs`. The rate limit is unit tested (`WearThrottleTests`).

## Decisions

- **Forced breaks are not blocked.** Game scripts that destroy valuables on purpose (for example `ValuableShootingCowboy`, `ValuableStarWand`) pass `_forceBreak = true`; only physics impacts are protected. This also guarantees CharmCore's charm wear (forced, on a pocketed valuable) is never blocked; the "not pocketed" check excludes it a second time.
- **Dead valuables are not touched**, so the destruction path of an already dead valuable is never blocked.
- **Only the carrier's own hands protect.** A valuable held by a player who does not carry the Diamond is not protected, and a pocketed charm valuable is never protected (it wears only through charm wear). `ProtectAllHeld` was not added: the design says the owner's hands, and a second switch would change gameplay.
- **One Diamond per player is used** (the first from `ForEachCarrier`).
- **Prevented hits still go through the vanilla callers** (`BreakLight/Medium/Heavy` set their own cooldowns and enemy investigation after `Break`), so noise or monster attraction from the impact is unchanged; only the value loss and break effect from `BreakRPC` are skipped.
- Throttle records are keyed by instance id and never pruned (a few numbers per held valuable per session).

## Known limits and unverified

- Not run in game. Unverified: that `playerGrabbing` on the master contains every holder (assumed synced by `GrabPlayerAddRPC`); that every valuable value loss really goes through `Break` (from reading the decompiled source); that Harmony resolves the internal `Break` method and `_forceBreak` name at runtime; behaviour with several Diamond carriers; the feel of the 0.5 s default wear window.
- Other damage sources that bypass `Break` (if any) are not covered.

## Debug mode

Provided by CharmCore: the `Debug.DebugMode` setting (off by default) places the valuable on the floor at the start of each level and enables `Charm.LogDebug` output.

## Known limits

- Looked up by exact prefab name; a game update that renames the prefab needs a mod update.
