# Public Works Plus

Public Works Plus lets you scale **fleet sizes** and **vehicle capacities** in *Cities: Skylines II*.

## Features

### Public Transit
- **Depot capacity** (max vehicles per depot): Bus, **Ferry**, Taxi, Tram, Train, Subway
- **Passenger capacity** (seats per vehicle): Bus, Tram, Train, Subway, Ship, Ferry, Airplane
- Optional: **Expand Transit Line in-game slider limits** (can allow down to 1 vehicle on most routes; max is also higher than vanilla)

### Industry & Cargo
- **Delivery vehicle cargo capacity**: Semi Trucks, Delivery Vans, Raw Material trucks, Motorbike delivery
  - Delivery sliders are now **100% to 500%**
  - **100% = vanilla**
- **Cargo station fleet** (harbor/train/airport cargo stations): max active transporters
- **Extractor fleet**: max trucks for industrial extractors
- **Experimental dispatch improvements**
  - Improves use of raised truck capacities on proven road delivery paths
  - Strongest proven results currently on **CompanyShopping** and **StorageTransfer (Car)** paths

### Parks & Roads
- **Park maintenance**
  - Depot fleet size
  - Vehicle maintenance capacity
  - Vehicle maintenance rate
- **Road maintenance**
  - Depot fleet size
  - Work Shift capacity
  - Repair rate (alpha)
- **Road wear speed** (alpha)

### Debug Tools (About tab)
- **Prefab Scan Report** (writes `ModsData/PublicWorksPlus/ScanReport-Prefabs.txt`)
- **Cargo station resource watch** inside scan report
- **Live delivery cargo snapshot** inside scan report
- **Open log folder** / **Open report folder**
- Optional verbose logging (disable for normal gameplay)

## What is currently proven

Live testing now shows raised-capacity delivery loads above vanilla for:

- **Semi trucks**
- **Delivery vans**
- **Raw material trucks**
- Motorbike delivery can also appear, but is rarer in snapshots

Current strongest proven dispatch improvements are:

- **CompanyShopping**
- **StorageTransfer (Car)**

Some other dispatch paths are still being researched and are not claimed as fully solved yet.

## Notes
- Avoid running multiple mods that change the same capacities/policies (they can override each other).
- Changes apply while a city is loaded; no restart needed.
- Safe to remove any time (use reset buttons if you want to return to vanilla first).
- This mod does **not** use Harmony for these delivery-capacity and dispatch improvements.

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

### Why one city shows “max 4–8 buses” on a route line and another shows “max 20–125 buses”
In CS2 the line vehicle slider max is not a fixed cap.  
It’s derived from the game’s estimate of how many vehicles are needed to hit the tightest interval the policy allows, given that line’s round-trip cycle time.

So if City A has:

- fast roads / good bus flow
- low boarding dwell (fewer passengers)
- less congestion / fewer delays

…the cycle time is short, so even at the “most vehicles” end of the policy, the math only justifies 8 (or similar).

If City B has:

- heavy congestion (buses crawl)
- long dwell times (huge ridership, slow boarding)
- slow segments / lots of delay

…the cycle time becomes massive, so the same policy can justify 50–125 vehicles even on “short” routes.

The same mod toggle gives different maxes across saves because the inputs (traffic + dwell + effective speeds) differ a lot.

This mod does not use any invasive Harmony patch, which means it follows the game’s own route logic and expands the existing limits instead of replacing that logic.  
This means route lines are still **variable**, but players can still get the minimum **1 vehicle** they want and also much higher maximums than vanilla.

Takeaway: if maximum vehicles on lines drop dramatically in one save, that is usually a clue to check traffic and dwell-time problems in that city.

### Why the scan report might show only a few StorageTransfer trucks
The **live delivery cargo snapshot** is a **one-time live snapshot**, not a long-running average.  
So a line like `StorageTransfer=2/2` does **not** mean there were only two storage-transfer jobs in the whole city. It only means two currently carrying live trucks matched that category at the exact instant of the probe.

Use both:
- the **scan report**
- and the **verbose dispatch log**

to understand longer-running storage-transfer activity.
