# QA battery: Unbreakable (CharmDiamond)

Deploy: Pockets, CharmCore and CharmDiamond are in the r2modman `Debug` profile (compare hashes with the builds). Restart the game first.

## Quick start with debug mode
Set `DebugMode = true` in the `Debug` section of `BepInEx/config/com.osmar.CharmDiamond.cfg` (off by default). When a level starts, a Diamond is placed on the floor about 2.5 m in front of you; the log then also shows the detailed lines. Any level works.

## Single player
- [ ] Log shows `Charm registered: Diamond - Unbreakable` and `CharmDiamond 1.0.0 loaded!`.
- [ ] Store the valuable in a slot: the slot shows its picture and value.
- [ ] With the Diamond pocketed, grab another valuable (or a second debug item) and slam it against walls, drop it from height, drag it along the floor: its value does not drop and no loss numbers appear.
- [ ] Each prevented hit wears the Diamond (slot value falls about 8% of its original); a long scrape within 0.5 s of the last charge costs nothing extra. Debug log shows `Unbreakable: prevented a hit`.
- [ ] Put the held item down (release it) and drop or hit it: it takes normal damage again.
- [ ] A second valuable stored in another pocket slot is not protected by the Diamond; it only loses value through its own charm wear, and charm wear (another charm triggering) still works while the Diamond is pocketed.
- [ ] Forced breaks (for example monsters or items that destroy valuables on purpose) still break held valuables.
- [ ] Take the valuable out: the power stops.
- [ ] Use it until the valuable breaks: the slot empties, the power stops, no errors in the log.

## Settings
- [ ] `WearCooldownSeconds = 0`: every prevented hit wears the Diamond. `WearPercent = 0`: protection with no wear. There is no cooldown veil on this charm.
- [ ] Charm `Enabled = false`: no effect and no wear. CharmCore `Enabled = false`: nothing works.

## Multiplayer (host and client)
- [ ] Host carries the Diamond: held valuables are protected for the host. Client carries the Diamond: the client's held valuables are protected too (the host decides), and the Diamond's value drops on both machines.
- [ ] A client without the Diamond holding a valuable gets no protection.
- [ ] Host settings override the client's.
- [ ] Client leaves and rejoins: protection follows whoever carries the Diamond.

Log patterns: `Charm registered`, `Unbreakable: prevented`, `threw`, `Exception`.
