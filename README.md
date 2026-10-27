# Repro: Android Shell flyout content keeps stale bottom offset when FlyoutFooter changes height

Issue: https://github.com/dotnet/maui/issues/39679

## Environment
- .NET MAUI **10.0.110** (`<MauiVersion>` in the csproj; change it to test other versions)
- Android only (reported on Android 15, Xiaomi 12 Pro; code path is OS-version independent)

## Run
```
dotnet workload install maui-android   # if needed
dotnet build -t:Run -f net10.0-android
```

## Setup (what the project contains)
- `Shell` with `FlyoutBehavior="Flyout"`
- `Shell.FlyoutContentTemplate`: `ScrollView` (light blue) -> `VerticalStackLayout` with ~15 items
- `Shell.FlyoutFooterTemplate`: `VerticalStackLayout` (orange) with a `Border`
  whose `IsVisible` is bound to `IsBannerVisible` (initially `false`) plus two buttons
- A "Toggle footer banner" button in the flyout content and on the main page
  (also a delayed 4 s toggle so it can be triggered while the flyout is open)

## Steps
1. Launch the app on an Android phone/emulator.
2. Open the flyout (hamburger icon). Scroll the blue content to the end - the red
   "LAST ITEM" sits right above the orange footer. (Baseline: OK.)
3. Tap **Toggle footer banner** -> the footer grows by ~140 dp.
4. Scroll the content to the end.

## Expected
The bottom edge of the content always stays directly above the footer; the last item
is fully visible and not covered by the footer.

## Actual
The content keeps the bottom margin/padding computed from the *old* footer height:
- footer grew -> last item(s) are hidden behind the footer / odd extra scroll range
- footer shrank (start with the banner visible, i.e. set the initial value to `true` in
  `MainViewModel`, then toggle it off) -> large empty band between content and footer

## Root cause (from the issue)
`ShellFlyoutTemplatedContentRenderer.UpdateContentPadding()` is called for header changes
(`MeasureInvalidated` + `LayoutChange`) but never for footer size changes.
