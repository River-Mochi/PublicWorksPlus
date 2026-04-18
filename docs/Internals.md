# Internal Systems & Behaviour — Public Works Plus [PWP]

Quick reference for how PWP works under the hood.

## Overview Table

| Area / Feature | What it does | Implementation (high level) |
|----------------|--------------|------------------------------|
| **Depot capacity** | Multiplies how many vehicles each transit depot can maintain/spawn. | Reads vanilla from `PrefabBase`, writes scaled values to `TransportDepotData.m_VehicleCapacity` (prefab entities). |
| **Passenger capacity** | Scales seats for public transport vehicles. | Reads vanilla from `PublicTransport` prefab, writes scaled values to `PublicTransportVehicleData.m_PassengerCapacity` (prefab entities). |
| **Industry cargo** | Scales cargo capacity for delivery vehicle prefabs (semi/van/raw/motorbike). | Buckets `DeliveryTruckData` prefabs, reads vanilla from `PrefabBase`, writes scaled `DeliveryTruckData.m_CargoCapacity` (prefab entities). |
| **Industry delivery slider UI** | Player-facing delivery sliders now show percent instead of x-factor. | Delivery vehicle settings store **100%..500%** values and `IndustrySystem` converts those percent values to runtime scalars when applying prefab changes. |
| **Live delivery proof** | Confirms raised capacities are actually used by live trucks in the city. | `DeliveryCargoProbeSystem` reads live `Game.Vehicles.DeliveryTruck.m_Amount` and compares it against vanilla/current prefab cargo caps. |
| **Cargo stations fleet** | Scales max active transporters for cargo stations. | Reads baseline from `CargoTransportStation` prefab (`transports`), writes scaled `TransportCompanyData.m_MaxTransports` (prefab entities). |
| **Extractor fleet** | Scales max trucks for industrial extractors. | Filters `TransportCompanyData` by prefab name patterns, scales `m_MaxTransports` (prefab entities). |
| **Company shopping dispatch tuning** | Promotes buyer-side request amounts toward fuller delivery truck loads. | Reads company buyer-side requests and raises request size toward the safe truck cap for the relevant delivery path. |
| **Storage transfer dispatch tuning** | Promotes road storage-transfer requests toward fuller truck loads. | Adjusts storage-company / cargo-station / OC outbound car requests toward the safe truck cap and mirrors matching inbound requests when needed. |
| **Dispatch category hints** | Gives a rough picture of which live truck paths are benefiting. | Probe groups live trucks into `CompanyShopping`, `StorageTransfer`, `OC-Transfer` hint, and `FacilityOwnedDispatch` hint using live truck state flags. |
| **Cargo station resource watch** | Shows live cargo-station stored garbage and transfer request activity. | Scan report inspects live cargo stations / ports and prints Garbage storage plus Car / Track / Transport request amounts. |
| **Maintenance vehicles** | Scales maintenance work capacity/rate. | Reads vanilla from `MaintenanceVehicle` prefab, writes scaled `MaintenanceVehicleData` fields (prefab entities). |
| **Maintenance depots** | Scales maximum maintenance vehicles allowed per depot. | Reads vanilla from `MaintenanceDepot` prefab, writes scaled `MaintenanceDepotData.m_VehicleCapacity` (prefab entities). |
| **Road wear speed (alpha)** | Adjusts how fast lanes deteriorate over time and traffic. | Scales `LaneDeteriorationData.m_TimeFactor` **and** `LaneDeteriorationData.m_TrafficFactor` (prefab entities). |
| **Prefab-based vanilla protection** | Prevents stacking multipliers across runs. | Reads baselines from `PrefabBase` (authoring components) instead of current `*Data` values. |
| **One-shot per apply** | Systems run only when explicitly triggered (load/apply/button). | Systems enable, run once, then set `Enabled = false`. |
| **Settings changes reapply** | Slider changes apply immediately to the loaded city. | `Setting.Apply()` re-enables the systems for one more pass. |
| **Transit line slider tuner** | Optional widening of the line vehicle slider limits. | Edits `VehicleCountPolicy` RouteModifier range when enabled. |
| **Prefab scan report** | Debug report of relevant prefabs, live cargo watch, and live delivery snapshot. | Button enables `PrefabScanSystem`, writes `ModsData/PublicWorksPlus/ScanReport-Prefabs.txt`. |
| **Debug logging** | Optional detailed logs to the mod log file. | Controlled by `EnableDebugLogging`, logs via `Mod.s_Log`. |
| **Safe locale loading** | Localization issues can’t break startup. | Locale sources wrapped in try/catch around `LocalizationManager.AddSource`. |
| **Options UI layout** | Tabs: Public-Transit / Industry / Parks-Roads / About. | `Setting` uses CO `SettingsUI*` attributes + Locale-backed labels/descs. |
| **Log/report folder opener** | Opens Logs or ModsData folder. | `ShellOpen.OpenFolderSafe(...)` uses `file:///` + shell fallback. |
| **Minimal runtime work** | No always-on background work in normal gameplay. | Systems run only when needed; verbose probe/report tools are opt-in debug/research tools. |

## Key game files / concepts

These are the main game-side places that mattered for the current dispatch work:

- `StorageCompanySystem` = where transfer amount is born
- `StorageTransferSystem` = where transfer amount is checked / normalized / queued
- `DeliveryTruckSelectData` = proves bigger truck capacities can be selected/used
- `ResourcePathfindSetup` = where source/target desirability is scored

## Current delivery-dispatch status

### Proven working now
- **CompanyShopping** is clearly using larger-than-vanilla request sizes and live trucks.
- **StorageTransfer (Car path)** is clearly using larger-than-vanilla request sizes and live trucks.
- **Semi / Van / Raw** delivery buckets are all proven above vanilla in live city snapshots.
- **Motorbike delivery** can also appear above vanilla, but it is much rarer in snapshots.

### Not fully isolated yet
- **OC-Transfer** is not yet isolated cleanly in the one-shot live snapshot.
- **FacilityOwnedDispatch** is not yet isolated cleanly in the current diagnostics.

### Important limitation
The `CATEGORY SUMMARY` section in the live delivery probe is a **one-shot live snapshot**.  
It does **not** count every dispatch job created over time.  
Because of that, `StorageTransfer=2/2` or similar does **not** mean only two storage-transfer jobs existed in the city overall. It only means two currently carrying live trucks matched that category at the instant of the probe.

## Delivery slider model

### Delivery vehicle sliders
Player-facing delivery sliders now use:

- **100% = vanilla**
- **500% = 5x**
- **25% steps**

These settings are stored as percent values for the UI, then converted to runtime scalars when the prefab cargo capacities are applied.

### Fleet sliders
Extractor fleet and cargo-station fleet are still separate fleet controls.  
They affect **how many** transporters can be active, not the cargo capacity of each delivery vehicle.

## Current scan report layout

scan report now includes these delivery-research sections:

1. **Current delivery slider results**
2. **Cargo station resource watch**
3. **Live delivery cargo snapshot**

These are the most useful sections for current delivery / dispatch tuning.

## What to watch when testing

### Good signs
- `OverCap=0`
- Semi / Van / Raw show meaningful percentages above vanilla
- CompanyShopping stays strong
- StorageTransfer keeps appearing in dispatch logs

### Still being researched
- Cleaner proof for OC road-transfer paths
- Better separation of seller/export behavior
- Better separation of facility-owned dispatch behavior

## Release position

Current state is strong enough to say:

- strongest proven wins are in **CompanyShopping** and **StorageTransfer (for road vehicles)**
- not every logistics / dispatch path in the game is fully solved.
