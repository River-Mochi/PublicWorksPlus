## Internal Systems & Behaviour — Adjust Transit Capacity [ATC]

Quick reference for how ATC works under the hood.

## Overview Table

| Area / Feature | What it does | Implementation (high level) |
|----------------|--------------|------------------------------|
| **Depot capacity** | Multiplies how many vehicles each transit depot can maintain or spawn. | Reads vanilla from `PrefabBase`, writes scaled values to `TransportDepotData.m_VehicleCapacity` on prefab entities. |
| **Passenger capacity** | Scales seats for public transport vehicles. | Reads vanilla from `PublicTransport` prefab, writes scaled values to `PublicTransportVehicleData.m_PassengerCapacity` on prefab entities. |
| **Prefab-based vanilla protection** | Prevents multipliers from stacking across runs. | Reads baselines from `PrefabBase` authoring components instead of current runtime `*Data` values. |
| **One-shot per apply** | Systems run only when explicitly triggered. | Systems enable, run once, then set `Enabled = false`. |
| **Settings changes reapply** | Slider changes apply immediately to the loaded city. | `Setting.Apply()` re-enables the transit systems for one more pass. |
| **Transit line slider tuner** | Optional widening of the line vehicle slider limits. | Edits `VehicleCountPolicy` `RouteModifierData` range for `VehicleInterval` when enabled. |
| **Debug logging** | Optional detailed logs to the mod log file. | Controlled by `EnableDebugLogging`, logs through `Mod.s_Log`. |
| **Safe locale loading** | Localization issues cannot break startup. | Locale sources are wrapped in `try/catch` around `LocalizationManager.AddSource`. |
| **Options UI layout** | Tabs: Public Transit / About. | `Setting` uses CO `SettingsUI*` attributes with locale-backed labels and descriptions. |
| **Log folder opener** | Opens the game Logs folder from the About tab. | `ShellOpen.OpenFolderSafe(...)` uses `file:///` first, then OS shell fallback. |
| **Minimal runtime work** | No background polling in normal use. | No work unless gameplay mode is active and a system is enabled by load or apply. |

## Notes

### Depot capacity
ATC modifies depot vehicle limits for:
- Bus, Ferry, Subway, Taxi, Train, Tram

### Passenger capacity
ATC modifies passenger seats for:
- Bus, Tram, Train, Subway, Ship, Ferry, Airplane

### Transit line tuner
The transit line feature is optional.

When enabled, it adjusts the vanilla `VehicleCountPolicy` so the in-game line slider can reach a lower minimum and a higher upper range than vanilla, while still following the game’s own route-based logic.

Because the game calculates the line result from route conditions, the maximum remains variable by save and by line.

### Vanilla baseline safety
ATC does not keep multiplying already-scaled runtime values.

Each apply pass reads the original baseline from the prefab authoring component, then writes a fresh scaled result. That keeps reset-to-vanilla behavior stable and avoids stacking drift.
