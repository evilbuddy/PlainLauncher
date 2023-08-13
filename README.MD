# PlainLauncher
A simple launcher

## Why making this ?
I made it to launch GZDoom with WADs.

## Why using this ?
I don't know. If you don't need it, then don't use it.

## How to use
- Open `PlainLauncher.exe`
- Click the `Settings` button at the bottom
- Enter your settings
  - Application executable (can browse with `...`)
  - Arguments (If `%F` is present, it will be replaced by the launched file. Otherwise, the file is placed at the end)
  - Folder containing the files (can browse with `...`)
- Save your settings with the `Save` button
- Select a file by clicking on it
- Launch the program with the selected parameters and file by clicking again

## Settings file
The settings are stored in the `pl.ini`, next to the executable.

```ini
[PlainLauncher]
app = \path\to\the\app.exe
argument = -argument
files = \path\to\the\files\
```

## Used libraries
- [.NET 5.0](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)
  - main framework
  - Yes, I know it has reached end-of-life.
- Microsoft.WindowsDesktop.App.WPF
  - UI base
- [ModernWpf](https://github.com/Kinnara/ModernWpf) (Kinnara)
    - nice, modern UI
- [ini-parser](https://github.com/rickyah/ini-parser) (rickyah)
  - read INI files
- [Windows-API-CodePack-NET](https://github.com/Wagnerp/Windows-API-CodePack-NET) (Microsoft, Wagnerp)
  - browse folder dialog