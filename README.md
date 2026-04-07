# Adjust Transit Capacity

Adjust Transit Capacity lets you scale **fleet sizes** and **vehicle capacities** in *Cities: Skylines II*.

## Features

### Public Transit
- **Depot capacity** (max vehicles per depot): Bus, **Ferry**, Taxi, Tram, Train, Subway
- **Passenger capacity** (seats per vehicle): Bus, Tram, Train, Subway, Ship, Ferry, Airplane
- Optional: **Expand Transit Line in-game slider limits** (can allow down to 1 vehicle on most routes; max is also higher than vanilla)


### Debug Tools (About tab)
- **Prefab Scan Report** (writes `ModsData/AdjustTransit/ScanReport-Prefabs.txt`)
- **Open log folder** / **Open report folder**
- Optional verbose logging (disable for normal gameplay)

## Notes
- Avoid running multiple mods that change the same capacities/policies (they can override each other).
- Changes apply while a city is loaded; no restart needed.
- Safe to remove any time (use reset buttons if you want to return to vanilla first).

## Languages (11)
English, Français, Deutsch, Español, Italiano, 한국어, 日本語, 简体中文, 繁體中文, Português (Brazil), Polski

## Credits
- River-Mochi — author/maintainer, localization
- Inspired by Wayz’s original **Depot Capacity Changer**
- yenyang — code review & technical advice
- Necko1996, BugsyG — testing
- StarQ — technical advice

## Links
- GitHub: https://github.com/River-Mochi/PublicWorksPlus
- Paradox Mods: https://mods.paradoxplaza.com/authors/River-mochi/cities_skylines_2?games=cities_skylines_2&orderBy=desc&sortBy=best&time=alltime
- Support Discord: https://discord.gg/HTav7ARPs2

## License
MIT


## Troubleshooting

Why one city shows “max 4–8 buses” on a route line and another shows “max 20–125 buses”
In CS2 the line vehicle slider max is not a fixed cap.
It’s derived from the game’s estimate of how many vehicles are needed to hit the tightest interval the policy allows, given that line’s round-trip cycle time.

So if City A has:

fast roads / good bus flow
low boarding dwell (fewer passengers)
less congestion / fewer delays

…the cycle time is short, so even at the “most vehicles” end of the policy, the math only justifies 8 (or similar).

If City B has:

heavy congestion (buses crawl)
long dwell times (huge ridership, slow boarding)
slow segments / lots of delay

…the cycle time becomes massive, so the same policy can justify 50–125 vehicles even on “short” routes.

The same mod toggle gives different maxes across saves because the inputs (traffic + dwell + effective speeds) differ a lot.

This mod does not use any invasive Harmony patch which means we follow the game's own vanilla logic but try to expand and enhance it.
This means Route lines are "variable" but will still give players the minimum "1 bus" they want and also much higher maximums (20-50 buses), but it's not a static set maximum amount.
Takeaway is if you see Maximums on bus lines drop dramatically, it's a clue to check traffic problems as that is causing this reduced amount.
