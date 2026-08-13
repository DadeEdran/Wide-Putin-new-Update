# Wide Putin VS White House

A Unity game project. Built with **Unity 2022.1.24f1** (Built-in Render Pipeline).

## ⚠️ Raw art assets are not in this repository

This repo contains the **project structure, logic and scene data** — but not the raw
art binaries. They were excluded to keep the repository within GitHub's free tier
limits (1 GB Git LFS storage, 100 MB hard per-file cap).

**What is here:**

- All C# scripts (`Assets/**/*.cs`)
- All scenes, prefabs, materials, animators and animation clips
- `ProjectSettings/` and `Packages/` — full editor and package configuration
- All `.meta` files, so asset GUIDs are preserved and references still resolve
  once the art is restored

**What is not here** (see `.gitignore`):

| Category | Extensions |
|---|---|
| Images | `.png` `.psd` `.jpg` `.tga` `.dds` `.exr` `.hdr` … |
| Audio | `.mp3` `.ogg` `.wav` … |
| Video | `.mp4` `.mov` `.avi` … |
| 3D models | `.fbx` `.obj` `.blend` … |
| Compiled binaries | `.dll` `.so` `.bundle` |

Cloning this repo alone will **not** give you a runnable game — Unity will open the
project with missing-asset references. The art must be copied in separately from a
local backup.

## Git LFS

One file exceeds GitHub's 100 MB per-file limit and is stored in Git LFS:

- `Assets/Scenes/levels/Level_2.unity` (~147 MB)

Install [Git LFS](https://git-lfs.com) before cloning, or that scene will come down
as a text pointer instead of the real file:

```sh
git lfs install
git clone https://github.com/DadeEdran/Wide-Putin-new-Update.git
```

`Level_1.unity` (81 MB) and `Level_3.unity` (89 MB) are under the cap and remain
plain YAML, so Unity's smart merge (`unityyamlmerge`) still works on them. If either
grows past 100 MB, it will need to move to LFS too — add it to `.gitattributes`
below the `*.unity` line.
