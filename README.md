# Adjust Transit Capacity

Adjust Transit Capacity lets you scale **public transit depot** and **passenger capacity** in *Cities: Skylines II*.

## Features

### Public Transit
- **Depot capacity** (max vehicles per depot): Bus, **Ferry**, Taxi, Tram, Train, Subway
- **Passenger capacity** (seats per vehicle): Bus, Tram, Train, Subway, Ship, Ferry, Airplane
- Optional: **Extended Transit Lines** toggle  
  Expands the in-game transit line slider range.  
  It can allow down to **1 vehicle** on tested routes, and the upper limit can go much higher than vanilla.

### About Tab
- **Open log folder**
- Optional **verbose logging** for troubleshooting

## Notes
- Avoid using multiple mods that change the same depot, passenger, or transit-line policy values.
- Changes apply while a city is loaded; no restart needed.
- Safe to remove at any time. Use the reset buttons first if you want to return values to vanilla.
- This branch uses a **new settings file**: `AdjustTransit`  
  Older ATC saved settings do **not** carry over automatically.

## Languages (11)
English, Français, Deutsch, Español, Italiano, 한국어, 日本語, 简体中文, 繁體中文, Português (Brazil), Polski

## Credits
- River-Mochi — author/maintainer, localization
- Inspired by Wayz’s original **Depot Capacity Changer**
- yenyang — code review and technical advice
- Necko1996, BugsyG — testing
- StarQ — technical advice

## Links
- GitHub: https://github.com/River-Mochi/PublicWorksPlus/tree/atc-mod
- Paradox Mods: https://mods.paradoxplaza.com/authors/River-mochi/cities_skylines_2?games=cities_skylines_2&orderBy=desc&sortBy=best&time=alltime
- Support Discord: https://discord.gg/HTav7ARPs2

## License
MIT

## Troubleshooting

### Why one city shows “max 4–8 buses” on a route line and another shows “max 20–125 buses”

In CS2, the transit line vehicle slider does **not** use a fixed maximum cap.

The game estimates how many vehicles are needed to reach the tightest interval the policy allows, based on that line’s full round-trip cycle time.

So if City A has:
- fast roads and good bus flow
- low boarding dwell time
- less congestion and fewer delays

...then the cycle time stays short, so even at the “most vehicles” end of the policy, the game may only justify 8 vehicles or so.

If City B has:
- heavy congestion
- long dwell times from high ridership
- slow segments and frequent delays

...then the cycle time becomes much longer, so the same policy can justify 50–125 vehicles even on routes that look short.

That is why the same ATC toggle can give different maximums across different saves: the underlying inputs are different.

This mod does **not** use an invasive Harmony patch for transit line limits. It follows the game’s own vanilla route logic, but expands the usable slider range.

That means route-line maximums stay **variable**, not fixed to one hard cap. Players still get the minimum **1 vehicle** option they want, and can often get much higher maximums than vanilla, but the exact number depends on the route.

### Takeaway

If the maximum on a bus line drops a lot, that is often a clue to check:
- traffic congestion
- boarding delays
- overall route speed

Those route conditions directly affect how many vehicles the game thinks the line can support.
